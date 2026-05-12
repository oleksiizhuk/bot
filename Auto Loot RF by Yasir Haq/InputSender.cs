using System;
using System.IO;

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

        // ── Mouse — physical click with cursor + focus save/restore ─────
        // DirectInput games (RF Online) ignore PostMessage/SendMessage for mouse.
        // We save the current foreground window and cursor pos, switch to the game
        // just long enough to click, then restore both immediately after.
        public static void Click(IntPtr hwnd, int screenX, int screenY, bool rightButton = false)
        {
            Win32.POINT saved;
            Win32.GetCursorPos(out saved);

            Win32.SetForegroundWindow(hwnd);
            System.Threading.Thread.Sleep(20);
            Win32.SetCursorPos(screenX, screenY);

            uint dn = rightButton ? Win32.MOUSEEVENTF_RIGHTDOWN : Win32.MOUSEEVENTF_LEFTDOWN;
            uint up = rightButton ? Win32.MOUSEEVENTF_RIGHTUP   : Win32.MOUSEEVENTF_LEFTUP;
            Win32.mouse_event(dn, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(30);
            Win32.mouse_event(up, 0, 0, 0, 0);

            Win32.SetCursorPos(saved.X, saved.Y);
        }

        public static void SendAttackAction(IntPtr hwnd, string key, int screenX, int screenY)
        {
            if (string.IsNullOrWhiteSpace(key)) return;
            key = key.Trim().ToUpper();

            if (key == "RMB") { Click(hwnd, screenX, screenY, rightButton: true);  return; }
            if (key == "LMB") { Click(hwnd, screenX, screenY, rightButton: false); return; }

            SendKey(hwnd, key);
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
