using System;
using System.Diagnostics;

namespace Auto_Loot_RF_by_Yasir_Haq
{
    internal static class GameFinder
    {
        private static readonly string[] KnownProcessNames =
            { "rf_online", "rfonline", "rf2client", "rf2", "launcher_rf" };

        public static IntPtr Find()
        {
            IntPtr found = IntPtr.Zero;
            Win32.EnumWindows((hWnd, lp) =>
            {
                if (!Win32.IsWindowVisible(hWnd)) return true;
                uint pid;
                Win32.GetWindowThreadProcessId(hWnd, out pid);
                string name = "";
                try { name = Process.GetProcessById((int)pid).ProcessName.ToLower(); }
                catch { return true; }

                foreach (string known in KnownProcessNames)
                {
                    if (name.Contains(known)) { found = hWnd; return false; }
                }
                return true;
            }, IntPtr.Zero);
            return found;
        }
    }
}
