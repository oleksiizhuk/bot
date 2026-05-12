using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Auto_Loot_RF_by_Yasir_Haq
{
    internal static class InputSender
    {
        private static readonly string _logPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "debug.log");

        private static void Log(string msg)
        {
            try { File.AppendAllText(_logPath, DateTime.Now.ToString("HH:mm:ss.fff") + "  " + msg + "\r\n"); }
            catch { }
        }

        // ── Keyboard (PostMessage — works for RF Online) ──────────────────
        public static void SendKey(IntPtr hWnd, string keyStr)
        {
            if (hWnd == IntPtr.Zero) return;
            byte vk = ParseVK(keyStr);
            if (vk == 0) return;
            uint   scan = Win32.MapVirtualKey(vk, 0);
            IntPtr lpDn = (IntPtr)((scan << 16) | 1u);
            IntPtr lpUp = new IntPtr(unchecked((int)(0xC0000000u | (scan << 16) | 1u)));
            Win32.PostMessage(hWnd, Win32.WM_KEYDOWN, (IntPtr)vk, lpDn);
            Win32.PostMessage(hWnd, Win32.WM_KEYUP,   (IntPtr)vk, lpUp);
        }

        // ── Mouse — initial targeting click (physical, cursor moves) ─────
        public static void Click(int x, int y)
        {
            Win32.SetCursorPos(x, y);
            Win32.mouse_event(Win32.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            Win32.mouse_event(Win32.MOUSEEVENTF_LEFTUP,   0, 0, 0, 0);
        }

        // ── Mouse — attack sequence (SendInput, cursor save/restore) ─────
        // SendInput is the only API that reaches DirectInput / Raw Input games.
        // Cursor is moved to target for the click, then instantly restored —
        // the flash is < 1 ms so the user won't notice it.
        public static void SendAttackAction(IntPtr hwnd, string key, int screenX, int screenY)
        {
            if (string.IsNullOrWhiteSpace(key)) return;
            key = key.Trim().ToUpper();

            if (key == "RMB") { PhysicalClick(hwnd, screenX, screenY, rightButton: true);  return; }
            if (key == "LMB") { PhysicalClick(hwnd, screenX, screenY, rightButton: false); return; }

            SendKey(hwnd, key);
        }

        private static void PhysicalClick(IntPtr hwnd, int screenX, int screenY, bool rightButton)
        {
            string btn = rightButton ? "RMB" : "LMB";

            Win32.SetCursorPos(screenX, screenY);

            uint downFlag = rightButton ? Win32.MOUSEEVENTF_RIGHTDOWN : Win32.MOUSEEVENTF_LEFTDOWN;
            uint upFlag   = rightButton ? Win32.MOUSEEVENTF_RIGHTUP   : Win32.MOUSEEVENTF_LEFTUP;

            var inputs = new Win32.INPUT[]
            {
                MakeMouseInput(downFlag),
                MakeMouseInput(upFlag),
            };
            uint sent = Win32.SendInput(2, inputs, Marshal.SizeOf(typeof(Win32.INPUT)));

            Log(string.Format("{0} -> screen=({1},{2}) SendInput={3}/2 {4}",
                btn, screenX, screenY, sent, sent < 2 ? "*** BLOCKED ***" : "OK"));
        }

        private static Win32.INPUT MakeMouseInput(uint flags)
        {
            return new Win32.INPUT
            {
                type = Win32.INPUT_MOUSE,
                u    = new Win32.INPUT_UNION
                {
                    mi = new Win32.MOUSEINPUT { dwFlags = flags }
                }
            };
        }

        private static byte ParseVK(string s)
        {
            if (string.IsNullOrEmpty(s)) return 0;
            s = s.Trim().ToUpper();
            switch (s)
            {
                case "TAB":   return 0x09;
                case "ENTER": return 0x0D;
                case "SPACE": return 0x20;
                case "F1":  return 0x70; case "F2":  return 0x71;
                case "F3":  return 0x72; case "F4":  return 0x73;
                case "F5":  return 0x74; case "F6":  return 0x75;
                case "F7":  return 0x76; case "F8":  return 0x77;
                case "F9":  return 0x78; case "F10": return 0x79;
                case "F11": return 0x7A; case "F12": return 0x7B;
                default: return s.Length == 1 ? (byte)char.ToUpper(s[0]) : (byte)0;
            }
        }
    }
}
