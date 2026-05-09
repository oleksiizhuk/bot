using System;

namespace Auto_Loot_RF_by_Yasir_Haq
{
    internal static class InputSender
    {
        public static void SendKey(IntPtr hWnd, string keyStr)
        {
            if (hWnd == IntPtr.Zero) return;
            byte vk = ParseVK(keyStr);
            if (vk == 0) return;
            uint scan  = Win32.MapVirtualKey(vk, 0);
            IntPtr lpDn = (IntPtr)((scan << 16) | 1u);
            IntPtr lpUp = new IntPtr(unchecked((int)(0xC0000000u | (scan << 16) | 1u)));
            Win32.PostMessage(hWnd, Win32.WM_KEYDOWN, (IntPtr)vk, lpDn);
            Win32.PostMessage(hWnd, Win32.WM_KEYUP,   (IntPtr)vk, lpUp);
        }

        public static void Click(int x, int y)
        {
            Win32.SetCursorPos(x, y);
            Win32.mouse_event(Win32.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            Win32.mouse_event(Win32.MOUSEEVENTF_LEFTUP,   0, 0, 0, 0);
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
