using System;
using System.Collections.Generic;
using OpenCvSharp;
using OpenCvSharp.Dnn;

namespace Auto_Loot_RF_by_Yasir_Haq
{
    // Neural-network mob detector: runs a YOLOv8 ONNX model (trained on game
    // screenshots) over a window capture and returns the mob nearest to screen
    // centre. Single class (0 = mob). Output tensor: [1, 5, 8400].
    internal sealed class YoloDetector : IDisposable
    {
        private const int InputSize = 640;

        private readonly Net _net;
        // CvDnn.Net.Forward is not thread-safe; several LootBots (one per game
        // window) share one detector, so serialise net access.
        private readonly object _gate = new object();
        public bool  IsLoaded     { get { return _net != null; } }
        // Diagnostics (last Detect call): highest raw anchor score over the whole
        // frame, and how many anchors passed the confidence threshold.
        public float LastMaxScore { get; private set; }
        public int   LastRawCount { get; private set; }
        public int   LastOutRows  { get; private set; }
        public int   LastOutCols  { get; private set; }
        public float Confidence   { get; set; }
        public float NmsThreshold { get; set; }
        // Fraction of the window's smaller side. Detections whose centre falls
        // within this radius of screen centre are ignored (that's the player's
        // own character, which the net otherwise mistakes for a mob).
        public float PlayerExcludeFrac { get; set; }

        public YoloDetector(string onnxPath)
        {
            Confidence        = 0.20f;   // model is weak; lower bar to catch more
            NmsThreshold      = 0.45f;
            // Player is always at screen centre and is bigger than the old 0.10
            // radius, so detections on its body edges were being clicked ("self
            // clicks"). 0.18 of the smaller side covers the player sprite while
            // still leaving most off-centre mobs clickable.
            PlayerExcludeFrac = 0.18f;
            _net = CvDnn.ReadNetFromOnnx(onnxPath);
            _net.SetPreferableBackend(Backend.OPENCV);
            _net.SetPreferableTarget(Target.CPU);
        }

        // Captures the window, detects mobs, returns SCREEN coordinates of the
        // detected mob nearest to the centre of the window, or null if none.
        public System.Drawing.Point? FindBestScreenPoint(IntPtr hwnd)
        {
            Win32.RECT wr;
            if (!ScreenCapture.GetClientScreenRect(hwnd, out wr)) return null;

            using (var bmp = ScreenCapture.CaptureWindow(hwnd))
            {
                if (bmp == null) return null;
                using (var src = MobDetector.BitmapToMat(bmp))
                {
                    int cx = src.Cols / 2, cy = src.Rows / 2;
                    double excludeR = Math.Min(src.Cols, src.Rows) * PlayerExcludeFrac;
                    double bestDist = double.MaxValue;
                    int bx = 0, by = 0;
                    bool found = false;

                    foreach (var d in Detect(src))
                    {
                        double dx = d.X - cx, dy = d.Y - cy;
                        double dist = Math.Sqrt(dx * dx + dy * dy);
                        if (dist < excludeR) continue;   // that's the player, skip
                        if (dist < bestDist) { bestDist = dist; bx = d.X; by = d.Y; found = true; }
                    }

                    if (!found) return null;
                    // Clamp inside the client area — never click the border/off-screen.
                    if (bx < 0) bx = 0; if (bx >= src.Cols) bx = src.Cols - 1;
                    if (by < 0) by = 0; if (by >= src.Rows) by = src.Rows - 1;
                    return new System.Drawing.Point(wr.Left + bx, wr.Top + by);
                }
            }
        }

        // All detected mobs as SCREEN coordinates, with the player (centre zone)
        // filtered out. Used by the closed-loop combat logic to lock onto a
        // target and tell when it dies (its detection disappears).
        public List<System.Drawing.Point> DetectScreenPoints(IntPtr hwnd)
        {
            var outPts = new List<System.Drawing.Point>();
            Win32.RECT wr;
            if (!ScreenCapture.GetClientScreenRect(hwnd, out wr)) return outPts;

            using (var bmp = ScreenCapture.CaptureWindow(hwnd))
            {
                if (bmp == null) return outPts;
                using (var src = MobDetector.BitmapToMat(bmp))
                {
                    int cx = src.Cols / 2, cy = src.Rows / 2;
                    double excludeR = Math.Min(src.Cols, src.Rows) * PlayerExcludeFrac;

                    foreach (var d in Detect(src))
                    {
                        double dx = d.X - cx, dy = d.Y - cy;
                        if (Math.Sqrt(dx * dx + dy * dy) < excludeR) continue;  // player
                        int bx = d.X, by = d.Y;
                        if (bx < 0) bx = 0; if (bx >= src.Cols) bx = src.Cols - 1;
                        if (by < 0) by = 0; if (by >= src.Rows) by = src.Rows - 1;
                        outPts.Add(new System.Drawing.Point(wr.Left + bx, wr.Top + by));
                    }
                }
            }
            return outPts;
        }

        // Mob centre points in image (window-local) coordinates.
        private struct Det { public int X, Y; }

        private List<Det> Detect(Mat src)
        {
            int w0 = src.Cols, h0 = src.Rows;

            // Letterbox: keep aspect ratio, pad to 640x640 with grey.
            float r   = Math.Min((float)InputSize / w0, (float)InputSize / h0);
            int   nw  = (int)Math.Round(w0 * r), nh = (int)Math.Round(h0 * r);
            int   padX = (InputSize - nw) / 2, padY = (InputSize - nh) / 2;

            var result = new List<Det>();

            using (var resized = new Mat())
            using (var canvas  = new Mat(InputSize, InputSize, MatType.CV_8UC3, new Scalar(114, 114, 114)))
            {
                Cv2.Resize(src, resized, new Size(nw, nh));
                using (var roi = new Mat(canvas, new Rect(padX, padY, nw, nh)))
                    resized.CopyTo(roi);

                using (var blob = CvDnn.BlobFromImage(canvas, 1.0 / 255.0,
                        new Size(InputSize, InputSize), new Scalar(), true, false))
                lock (_gate)
                {
                    _net.SetInput(blob);
                    using (var output = _net.Forward())   // [1, 5, 8400]
                    {
                        int rows = output.Size(1);         // 5  (cx,cy,w,h,score)
                        int cols = output.Size(2);         // 8400 anchors
                        LastOutRows = rows;
                        LastOutCols = cols;

                        using (var m  = output.Reshape(1, rows))   // rows x cols
                        using (var mt = new Mat())
                        {
                            Cv2.Transpose(m, mt);          // cols x rows

                            var boxes  = new List<Rect>();
                            var scores = new List<float>();
                            float maxScore = 0f;

                            for (int i = 0; i < cols; i++)
                            {
                                float score = mt.Get<float>(i, 4);
                                if (score > maxScore) maxScore = score;
                                if (score < Confidence) continue;

                                float bcx = mt.Get<float>(i, 0);
                                float bcy = mt.Get<float>(i, 1);
                                float bw  = mt.Get<float>(i, 2);
                                float bh  = mt.Get<float>(i, 3);

                                // Undo letterbox → original image coords.
                                float ox = (bcx - padX) / r;
                                float oy = (bcy - padY) / r;
                                float ow = bw / r;
                                float oh = bh / r;

                                boxes.Add(new Rect((int)(ox - ow / 2), (int)(oy - oh / 2),
                                                   (int)ow, (int)oh));
                                scores.Add(score);
                            }

                            LastMaxScore = maxScore;
                            LastRawCount = boxes.Count;

                            if (boxes.Count == 0) return result;

                            int[] keep;
                            CvDnn.NMSBoxes(boxes, scores, Confidence, NmsThreshold, out keep);
                            foreach (int i in keep)
                            {
                                var b = boxes[i];
                                result.Add(new Det { X = b.X + b.Width / 2, Y = b.Y + b.Height / 2 });
                            }
                        }
                    }
                }
            }

            return result;
        }

        public void Dispose()
        {
            if (_net != null) _net.Dispose();
        }
    }
}
