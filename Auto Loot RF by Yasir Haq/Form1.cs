using System;
using System.Drawing;
using System.Windows.Forms;

namespace Auto_Loot_RF_by_Yasir_Haq
{
    public partial class Form1 : Form
    {
        static readonly Color C_BG     = Color.FromArgb(15,  15,  26);
        static readonly Color C_PANEL  = Color.FromArgb(24,  24,  42);
        static readonly Color C_HEADER = Color.FromArgb(20,  20,  36);
        static readonly Color C_STATUS = Color.FromArgb(11,  11,  20);
        static readonly Color C_INPUT  = Color.FromArgb(10,  10,  22);
        static readonly Color C_ACCENT = Color.FromArgb(210, 155, 0);
        static readonly Color C_TEXT   = Color.FromArgb(220, 220, 240);
        static readonly Color C_DIM    = Color.FromArgb(110, 110, 145);
        static readonly Color C_GREEN  = Color.FromArgb(0,   175, 88);
        static readonly Color C_ORANGE = Color.FromArgb(185, 115, 0);
        static readonly Color C_RED    = Color.FromArgb(185, 42,  42);

        private readonly LootBot _bot = new LootBot();

        public Form1()
        {
            InitializeComponent();
            try { ApplyTheme(); UpdateStatus(); }
            catch (Exception ex)
            {
                MessageBox.Show("Startup error:\n\n" + ex.Message, "RF Auto Loot",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Theme ─────────────────────────────────────────────────────────
        private void ApplyTheme()
        {
            BackColor             = C_BG;
            panelHeader.BackColor = C_HEADER;
            panelStatus.BackColor = C_STATUS;

            foreach (var p in new Panel[] { panelKeys, panelTiming, panelButtons })
            {
                p.BackColor = C_PANEL;
                var cap = p;
                cap.Paint += (s, e) =>
                {
                    using (var b = new SolidBrush(C_ACCENT))
                        e.Graphics.FillRectangle(b, 0, 0, 3, cap.Height);
                };
            }

            panelHeader.Paint += (s, e) =>
            {
                using (var b = new SolidBrush(C_ACCENT))
                    e.Graphics.FillRectangle(b, 0, panelHeader.Height - 2, panelHeader.Width, 2);
            };

            labelTitle.ForeColor    = C_TEXT;  labelTitle.BackColor    = Color.Transparent;
            labelSubtitle.ForeColor = C_DIM;   labelSubtitle.BackColor = Color.Transparent;

            foreach (var l in new Label[] { labelKeysSec, labelTimingSec })
            {
                l.ForeColor = C_ACCENT;
                l.BackColor = Color.Transparent;
            }

            foreach (var l in new Label[] { labelTargetCoords, labelCoordX, labelCoordY,
                                             labelAttackKey, labelKeyDelay, labelLootKey,
                                             labelKillTime, labelKillUnit, labelLootTime, labelLootUnit })
            {
                l.ForeColor = C_DIM;
                l.BackColor = Color.Transparent;
            }

            foreach (var tb in new TextBox[] { textBoxAttack1, textBoxAttack2, textBoxAttack3,
                                               textBoxAttack4, textBoxAttack5, textBoxLootKey })
            {
                tb.BackColor   = C_INPUT;
                tb.ForeColor   = C_TEXT;
                tb.BorderStyle = BorderStyle.FixedSingle;
            }

            foreach (var nu in new NumericUpDown[] { numericKillTime, numericLootTime, numericTargetX, numericTargetY, numericKeyDelay })
            {
                nu.BackColor = C_INPUT;
                nu.ForeColor = C_TEXT;
            }

            buttonPickCoords.BackColor                    = C_PANEL;
            buttonPickCoords.ForeColor                    = C_TEXT;
            buttonPickCoords.FlatAppearance.BorderColor   = C_DIM;

            buttonLoot.BackColor                    = C_GREEN;
            buttonLoot.ForeColor                    = Color.White;
            buttonLoot.FlatAppearance.BorderColor   = C_GREEN;

            buttonKillLoot.BackColor                  = C_ORANGE;
            buttonKillLoot.ForeColor                  = Color.White;
            buttonKillLoot.FlatAppearance.BorderColor = C_ORANGE;

            labelStatus.BackColor = Color.Transparent;
            labelStatus.ForeColor = C_DIM;
        }

        // ── Button handlers ───────────────────────────────────────────────
        private void buttonLoot_Click(object sender, EventArgs e)
        {
            if (_bot.IsLootActive) { _bot.Stop(); UpdateUI(); return; }

            var hwnd = GameFinder.Find();
            if (hwnd == IntPtr.Zero) { ShowNoGameWarning(); return; }

            _bot.StartLoot(hwnd, textBoxLootKey.Text);
            UpdateUI();
        }

        private void buttonKillLoot_Click(object sender, EventArgs e)
        {
            if (_bot.IsKillLootActive) { _bot.Stop(); UpdateUI(); return; }

            var hwnd = GameFinder.Find();
            if (hwnd == IntPtr.Zero) { ShowNoGameWarning(); return; }

            _bot.StartKillLoot(
                hwnd,
                new[] { textBoxAttack1.Text, textBoxAttack2.Text, textBoxAttack3.Text,
                         textBoxAttack4.Text, textBoxAttack5.Text },
                textBoxLootKey.Text,
                (int)numericKillTime.Value,
                (int)numericLootTime.Value,
                (int)numericTargetX.Value,
                (int)numericTargetY.Value,
                (int)numericKeyDelay.Value);
            UpdateUI();
        }

        private void buttonPickCoords_Click(object sender, EventArgs e)
        {
            numericTargetX.Value = Cursor.Position.X;
            numericTargetY.Value = Cursor.Position.Y;
        }

        // ── UI state ──────────────────────────────────────────────────────
        private void UpdateUI() { UpdateButtonStates(); UpdateStatus(); }

        private void ShowNoGameWarning()
        {
            MessageBox.Show("RF Online window not found.\nPlease start the game first.",
                "RF Auto Loot", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void UpdateButtonStates()
        {
            if (_bot.IsLootActive)
            {
                buttonLoot.Text = "  STOP LOOT";
                buttonLoot.BackColor = C_RED;
                buttonLoot.FlatAppearance.BorderColor = C_RED;
            }
            else
            {
                buttonLoot.Text = "  START LOOT";
                buttonLoot.BackColor = C_GREEN;
                buttonLoot.FlatAppearance.BorderColor = C_GREEN;
            }

            if (_bot.IsKillLootActive)
            {
                buttonKillLoot.Text = "  STOP";
                buttonKillLoot.BackColor = C_RED;
                buttonKillLoot.FlatAppearance.BorderColor = C_RED;
            }
            else
            {
                buttonKillLoot.Text = "  KILL + LOOT";
                buttonKillLoot.BackColor = C_ORANGE;
                buttonKillLoot.FlatAppearance.BorderColor = C_ORANGE;
            }
        }

        private void UpdateStatus()
        {
            if (_bot.IsLootActive)
            {
                labelStatus.Text      = "●  Looting active";
                labelStatus.ForeColor = C_GREEN;
            }
            else if (_bot.IsKillLootActive)
            {
                labelStatus.Text      = "●  Kill + Loot active";
                labelStatus.ForeColor = C_GREEN;
            }
            else
            {
                labelStatus.Text      = "●  Ready  —  press Start to begin";
                labelStatus.ForeColor = C_DIM;
            }
        }
    }
}
