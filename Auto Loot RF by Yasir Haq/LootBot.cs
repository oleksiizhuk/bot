using System;
using System.Windows.Forms;

namespace Auto_Loot_RF_by_Yasir_Haq
{
    internal sealed class LootBot
    {
        public bool IsLootActive     { get; private set; }
        public bool IsKillLootActive { get; private set; }

        private readonly Timer _timerLoot     = new Timer { Interval = 150 };
        private readonly Timer _timerKillLoot = new Timer { Interval = 150 };

        private IntPtr _hwnd;
        private string _attackKey;
        private string _lootKey;
        private int    _clickX, _clickY;
        private int    _phase, _phaseTicks, _attackTicks, _lootTicks;

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

        public void StartKillLoot(IntPtr hwnd, string attackKey, string lootKey,
                                   int killSec, int lootSec, int clickX, int clickY)
        {
            Stop();
            _hwnd        = hwnd;
            _attackKey   = attackKey;
            _lootKey     = lootKey;
            _clickX      = clickX;
            _clickY      = clickY;
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
                    InputSender.SendKey(_hwnd, _attackKey);
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
