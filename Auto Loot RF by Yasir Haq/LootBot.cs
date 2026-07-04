using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Auto_Loot_RF_by_Yasir_Haq
{
    // KILL+LOOT is a closed-loop, vision-driven controller (not a fixed-timer
    // FSM): it locks onto one mob, attacks until that mob disappears from the
    // detector (= dead), loots, then re-acquires. This stops the old "attack a
    // fixed number of seconds then run off" behaviour.
    internal sealed class LootBot
    {
        public bool        IsLootActive     { get; private set; }
        public bool        IsKillLootActive { get; private set; }
        public MobDetector Detector         { get; set; }
        public YoloDetector Yolo            { get; set; }  // neural-net mob detection
        public bool        UseYolo          { get; set; }  // AI (ONNX) mob detection
        public bool        UseMotion        { get; set; }  // frame-diff mob detection
        public bool        UseTemplates     { get; set; }  // template-match mob detection
        public string      DatasetDir       { get; set; }  // save a screenshot each engage when set

        // --- closed-loop tuning (fractions of the window's smaller side) ---
        // Only mobs within this radius of screen centre are engaged → the bot no
        // longer runs across the map chasing far targets.
        private const double EngageFrac = 0.45;
        // Detections within this radius of screen centre are the player's own
        // character (camera is locked on it) and must never be acquired — the
        // player sprite (mounted beast / MAU mech) is big, so this covers it.
        // Enforced in the loop so it holds whichever detector produced the point.
        private const double PlayerFrac = 0.22;
        // A locked target counts as alive while a detection stays within this
        // radius of its last seen position.
        private const double TolFrac = 0.16;
        // Consecutive scans with no mob near the target before we call it dead.
        private const int MissScansToKill = 2;
        private const int TickMs = 150;

        private readonly System.Windows.Forms.Timer _timerLoot =
            new System.Windows.Forms.Timer { Interval = 150 };

        private IntPtr   _hwnd;
        private string[] _attackKeys;
        private string   _lootKey;
        private int      _clickX, _clickY, _keyDelay;
        private int      _maxEngageMs, _lootMs;
        private CancellationTokenSource _cts;

        public LootBot()
        {
            _timerLoot.Tick += OnLootTick;
        }

        public void StartLoot(IntPtr hwnd, string lootKey)
        {
            Stop();
            _hwnd        = hwnd;
            _lootKey     = lootKey;
            IsLootActive = true;
            _timerLoot.Start();
        }

        // NOTE on params (meaning changed for the closed loop):
        //   killSec  = MAX seconds to stay on one mob before giving up (safety
        //              cap); normally the bot leaves the instant the mob dies.
        //   lootSec  = how long to loot after a confirmed kill.
        //   clickX/Y = retained for API compatibility; no longer clicked on
        //              empty ground (that caused the "runs to nowhere" bug).
        public void StartKillLoot(IntPtr hwnd, string[] attackKeys, string lootKey,
                                   int killSec, int lootSec, int clickX, int clickY, int keyDelay)
        {
            Stop();
            _hwnd        = hwnd;
            _attackKeys  = attackKeys;
            _lootKey     = lootKey;
            _clickX      = clickX;
            _clickY      = clickY;
            _keyDelay    = keyDelay;
            _maxEngageMs = Math.Max(1000, killSec * 1000);
            _lootMs      = Math.Max(200,  lootSec * 1000);
            IsKillLootActive = true;
            _cts = new CancellationTokenSource();
            var tok = _cts.Token;
            Log("StartKillLoot hwnd=" + hwnd + " useYolo=" + UseYolo +
                " yoloLoaded=" + (Yolo != null && Yolo.IsLoaded) +
                " useMotion=" + UseMotion + " useTmpl=" + UseTemplates +
                " maxEngageMs=" + _maxEngageMs + " lootMs=" + _lootMs);
            Task.Run(() => KillLootLoop(tok));
        }

        public void Stop()
        {
            _timerLoot.Stop();
            if (_cts != null)
            {
                try { _cts.Cancel(); } catch { }
                _cts = null;
            }
            IsLootActive     = false;
            IsKillLootActive = false;
            _hwnd            = IntPtr.Zero;
        }

        private void OnLootTick(object sender, EventArgs e)
        {
            InputSender.SendKey(_hwnd, _lootKey);
        }

        // Lightweight diagnostics → bin/Release/debug.log
        private static readonly object _logGate = new object();
        private static void Log(string msg)
        {
            try
            {
                string path = System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, "debug.log");
                lock (_logGate)
                    System.IO.File.AppendAllText(path,
                        DateTime.Now.ToString("HH:mm:ss.fff") + "  " + msg + "\r\n");
            }
            catch { }
        }

        private enum State { Acquire, Engage, Loot }

        private void KillLootLoop(CancellationToken tok)
        {
            var h         = _hwnd;
            var state     = State.Acquire;
            int targetX   = 0, targetY = 0;
            int missScans = 0;
            int iter      = 0;
            var engage    = new Stopwatch();
            Log("KillLootLoop started");

            while (!tok.IsCancellationRequested && h != IntPtr.Zero)
            {
                try
                {
                    Win32.RECT cr;
                    if (!ScreenCapture.GetClientScreenRect(h, out cr))
                    {
                        Log("GetClientScreenRect FAILED for hwnd=" + h);
                        Sleep(tok, TickMs); continue;
                    }
                    int cenX    = (cr.Left + cr.Right)  / 2;
                    int cenY    = (cr.Top  + cr.Bottom) / 2;
                    int minSide = Math.Min(cr.Right - cr.Left, cr.Bottom - cr.Top);
                    double engageR = minSide * EngageFrac;
                    double playerR = minSide * PlayerFrac;
                    double tolR    = minSide * TolFrac;

                    List<Point> mobs = FindMobs(h);
                    if ((iter++ % 5) == 0)
                    {
                        string diag = "";
                        if (UseYolo && Yolo != null)
                            diag = " raw=" + Yolo.LastRawCount +
                                   " maxScore=" + Yolo.LastMaxScore.ToString("0.000") +
                                   " out=" + Yolo.LastOutRows + "x" + Yolo.LastOutCols;
                        Log("iter=" + iter + " state=" + state + " mobs(kept)=" + mobs.Count +
                            diag + " client=" + (cr.Right - cr.Left) + "x" + (cr.Bottom - cr.Top));
                    }
                    // One-shot: save the exact frame fed to the detector so we can
                    // see whether the capture is a real game image or black.
                    if (iter == 2) SaveDebugCapture(h);

                    switch (state)
                    {
                        case State.Acquire:
                        {
                            // Pick the nearest mob inside the engage ring but
                            // OUTSIDE the player zone, so we never click ourselves.
                            Point? best = NearestWithin(mobs, cenX, cenY, engageR, playerR);
                            if (best.HasValue)
                            {
                                if (DatasetDir != null) SaveDatasetShot(h, DatasetDir);
                                targetX = best.Value.X; targetY = best.Value.Y;
                                Log("ACQUIRE -> target=" + targetX + "," + targetY + "  ENGAGE");
                                InputSender.Click(h, targetX, targetY);   // select / start attacking
                                missScans = 0;
                                engage.Reset(); engage.Start();
                                state = State.Engage;
                            }
                            else
                            {
                                // No mob detected → fall back to blind attack+loot
                                // so KILL+LOOT still works when the detector finds
                                // nothing (e.g. model issues). Aim at configured
                                // coords, or screen centre if unset.
                                int ax = (_clickX != 0 || _clickY != 0) ? _clickX : cenX;
                                int ay = (_clickX != 0 || _clickY != 0) ? _clickY : cenY;
                                if ((iter % 20) == 0) Log("blind attack+loot at " + ax + "," + ay);
                                Attack(h, ax, ay, tok);
                                InputSender.SendKey(h, _lootKey);
                            }
                            break;
                        }

                        case State.Engage:
                        {
                            Point? still = NearestWithin(mobs, targetX, targetY, tolR, 0);
                            if (still.HasValue)
                            {
                                targetX = still.Value.X; targetY = still.Value.Y;
                                missScans = 0;
                                Attack(h, targetX, targetY, tok);
                            }
                            else
                            {
                                missScans++;
                                if (missScans >= MissScansToKill) { Log("target lost -> LOOT"); state = State.Loot; break; }
                            }
                            // Safety: never get stuck forever on one mob.
                            if (engage.ElapsedMilliseconds >= _maxEngageMs) state = State.Loot;
                            break;
                        }

                        case State.Loot:
                        {
                            var lootWatch = Stopwatch.StartNew();
                            while (!tok.IsCancellationRequested &&
                                   lootWatch.ElapsedMilliseconds < _lootMs)
                            {
                                InputSender.SendKey(h, _lootKey);
                                Sleep(tok, TickMs);
                            }
                            missScans = 0;
                            state = State.Acquire;
                            break;
                        }
                    }
                }
                catch (Exception ex) { Log("LOOP EX: " + ex.GetType().Name + ": " + ex.Message); }

                Sleep(tok, TickMs);
            }
            Log("KillLootLoop ended (cancelled=" + tok.IsCancellationRequested + ")");
        }

        private void Attack(IntPtr h, int x, int y, CancellationToken tok)
        {
            if (_attackKeys == null) return;
            foreach (var key in _attackKeys)
            {
                if (tok.IsCancellationRequested) return;
                if (string.IsNullOrWhiteSpace(key)) continue;
                InputSender.SendAttackAction(h, key, x, y);
                if (_keyDelay > 0) Sleep(tok, _keyDelay);
            }
        }

        // Mob centres in SCREEN coords. Prefer the neural net; fall back to
        // motion / templates (those return at most one point).
        private List<Point> FindMobs(IntPtr h)
        {
            if (UseYolo && Yolo != null && Yolo.IsLoaded)
            {
                var pts = Yolo.DetectScreenPoints(h);
                if (pts != null && pts.Count > 0) return pts;
                // YOLO found nothing this frame → fall through to motion/templates.
            }
            var list = new List<Point>();
            if (Detector != null)
            {
                Point? p = null;
                if (UseMotion) p = Detector.FindByMotionScreenPoint(h);
                if (!p.HasValue && UseTemplates && Detector.Count > 0) p = Detector.FindBestScreenPoint(h);
                if (p.HasValue) list.Add(p.Value);
            }
            return list;
        }

        // Nearest point to (x,y) within maxR and at least minR away (minR rejects
        // the player at screen centre during Acquire; pass 0 to disable).
        private static Point? NearestWithin(List<Point> pts, int x, int y, double maxR, double minR)
        {
            double best = double.MaxValue;
            Point bp = Point.Empty;
            bool found = false;
            foreach (var p in pts)
            {
                double dx = p.X - x, dy = p.Y - y;
                double d = Math.Sqrt(dx * dx + dy * dy);
                if (d >= minR && d <= maxR && d < best) { best = d; bp = p; found = true; }
            }
            if (found) return bp;
            return null;
        }

        private static void SaveDebugCapture(IntPtr h)
        {
            try
            {
                using (var bmp = ScreenCapture.CaptureWindow(h))
                {
                    if (bmp == null) { Log("DebugCapture: bmp==null"); return; }
                    string path = System.IO.Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory, "cap_debug.jpg");
                    bmp.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
                    Log("DebugCapture saved " + bmp.Width + "x" + bmp.Height + " -> cap_debug.jpg");
                }
            }
            catch (Exception ex) { Log("DebugCapture EX: " + ex.Message); }
        }

        // Interruptible sleep that returns early when the loop is cancelled.
        private static void Sleep(CancellationToken tok, int ms)
        {
            try { tok.WaitHandle.WaitOne(ms); } catch { }
        }

        // Saves one JPEG per engagement for future YOLO training. Capped so a
        // forgotten checkbox doesn't fill the disk.
        private const int MaxDatasetShots = 500;
        private static int _datasetCount = -1;

        private static void SaveDatasetShot(IntPtr hwnd, string dir)
        {
            try
            {
                if (_datasetCount < 0)
                {
                    System.IO.Directory.CreateDirectory(dir);
                    _datasetCount = System.IO.Directory.GetFiles(dir, "*.jpg").Length;
                }
                if (_datasetCount >= MaxDatasetShots) return;

                using (var bmp = ScreenCapture.CaptureWindow(hwnd))
                {
                    if (bmp == null) return;
                    string path = System.IO.Path.Combine(dir,
                        DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + ".jpg");
                    bmp.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
                    _datasetCount++;
                }
            }
            catch { }
        }
    }
}
