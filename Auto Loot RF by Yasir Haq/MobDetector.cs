using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using OpenCvSharp;

namespace Auto_Loot_RF_by_Yasir_Haq
{
    internal sealed class MobDetector : IDisposable
    {
        private class Entry { public string Name; public Mat Tmpl; }
        private readonly List<Entry> _list = new List<Entry>();

        private float _threshold = 0.8f;
        public float Threshold { get { return _threshold; } set { _threshold = value; } }
        public int   Count     { get { return _list.Count; } }

        // Convert System.Drawing.Bitmap (GAC) to Mat without BitmapConverter.
        // BitmapConverter expects System.Drawing.Common (NuGet), which conflicts
        // with the .NET Framework GAC System.Drawing — different assemblies, same type name.
        internal static Mat BitmapToMat(System.Drawing.Bitmap bmp)
        {
            var rect = new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height);
            var bd   = bmp.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            try
            {
                using (var h = Mat.FromPixelData(bmp.Height, bmp.Width, MatType.CV_8UC3, bd.Scan0, (long)bd.Stride))
                    return h.Clone();
            }
            finally { bmp.UnlockBits(bd); }
        }

        public void AddTemplate(string name, System.Drawing.Bitmap bmp)
        {
            _list.Add(new Entry { Name = name, Tmpl = BitmapToMat(bmp) });
        }

        public void Remove(string name)
        {
            int idx = _list.FindIndex(e => e.Name == name);
            if (idx < 0) return;
            _list[idx].Tmpl.Dispose();
            _list.RemoveAt(idx);
        }

        public void Clear()
        {
            foreach (var e in _list) e.Tmpl.Dispose();
            _list.Clear();
        }

        // Motion detection: mobs move, terrain is static. Diffs two frames
        // ~400 ms apart and returns the moving blob closest to screen centre
        // (closest mob to the character). Template-free. Blocks the calling
        // thread for the capture interval — call from a background task.
        public System.Drawing.Point? FindByMotionScreenPoint(IntPtr hwnd)
        {
            Win32.RECT wr;
            if (!ScreenCapture.GetClientScreenRect(hwnd, out wr)) return null;

            Mat g1 = CaptureGray(hwnd);
            if (g1 == null) return null;
            System.Threading.Thread.Sleep(400);
            Mat g2 = CaptureGray(hwnd);
            if (g2 == null) { g1.Dispose(); return null; }

            using (g1)
            using (g2)
            using (var diff = new Mat())
            {
                if (g1.Size() != g2.Size()) return null; // window resized mid-capture

                Cv2.Absdiff(g1, g2, diff);
                Cv2.Threshold(diff, diff, 22, 255, ThresholdTypes.Binary);
                using (var kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new Size(7, 7)))
                    Cv2.Dilate(diff, diff, kernel);

                int w  = diff.Cols, h = diff.Rows;
                int cx = w / 2,     cy = h / 2;
                // Player sits at exact centre and animates constantly; its sprite
                // (mounted beast / big MAU mech) is large, so exclude a generous
                // radius or its moving legs/weapon get picked as the nearest blob.
                double excludeR = Math.Min(w, h) * 0.22;

                // Skip HUD zones: top bars, bottom chat/inventory, side panels.
                var roi = new Rect((int)(w * 0.08), (int)(h * 0.12),
                                   (int)(w * 0.84), (int)(h * 0.56));

                Point[][]        contours;
                HierarchyIndex[] hier;
                using (var roiMat = new Mat(diff, roi))
                    Cv2.FindContours(roiMat, out contours, out hier,
                        RetrievalModes.External, ContourApproximationModes.ApproxSimple);

                double bestDist = double.MaxValue;
                int bx = 0, by = 0;

                foreach (var c in contours)
                {
                    if (Cv2.ContourArea(c) < 60) continue; // capture noise
                    var r  = Cv2.BoundingRect(c);
                    int mx = roi.X + r.X + r.Width  / 2;
                    int my = roi.Y + r.Y + r.Height / 2;

                    double dx = mx - cx, dy = my - cy;
                    double dist = Math.Sqrt(dx * dx + dy * dy);
                    if (dist < excludeR) continue; // own character animating at centre

                    if (dist < bestDist) { bestDist = dist; bx = mx; by = my; }
                }

                if (bestDist == double.MaxValue) return null;
                return new System.Drawing.Point(wr.Left + bx, wr.Top + by);
            }
        }

        private static Mat CaptureGray(IntPtr hwnd)
        {
            using (var bmp = ScreenCapture.CaptureWindow(hwnd))
            {
                if (bmp == null) return null;
                using (var m = BitmapToMat(bmp))
                {
                    var gray = new Mat();
                    Cv2.CvtColor(m, gray, ColorConversionCodes.BGR2GRAY);
                    Cv2.GaussianBlur(gray, gray, new Size(5, 5), 0);
                    return gray;
                }
            }
        }

        // Returns screen coordinates of best match, or null if below threshold.
        public System.Drawing.Point? FindBestScreenPoint(IntPtr hwnd)
        {
            if (_list.Count == 0) return null;

            Win32.RECT wr;
            if (!ScreenCapture.GetClientScreenRect(hwnd, out wr)) return null;

            using (var bmp = ScreenCapture.CaptureWindow(hwnd))
            {
                if (bmp == null) return null;

                using (var screen = BitmapToMat(bmp))
                using (var gray   = new Mat())
                {
                    Cv2.CvtColor(screen, gray, ColorConversionCodes.BGR2GRAY);

                    double best = 0;
                    int bx = 0, by = 0;

                    foreach (var entry in _list)
                    {
                        if (entry.Tmpl.Rows > screen.Rows || entry.Tmpl.Cols > screen.Cols) continue;

                        using (var tg  = new Mat())
                        using (var res = new Mat())
                        {
                            Cv2.CvtColor(entry.Tmpl, tg, ColorConversionCodes.BGR2GRAY);
                            Cv2.MatchTemplate(gray, tg, res, TemplateMatchModes.CCoeffNormed);

                            double minVal, maxVal;
                            OpenCvSharp.Point minLoc, maxLoc;
                            Cv2.MinMaxLoc(res, out minVal, out maxVal, out minLoc, out maxLoc);

                            if (maxVal > best)
                            {
                                best = maxVal;
                                bx   = maxLoc.X + entry.Tmpl.Cols / 2;
                                by   = maxLoc.Y + entry.Tmpl.Rows / 2;
                            }
                        }
                    }

                    if (best < _threshold) return null;
                    return new System.Drawing.Point(wr.Left + bx, wr.Top + by);
                }
            }
        }

        public void Dispose() { Clear(); }
    }
}
