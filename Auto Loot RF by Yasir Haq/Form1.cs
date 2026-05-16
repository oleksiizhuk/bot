using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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

        private readonly LootBot[]      _bots       = new[] { new LootBot(), new LootBot(), new LootBot() };
        private readonly MobDetector    _detector   = new MobDetector();
        private readonly List<IntPtr>[] _wndHandles = new[] { new List<IntPtr>(), new List<IntPtr>(), new List<IntPtr>() };
        private static readonly string TemplatesDir =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "templates");

        private ComboBox[] _wndCombos;
        private CheckBox[] _wndChecks;
        private Button[]   _pickBtns;

        private readonly System.Windows.Forms.Timer _pickTimer    = new System.Windows.Forms.Timer { Interval = 50 };
        private readonly System.Windows.Forms.Timer _pickWndTimer = new System.Windows.Forms.Timer { Interval = 50 };
        private bool   _picking;
        private bool   _pickReady;
        private bool   _pickingWindow;
        private bool   _pickWindowReady;
        private IntPtr _pickedHwnd;
        private int    _pickWindowIndex = -1;
        private Button _currentPickBtn;

        public Form1()
        {
            InitializeComponent();

            _wndCombos = new[] { comboBoxWindow, comboBoxWindow2, comboBoxWindow3 };
            _wndChecks = new[] { checkBoxWnd1, checkBoxWnd2, checkBoxWnd3 };
            _pickBtns  = new[] { buttonPickWindow, buttonPickWindow2, buttonPickWindow3 };

            _pickTimer.Tick    += OnPickTick;
            _pickWndTimer.Tick += OnPickWindowTick;
            try { ApplyTheme(); RefreshWindowList(); UpdateStatus(); LoadTemplates(); }
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

            foreach (var p in new Panel[] { panelWindow, panelKeys, panelTiming, panelButtons })
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

            foreach (var l in new Label[] { labelWindowSec, labelKeysSec, labelTimingSec })
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

            foreach (var cb in _wndCombos)
            {
                cb.BackColor = C_INPUT;
                cb.ForeColor = C_TEXT;
            }

            foreach (var chk in _wndChecks)
            {
                chk.ForeColor = C_DIM;
                chk.BackColor = Color.Transparent;
            }

            foreach (var btn in _pickBtns)
            {
                btn.BackColor                  = C_PANEL;
                btn.ForeColor                  = C_TEXT;
                btn.FlatAppearance.BorderColor = C_DIM;
            }

            buttonRefreshWindows.BackColor                  = C_PANEL;
            buttonRefreshWindows.ForeColor                  = C_TEXT;
            buttonRefreshWindows.FlatAppearance.BorderColor = C_DIM;

            buttonPickCoords.BackColor                  = C_PANEL;
            buttonPickCoords.ForeColor                  = C_TEXT;
            buttonPickCoords.FlatAppearance.BorderColor = C_DIM;

            buttonLoot.BackColor                  = C_GREEN;
            buttonLoot.ForeColor                  = Color.White;
            buttonLoot.FlatAppearance.BorderColor = C_GREEN;

            buttonKillLoot.BackColor                  = C_ORANGE;
            buttonKillLoot.ForeColor                  = Color.White;
            buttonKillLoot.FlatAppearance.BorderColor = C_ORANGE;

            labelStatus.BackColor = Color.Transparent;
            labelStatus.ForeColor = C_DIM;

            panelDetect.BackColor = C_PANEL;
            panelDetect.Paint += (s, e) =>
            {
                using (var b = new SolidBrush(C_ACCENT))
                    e.Graphics.FillRectangle(b, 0, 0, 3, panelDetect.Height);
            };
            labelDetectSec.ForeColor     = C_ACCENT; labelDetectSec.BackColor     = Color.Transparent;
            labelThreshold.ForeColor     = C_DIM;    labelThreshold.BackColor     = Color.Transparent;
            checkBoxAutoTarget.ForeColor = C_DIM;    checkBoxAutoTarget.BackColor = Color.Transparent;
            numericThreshold.BackColor   = C_INPUT;  numericThreshold.ForeColor   = C_TEXT;
            listBoxTemplates.BackColor   = C_INPUT;  listBoxTemplates.ForeColor   = C_TEXT;
            pictureBoxTemplate.BackColor = C_INPUT;
            buttonSnipTemplate.BackColor                  = C_PANEL;
            buttonSnipTemplate.ForeColor                  = C_TEXT;
            buttonSnipTemplate.FlatAppearance.BorderColor = C_DIM;
            buttonRemoveTemplate.BackColor                  = C_RED;
            buttonRemoveTemplate.ForeColor                  = Color.White;
            buttonRemoveTemplate.FlatAppearance.BorderColor = C_RED;
        }

        // ── Window list ───────────────────────────────────────────────────
        private void RefreshWindowList()
        {
            var hwnds  = new List<IntPtr>(GameFinder.FindAll());
            var titles = hwnds.ConvertAll(h => GameFinder.GetWindowTitle(h));

            for (int i = 0; i < 3; i++)
            {
                _wndHandles[i].Clear();
                _wndCombos[i].Items.Clear();
                _wndHandles[i].AddRange(hwnds);
                foreach (var t in titles) _wndCombos[i].Items.Add(t);
                if (_wndCombos[i].Items.Count > 0)
                    _wndCombos[i].SelectedIndex = Math.Min(i, _wndCombos[i].Items.Count - 1);
            }
        }

        private IntPtr GetSelectedHwnd(int index)
        {
            int idx = _wndCombos[index].SelectedIndex;
            if (idx < 0 || idx >= _wndHandles[index].Count) return IntPtr.Zero;
            return _wndHandles[index][idx];
        }

        private void buttonRefreshWindows_Click(object sender, EventArgs e)
        {
            RefreshWindowList();
        }

        // ── Pick window ───────────────────────────────────────────────────
        private void buttonPickWindow_Click(object sender, EventArgs e)  { BeginPickWindow(0, buttonPickWindow); }
        private void buttonPickWindow2_Click(object sender, EventArgs e) { BeginPickWindow(1, buttonPickWindow2); }
        private void buttonPickWindow3_Click(object sender, EventArgs e) { BeginPickWindow(2, buttonPickWindow3); }

        private void BeginPickWindow(int index, Button btn)
        {
            if (_pickingWindow)
            {
                StopPickingWindow(cancel: true);
                if (_pickWindowIndex == index) return;
            }
            _pickWindowIndex = index;
            _currentPickBtn  = btn;
            _pickingWindow   = true;
            _pickWindowReady = false;
            _pickedHwnd      = IntPtr.Zero;
            btn.Text                       = "Cancel";
            btn.BackColor                  = C_RED;
            btn.FlatAppearance.BorderColor = C_RED;
            _pickWndTimer.Start();
        }

        private void OnPickWindowTick(object sender, EventArgs e)
        {
            if (!_pickWindowReady)
            {
                _pickWindowReady = (Win32.GetAsyncKeyState(Win32.VK_LBUTTON) & 0x8000) == 0;
                return;
            }
            if ((Win32.GetAsyncKeyState(Win32.VK_LBUTTON) & 0x8000) != 0)
            {
                Win32.POINT pt;
                Win32.GetCursorPos(out pt);
                _pickedHwnd = Win32.WindowFromPoint(pt);
                StopPickingWindow(cancel: false);
            }
        }

        private void StopPickingWindow(bool cancel)
        {
            _pickWndTimer.Stop();
            _pickingWindow   = false;
            _pickWindowReady = false;

            var btn = _currentPickBtn;
            if (btn != null)
            {
                btn.Text                       = "Pick";
                btn.BackColor                  = C_PANEL;
                btn.FlatAppearance.BorderColor = C_DIM;
            }

            if (!cancel && _pickedHwnd != IntPtr.Zero && _pickWindowIndex >= 0)
            {
                string title = GameFinder.GetWindowTitle(_pickedHwnd);
                _wndHandles[_pickWindowIndex].Clear();
                _wndCombos[_pickWindowIndex].Items.Clear();
                _wndHandles[_pickWindowIndex].Add(_pickedHwnd);
                _wndCombos[_pickWindowIndex].Items.Add(title);
                _wndCombos[_pickWindowIndex].SelectedIndex = 0;
            }
            _pickWindowIndex = -1;
            _currentPickBtn  = null;
        }

        // ── Bot start / stop ──────────────────────────────────────────────
        private void buttonLoot_Click(object sender, EventArgs e)
        {
            if (Array.Exists(_bots, b => b.IsLootActive))
            {
                foreach (var b in _bots) b.Stop();
                UpdateUI();
                return;
            }

            bool anyStarted = false;
            for (int i = 0; i < 3; i++)
            {
                if (!_wndChecks[i].Checked) continue;
                var hwnd = GetSelectedHwnd(i);
                if (hwnd == IntPtr.Zero) continue;
                _bots[i].StartLoot(hwnd, textBoxLootKey.Text);
                anyStarted = true;
            }
            if (!anyStarted) ShowNoGameWarning();
            UpdateUI();
        }

        private void buttonKillLoot_Click(object sender, EventArgs e)
        {
            if (Array.Exists(_bots, b => b.IsKillLootActive))
            {
                foreach (var b in _bots) b.Stop();
                UpdateUI();
                return;
            }

            bool anyStarted = false;
            for (int i = 0; i < 3; i++)
            {
                if (!_wndChecks[i].Checked) continue;
                var hwnd = GetSelectedHwnd(i);
                if (hwnd == IntPtr.Zero) continue;
                _bots[i].Detector = checkBoxAutoTarget.Checked ? _detector : null;
                _bots[i].StartKillLoot(
                    hwnd,
                    new[] { textBoxAttack1.Text, textBoxAttack2.Text, textBoxAttack3.Text,
                             textBoxAttack4.Text, textBoxAttack5.Text },
                    textBoxLootKey.Text,
                    (int)numericKillTime.Value,
                    (int)numericLootTime.Value,
                    (int)numericTargetX.Value,
                    (int)numericTargetY.Value,
                    (int)numericKeyDelay.Value);
                anyStarted = true;
            }
            if (!anyStarted) ShowNoGameWarning();
            UpdateUI();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Application.Exit();
        }

        // ── Mob Detection ─────────────────────────────────────────────────
        private void LoadTemplates()
        {
            if (!Directory.Exists(TemplatesDir)) return;
            foreach (var file in Directory.GetFiles(TemplatesDir, "*.png"))
            {
                string name = Path.GetFileNameWithoutExtension(file);
                using (var bmp = new Bitmap(file))
                    _detector.AddTemplate(name, bmp);
                listBoxTemplates.Items.Add(name);
            }
        }

        private void buttonSnipTemplate_Click(object sender, EventArgs e)
        {
            using (var snip = new SnipOverlay())
            {
                if (snip.ShowDialog() != DialogResult.OK || snip.Result == null) return;

                Directory.CreateDirectory(TemplatesDir);
                string name = "mob_" + (_detector.Count + 1);
                string path = Path.Combine(TemplatesDir, name + ".png");
                snip.Result.Save(path, ImageFormat.Png);

                _detector.AddTemplate(name, snip.Result);
                listBoxTemplates.Items.Add(name);
            }
        }

        private void buttonRemoveTemplate_Click(object sender, EventArgs e)
        {
            if (listBoxTemplates.SelectedItem == null) return;
            string name = listBoxTemplates.SelectedItem.ToString();
            _detector.Remove(name);
            listBoxTemplates.Items.Remove(name);
            var old = pictureBoxTemplate.Image;
            pictureBoxTemplate.Image = null;
            if (old != null) old.Dispose();
            string path = Path.Combine(TemplatesDir, name + ".png");
            if (File.Exists(path)) File.Delete(path);
        }

        private void numericThreshold_ValueChanged(object sender, EventArgs e)
        {
            _detector.Threshold = (float)numericThreshold.Value;
        }

        private void listBoxTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxTemplates.SelectedItem == null) { pictureBoxTemplate.Image = null; return; }
            string name = listBoxTemplates.SelectedItem.ToString();
            string path = Path.Combine(TemplatesDir, name + ".png");
            if (!File.Exists(path)) { pictureBoxTemplate.Image = null; return; }
            try
            {
                Bitmap bmp;
                using (var tmp = new Bitmap(path))
                    bmp = new Bitmap(tmp);
                var old = pictureBoxTemplate.Image;
                pictureBoxTemplate.Image = bmp;
                if (old != null) old.Dispose();
            }
            catch { pictureBoxTemplate.Image = null; }
        }

        private void listBoxTemplates_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxTemplates.SelectedItem == null) return;
            string name = listBoxTemplates.SelectedItem.ToString();
            string path = Path.Combine(TemplatesDir, name + ".png");
            if (!File.Exists(path)) return;

            Bitmap bmp;
            try { using (var tmp = new Bitmap(path)) bmp = new Bitmap(tmp); } catch { return; }

            var popup = new Form
            {
                Text            = name,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox     = false,
                MinimizeBox     = false,
                StartPosition   = FormStartPosition.CenterScreen,
                ClientSize      = new Size(800, 600),
                BackColor       = C_BG
            };
            var pb = new PictureBox
            {
                Dock     = DockStyle.Fill,
                Image    = bmp,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            pb.Click += (s, a) => popup.Close();
            popup.Controls.Add(pb);
            popup.FormClosed += (s, a) => bmp.Dispose();
            popup.Show(this);
        }

        private void buttonPickCoords_Click(object sender, EventArgs e)
        {
            if (_picking) { StopPicking(); return; }

            _picking   = true;
            _pickReady = false;
            buttonPickCoords.Text                       = "Cancel";
            buttonPickCoords.BackColor                  = C_RED;
            buttonPickCoords.FlatAppearance.BorderColor = C_RED;
            _pickTimer.Start();
        }

        private void OnPickTick(object sender, EventArgs e)
        {
            var pos = Cursor.Position;
            numericTargetX.Value = Math.Max(numericTargetX.Minimum, Math.Min(numericTargetX.Maximum, pos.X));
            numericTargetY.Value = Math.Max(numericTargetY.Minimum, Math.Min(numericTargetY.Maximum, pos.Y));

            if (!_pickReady) { _pickReady = (Win32.GetAsyncKeyState(Win32.VK_LBUTTON) & 0x8000) == 0; return; }

            if ((Win32.GetAsyncKeyState(Win32.VK_LBUTTON) & 0x8000) != 0)
                StopPicking();
        }

        private void StopPicking()
        {
            _pickTimer.Stop();
            _picking   = false;
            _pickReady = false;
            buttonPickCoords.Text                       = "Pick";
            buttonPickCoords.BackColor                  = C_PANEL;
            buttonPickCoords.FlatAppearance.BorderColor = C_DIM;
        }

        // ── UI state ──────────────────────────────────────────────────────
        private void UpdateUI() { UpdateButtonStates(); UpdateStatus(); }

        private void ShowNoGameWarning()
        {
            MessageBox.Show("RF Online window not found.\nPlease start the game and select a window.",
                "RF Auto Loot", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void UpdateButtonStates()
        {
            bool anyLoot     = Array.Exists(_bots, b => b.IsLootActive);
            bool anyKillLoot = Array.Exists(_bots, b => b.IsKillLootActive);

            if (anyLoot)
            {
                buttonLoot.Text                       = "  STOP LOOT";
                buttonLoot.BackColor                  = C_RED;
                buttonLoot.FlatAppearance.BorderColor = C_RED;
            }
            else
            {
                buttonLoot.Text                       = "  START LOOT";
                buttonLoot.BackColor                  = C_GREEN;
                buttonLoot.FlatAppearance.BorderColor = C_GREEN;
            }

            if (anyKillLoot)
            {
                buttonKillLoot.Text                       = "  STOP";
                buttonKillLoot.BackColor                  = C_RED;
                buttonKillLoot.FlatAppearance.BorderColor = C_RED;
            }
            else
            {
                buttonKillLoot.Text                       = "  KILL + LOOT";
                buttonKillLoot.BackColor                  = C_ORANGE;
                buttonKillLoot.FlatAppearance.BorderColor = C_ORANGE;
            }
        }

        private void UpdateStatus()
        {
            int lootCount     = Array.FindAll(_bots, b => b.IsLootActive).Length;
            int killLootCount = Array.FindAll(_bots, b => b.IsKillLootActive).Length;

            if (lootCount > 0)
            {
                labelStatus.Text = lootCount > 1
                    ? "●  Looting active  (" + lootCount + " windows)"
                    : "●  Looting active";
                labelStatus.ForeColor = C_GREEN;
            }
            else if (killLootCount > 0)
            {
                labelStatus.Text = killLootCount > 1
                    ? "●  Kill + Loot active  (" + killLootCount + " windows)"
                    : "●  Kill + Loot active";
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
