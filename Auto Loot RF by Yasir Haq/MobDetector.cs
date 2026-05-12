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
        private static Mat BitmapToMat(System.Drawing.Bitmap bmp)
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

        // Returns screen coordinates of best match, or null if below threshold.
        public System.Drawing.Point? FindBestScreenPoint(IntPtr hwnd)
        {
            if (_list.Count == 0) return null;

            Win32.RECT wr;
            Win32.GetWindowRect(hwnd, out wr);

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
