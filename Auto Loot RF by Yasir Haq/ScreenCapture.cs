using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Auto_Loot_RF_by_Yasir_Haq
{
    internal static class ScreenCapture
    {
        // Client area (the game viewport, no title bar/border) in SCREEN coords.
        // All detection works in this space so clicks can never land on the
        // window chrome or outside the game.
        public static bool GetClientScreenRect(IntPtr hwnd, out Win32.RECT screenRect)
        {
            screenRect = new Win32.RECT();
            Win32.RECT cr;
            if (!Win32.GetClientRect(hwnd, out cr)) return false;
            int w = cr.Right - cr.Left;
            int h = cr.Bottom - cr.Top;
            if (w <= 0 || h <= 0) return false;

            var origin = new Win32.POINT { X = cr.Left, Y = cr.Top };
            Win32.ClientToScreen(hwnd, ref origin);
            screenRect.Left   = origin.X;
            screenRect.Top    = origin.Y;
            screenRect.Right  = origin.X + w;
            screenRect.Bottom = origin.Y + h;
            return true;
        }

        public static Bitmap CaptureWindow(IntPtr hwnd)
        {
            Win32.RECT r;
            if (!GetClientScreenRect(hwnd, out r)) return null;
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
