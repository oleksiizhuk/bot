using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Auto_Loot_RF_by_Yasir_Haq
{
    internal sealed class LootBot
    {
        public bool IsLootActive     { get; private set; }
        public bool IsKillLootActive { get; private set; }

        private readonly Timer _timerLoot     = new Timer { Interval = 150 };
        private readonly Timer _timerKillLoot = new Timer { Interval = 150 };

        private IntPtr   _hwnd;
        private string[] _attackKeys;
        private string   _lootKey;
        private int    _clickX, _clickY, _keyDelay;
        private int    _phase, _phaseTicks, _attackTicks, _lootTicks;
        private volatile bool _attackRunning;

        public LootBot()
        {
            _timerLoot.Tick     += OnLootTick;
            _timerKillLoot.Tick += OnKillLootTick;
        }

        public void StartLoot(IntPtr hwnd, string lootKey)
        {
            Stop();
            _hwnd        = hwnd;
            _lootKey     = lootKey;
            IsLootActive = true;
            _timerLoot.Start();
        }

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
            _attackTicks = Math.Max(1, killSec * 1000 / 150);
            _lootTicks   = Math.Max(1, lootSec * 1000 / 150);
            _phase       = 0;
            _phaseTicks  = 0;
            IsKillLootActive = true;
            _timerKillLoot.Start();
        }

        public void Stop()
        {
            _timerLoot.Stop();
            _timerKillLoot.Stop();
            IsLootActive     = false;
            IsKillLootActive = false;
            _attackRunning   = false;
            _hwnd = IntPtr.Zero;
        }

        private void OnLootTick(object sender, EventArgs e)
        {
            InputSender.SendKey(_hwnd, _lootKey);
        }

        private void OnKillLootTick(object sender, EventArgs e)
        {
            switch (_phase)
            {
                case 0:
                    InputSender.Click(_clickX, _clickY);
                    _phaseTicks = 0;
                    _phase      = 1;
                    break;
                case 1:
                    if (!_attackRunning)
                    {
                        var hwnd   = _hwnd;
                        var keys   = _attackKeys;
                        var delay  = _keyDelay;
                        var cx     = _clickX;
                        var cy     = _clickY;
                        _attackRunning = true;
                        Task.Run(async () =>
                        {
                            try
                            {
                                foreach (var key in keys)
                                {
                                    if (string.IsNullOrWhiteSpace(key)) continue;
                                    InputSender.SendAttackAction(hwnd, key, cx, cy);
                                    if (delay > 0) await Task.Delay(delay);
                                }
                            }
                            finally { _attackRunning = false; }
                        });
                    }
                    if (++_phaseTicks >= _attackTicks) { _phaseTicks = 0; _phase = 2; }
                    break;
                case 2:
                    InputSender.SendKey(_hwnd, _lootKey);
                    if (++_phaseTicks >= _lootTicks)  { _phaseTicks = 0; _phase = 0; }
                    break;
            }
        }
    }
}
