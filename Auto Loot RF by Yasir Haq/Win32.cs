using System;
using System.Runtime.InteropServices;

namespace Auto_Loot_RF_by_Yasir_Haq
{
    internal static class Win32
    {
        [DllImport("user32.dll")] public static extern bool   PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")] public static extern uint   MapVirtualKey(uint uCode, uint uMapType);
        [DllImport("user32.dll")] public static extern bool   EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)] public static extern int GetWindowText(IntPtr hWnd, System.Text.StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll")] public static extern bool   IsWindowVisible(IntPtr hWnd);
        [DllImport("user32.dll")] public static extern uint   GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        [DllImport("user32.dll")] public static extern bool   SetCursorPos(int X, int Y);
        [DllImport("user32.dll")] public static extern bool   GetCursorPos(out POINT lpPoint);
        [DllImport("user32.dll")] public static extern uint   SendInput(uint nInputs, INPUT[] pInputs, int cbSize);
        [DllImport("user32.dll")] public static extern void   mouse_event(uint dwFlags, int dx, int dy, uint dwData, int dwExtraInfo);
        [DllImport("user32.dll")] public static extern bool   AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);
        [DllImport("user32.dll")] public static extern IntPtr SetFocus(IntPtr hWnd);
        [DllImport("kernel32.dll")] public static extern uint GetCurrentThreadId();

        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        // ── Structs for SendInput ─────────────────────────────────────────
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT { public int X; public int Y; }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT  { public int Left, Top, Right, Bottom; }

        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEINPUT
        {
            public int    dx, dy;
            public uint   mouseData;
            public uint   dwFlags;
            public uint   time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct INPUT_UNION
        {
            [FieldOffset(0)] public MOUSEINPUT mi;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct INPUT
        {
            public uint       type;   // 0 = mouse
            public INPUT_UNION u;
        }

        [DllImport("user32.dll")] public static extern bool   ScreenToClient(IntPtr hWnd, ref POINT lpPoint);
        [DllImport("user32.dll")] public static extern bool   GetWindowRect(IntPtr hWnd, out RECT lpRect);
        [DllImport("user32.dll")] public static extern bool   SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")] public static extern bool   BringWindowToTop(IntPtr hWnd);
        [DllImport("user32.dll")] public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")] public static extern short  GetAsyncKeyState(int vKey);
        [DllImport("user32.dll")] public static extern IntPtr WindowFromPoint(POINT point);

        public const int VK_LBUTTON = 0x01;

        // ── Constants ────────────────────────────────────────────────────
        public const uint INPUT_MOUSE = 0;

        public const uint WM_KEYDOWN     = 0x0100;
        public const uint WM_KEYUP       = 0x0101;
        public const uint WM_LBUTTONDOWN = 0x0201;
        public const uint WM_LBUTTONUP   = 0x0202;
        public const uint WM_RBUTTONDOWN = 0x0204;
        public const uint WM_RBUTTONUP   = 0x0205;

        public const uint MK_LBUTTON = 0x0001;
        public const uint MK_RBUTTON = 0x0002;

        public const uint MOUSEEVENTF_LEFTDOWN  = 0x0002;
        public const uint MOUSEEVENTF_LEFTUP    = 0x0004;
        public const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        public const uint MOUSEEVENTF_RIGHTUP   = 0x0010;

        public static IntPtr MakeLParam(int x, int y)
        {
            return (IntPtr)(((ushort)y << 16) | (ushort)x);
        }
    }
}
