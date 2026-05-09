using System;
using System.Runtime.InteropServices;

namespace Auto_Loot_RF_by_Yasir_Haq
{
    internal static class Win32
    {
        [DllImport("user32.dll")] public static extern bool   PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")] public static extern uint   MapVirtualKey(uint uCode, uint uMapType);
        [DllImport("user32.dll")] public static extern bool   EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);
        [DllImport("user32.dll")] public static extern bool   IsWindowVisible(IntPtr hWnd);
        [DllImport("user32.dll")] public static extern uint   GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        [DllImport("user32.dll")] public static extern bool   SetCursorPos(int X, int Y);
        [DllImport("user32.dll")] public static extern void   mouse_event(uint dwFlags, int dx, int dy, uint dwData, int dwExtraInfo);

        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        public const uint WM_KEYDOWN           = 0x0100;
        public const uint WM_KEYUP             = 0x0101;
        public const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        public const uint MOUSEEVENTF_LEFTUP   = 0x0004;
    }
}
