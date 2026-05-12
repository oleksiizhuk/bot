using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Auto_Loot_RF_by_Yasir_Haq
{
    internal static class ScreenCapture
    {
        public static Bitmap CaptureWindow(IntPtr hwnd)
        {
            Win32.RECT r;
            Win32.GetWindowRect(hwnd, out r);
            int w = r.Right  - r.Left;
            int h = r.Bottom - r.Top;
            if (w <= 0 || h <= 0) return null;
            var bmp = new Bitmap(w, h, PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(bmp))
                g.CopyFromScreen(r.Left, r.Top, 0, 0, new Size(w, h));
            return bmp;
        }
    }
}
