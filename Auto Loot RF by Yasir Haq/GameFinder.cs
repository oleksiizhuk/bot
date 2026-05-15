using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Auto_Loot_RF_by_Yasir_Haq
{
    internal static class GameFinder
    {
        private static readonly string[] KnownProcessNames =
            { "rf_online", "rfonline", "rf2client", "rf2", "launcher_rf" };

        public static IntPtr Find()
        {
            var all = FindAll();
            return all.Length > 0 ? all[0] : IntPtr.Zero;
        }

        // Returns known RF windows first, then falls back to all visible windows with a title.
        public static IntPtr[] FindAll()
        {
            var known = new List<IntPtr>();
            var other = new List<IntPtr>();

            Win32.EnumWindows((hWnd, lp) =>
            {
                if (!Win32.IsWindowVisible(hWnd)) return true;
                var sb = new StringBuilder(256);
                if (Win32.GetWindowText(hWnd, sb, sb.Capacity) == 0) return true;
                string title = sb.ToString().Trim();
                if (string.IsNullOrEmpty(title)) return true;

                uint pid;
                Win32.GetWindowThreadProcessId(hWnd, out pid);
                string name = "";
                try { name = Process.GetProcessById((int)pid).ProcessName.ToLower(); }
                catch { }

                bool isKnown = false;
                foreach (string k in KnownProcessNames)
                    if (name.Contains(k)) { isKnown = true; break; }

                if (isKnown) known.Add(hWnd);
                else         other.Add(hWnd);
                return true;
            }, IntPtr.Zero);

            // Return known RF windows; if none found, fall back to all visible windows.
            return known.Count > 0 ? known.ToArray() : other.ToArray();
        }

        public static string GetWindowTitle(IntPtr hWnd)
        {
            uint pid;
            Win32.GetWindowThreadProcessId(hWnd, out pid);
            var sb = new StringBuilder(256);
            Win32.GetWindowText(hWnd, sb, sb.Capacity);
            string title = sb.ToString().Trim();
            string procName = "";
            try { procName = Process.GetProcessById((int)pid).ProcessName; } catch { }
            return string.IsNullOrEmpty(title)
                ? string.Format("[{0}] PID {1}", procName, pid)
                : string.Format("{0}  [{1}]", title, procName);
        }
    }
}
