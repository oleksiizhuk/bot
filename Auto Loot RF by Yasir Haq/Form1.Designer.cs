namespace Auto_Loot_RF_by_Yasir_Haq
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.panelHeader  = new System.Windows.Forms.Panel();
            this.panelWindow  = new System.Windows.Forms.Panel();
            this.panelKeys    = new System.Windows.Forms.Panel();
            this.panelTiming  = new System.Windows.Forms.Panel();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.panelStatus  = new System.Windows.Forms.Panel();

            this.labelTitle    = new System.Windows.Forms.Label();
            this.labelSubtitle = new System.Windows.Forms.Label();

            this.labelKeysSec     = new System.Windows.Forms.Label();
            this.labelTargetCoords  = new System.Windows.Forms.Label();
            this.labelCoordX        = new System.Windows.Forms.Label();
            this.numericTargetX     = new System.Windows.Forms.NumericUpDown();
            this.labelCoordY        = new System.Windows.Forms.Label();
            this.numericTargetY     = new System.Windows.Forms.NumericUpDown();
            this.buttonPickCoords   = new System.Windows.Forms.Button();
            this.labelAttackKey   = new System.Windows.Forms.Label();
            this.textBoxAttack1   = new System.Windows.Forms.TextBox();
            this.textBoxAttack2   = new System.Windows.Forms.TextBox();
            this.textBoxAttack3   = new System.Windows.Forms.TextBox();
            this.textBoxAttack4   = new System.Windows.Forms.TextBox();
            this.textBoxAttack5   = new System.Windows.Forms.TextBox();
            this.labelKeyDelay    = new System.Windows.Forms.Label();
            this.numericKeyDelay  = new System.Windows.Forms.NumericUpDown();
            this.labelLootKey     = new System.Windows.Forms.Label();
            this.textBoxLootKey   = new System.Windows.Forms.TextBox();

            this.labelTimingSec  = new System.Windows.Forms.Label();
            this.labelKillTime   = new System.Windows.Forms.Label();
            this.numericKillTime = new System.Windows.Forms.NumericUpDown();
            this.labelKillUnit   = new System.Windows.Forms.Label();
            this.labelLootTime   = new System.Windows.Forms.Label();
            this.numericLootTime = new System.Windows.Forms.NumericUpDown();
            this.labelLootUnit   = new System.Windows.Forms.Label();

            this.buttonLoot     = new System.Windows.Forms.Button();
            this.buttonKillLoot = new System.Windows.Forms.Button();

            this.labelStatus = new System.Windows.Forms.Label();

            this.labelWindowSec       = new System.Windows.Forms.Label();
            this.buttonRefreshWindows = new System.Windows.Forms.Button();

            this.checkBoxWnd1      = new System.Windows.Forms.CheckBox();
            this.comboBoxWindow    = new System.Windows.Forms.ComboBox();
            this.buttonPickWindow  = new System.Windows.Forms.Button();

            this.checkBoxWnd2      = new System.Windows.Forms.CheckBox();
            this.comboBoxWindow2   = new System.Windows.Forms.ComboBox();
            this.buttonPickWindow2 = new System.Windows.Forms.Button();

            this.checkBoxWnd3      = new System.Windows.Forms.CheckBox();
            this.comboBoxWindow3   = new System.Windows.Forms.ComboBox();
            this.buttonPickWindow3 = new System.Windows.Forms.Button();

            this.panelDetect          = new System.Windows.Forms.Panel();
            this.labelDetectSec       = new System.Windows.Forms.Label();
            this.labelThreshold       = new System.Windows.Forms.Label();
            this.numericThreshold     = new System.Windows.Forms.NumericUpDown();
            this.buttonSnipTemplate   = new System.Windows.Forms.Button();
            this.buttonRemoveTemplate = new System.Windows.Forms.Button();
            this.checkBoxAutoTarget   = new System.Windows.Forms.CheckBox();
            this.listBoxTemplates     = new System.Windows.Forms.ListBox();
            this.pictureBoxTemplate   = new System.Windows.Forms.PictureBox();

            ((System.ComponentModel.ISupportInitialize)(this.numericKillTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericLootTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTargetX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTargetY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericKeyDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTemplate)).BeginInit();
            this.panelDetect.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelWindow.SuspendLayout();
            this.panelKeys.SuspendLayout();
            this.panelTiming.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelStatus.SuspendLayout();
            this.SuspendLayout();

            // ── panelHeader (y=0, h=62) ──────────────────────────────────
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Size     = new System.Drawing.Size(500, 62);
            this.panelHeader.Controls.Add(this.labelTitle);
            this.panelHeader.Controls.Add(this.labelSubtitle);

            this.labelTitle.AutoSize  = false;
            this.labelTitle.Location  = new System.Drawing.Point(16, 9);
            this.labelTitle.Size      = new System.Drawing.Size(340, 28);
            this.labelTitle.Text      = "RF AUTO LOOT";
            this.labelTitle.Font      = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);

            this.labelSubtitle.AutoSize  = false;
            this.labelSubtitle.Location  = new System.Drawing.Point(18, 38);
            this.labelSubtitle.Size      = new System.Drawing.Size(300, 16);
            this.labelSubtitle.Text      = "RF Online Automation Tool";
            this.labelSubtitle.Font      = new System.Drawing.Font("Segoe UI", 8.25F);

            // ── panelWindow (y=65, h=116) — three window rows ─────────────
            this.panelWindow.Location = new System.Drawing.Point(10, 65);
            this.panelWindow.Size     = new System.Drawing.Size(480, 116);
            this.panelWindow.Controls.Add(this.labelWindowSec);
            this.panelWindow.Controls.Add(this.buttonRefreshWindows);
            this.panelWindow.Controls.Add(this.checkBoxWnd1);
            this.panelWindow.Controls.Add(this.comboBoxWindow);
            this.panelWindow.Controls.Add(this.buttonPickWindow);
            this.panelWindow.Controls.Add(this.checkBoxWnd2);
            this.panelWindow.Controls.Add(this.comboBoxWindow2);
            this.panelWindow.Controls.Add(this.buttonPickWindow2);
            this.panelWindow.Controls.Add(this.checkBoxWnd3);
            this.panelWindow.Controls.Add(this.comboBoxWindow3);
            this.panelWindow.Controls.Add(this.buttonPickWindow3);

            this.labelWindowSec.AutoSize = true;
            this.labelWindowSec.Location = new System.Drawing.Point(12, 6);
            this.labelWindowSec.Text     = "TARGET WINDOWS";
            this.labelWindowSec.Font     = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);

            this.buttonRefreshWindows.Location  = new System.Drawing.Point(372, 3);
            this.buttonRefreshWindows.Size      = new System.Drawing.Size(96, 24);
            this.buttonRefreshWindows.Text      = "↻ Refresh";
            this.buttonRefreshWindows.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.buttonRefreshWindows.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRefreshWindows.Cursor    = System.Windows.Forms.Cursors.Hand;
            this.buttonRefreshWindows.Click    += new System.EventHandler(this.buttonRefreshWindows_Click);

            // Row 1
            this.checkBoxWnd1.AutoSize = true;
            this.checkBoxWnd1.Location = new System.Drawing.Point(12, 31);
            this.checkBoxWnd1.Text     = "#1";
            this.checkBoxWnd1.Checked  = true;
            this.checkBoxWnd1.Font     = new System.Drawing.Font("Segoe UI", 8.5F);

            this.comboBoxWindow.Location      = new System.Drawing.Point(50, 28);
            this.comboBoxWindow.Size          = new System.Drawing.Size(196, 22);
            this.comboBoxWindow.Font          = new System.Drawing.Font("Segoe UI", 8.5F);
            this.comboBoxWindow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWindow.FlatStyle     = System.Windows.Forms.FlatStyle.Flat;

            this.buttonPickWindow.Location  = new System.Drawing.Point(252, 27);
            this.buttonPickWindow.Size      = new System.Drawing.Size(52, 24);
            this.buttonPickWindow.Text      = "Pick";
            this.buttonPickWindow.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.buttonPickWindow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPickWindow.Cursor    = System.Windows.Forms.Cursors.Hand;
            this.buttonPickWindow.Click    += new System.EventHandler(this.buttonPickWindow_Click);

            // Row 2
            this.checkBoxWnd2.AutoSize = true;
            this.checkBoxWnd2.Location = new System.Drawing.Point(12, 59);
            this.checkBoxWnd2.Text     = "#2";
            this.checkBoxWnd2.Checked  = false;
            this.checkBoxWnd2.Font     = new System.Drawing.Font("Segoe UI", 8.5F);

            this.comboBoxWindow2.Location      = new System.Drawing.Point(50, 56);
            this.comboBoxWindow2.Size          = new System.Drawing.Size(196, 22);
            this.comboBoxWindow2.Font          = new System.Drawing.Font("Segoe UI", 8.5F);
            this.comboBoxWindow2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWindow2.FlatStyle     = System.Windows.Forms.FlatStyle.Flat;

            this.buttonPickWindow2.Location  = new System.Drawing.Point(252, 55);
            this.buttonPickWindow2.Size      = new System.Drawing.Size(52, 24);
            this.buttonPickWindow2.Text      = "Pick";
            this.buttonPickWindow2.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.buttonPickWindow2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPickWindow2.Cursor    = System.Windows.Forms.Cursors.Hand;
            this.buttonPickWindow2.Click    += new System.EventHandler(this.buttonPickWindow2_Click);

            // Row 3
            this.checkBoxWnd3.AutoSize = true;
            this.checkBoxWnd3.Location = new System.Drawing.Point(12, 87);
            this.checkBoxWnd3.Text     = "#3";
            this.checkBoxWnd3.Checked  = false;
            this.checkBoxWnd3.Font     = new System.Drawing.Font("Segoe UI", 8.5F);

            this.comboBoxWindow3.Location      = new System.Drawing.Point(50, 84);
            this.comboBoxWindow3.Size          = new System.Drawing.Size(196, 22);
            this.comboBoxWindow3.Font          = new System.Drawing.Font("Segoe UI", 8.5F);
            this.comboBoxWindow3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWindow3.FlatStyle     = System.Windows.Forms.FlatStyle.Flat;

            this.buttonPickWindow3.Location  = new System.Drawing.Point(252, 83);
            this.buttonPickWindow3.Size      = new System.Drawing.Size(52, 24);
            this.buttonPickWindow3.Text      = "Pick";
            this.buttonPickWindow3.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.buttonPickWindow3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPickWindow3.Cursor    = System.Windows.Forms.Cursors.Hand;
            this.buttonPickWindow3.Click    += new System.EventHandler(this.buttonPickWindow3_Click);

            // ── panelKeys (y=186, h=215) ─────────────────────────────────
            this.panelKeys.Location = new System.Drawing.Point(10, 186);
            this.panelKeys.Size     = new System.Drawing.Size(480, 215);
            this.panelKeys.Controls.Add(this.labelKeysSec);
            this.panelKeys.Controls.Add(this.labelTargetCoords);
            this.panelKeys.Controls.Add(this.labelCoordX);
            this.panelKeys.Controls.Add(this.numericTargetX);
            this.panelKeys.Controls.Add(this.labelCoordY);
            this.panelKeys.Controls.Add(this.numericTargetY);
            this.panelKeys.Controls.Add(this.buttonPickCoords);
            this.panelKeys.Controls.Add(this.labelAttackKey);
            this.panelKeys.Controls.Add(this.textBoxAttack1);
            this.panelKeys.Controls.Add(this.textBoxAttack2);
            this.panelKeys.Controls.Add(this.textBoxAttack3);
            this.panelKeys.Controls.Add(this.textBoxAttack4);
            this.panelKeys.Controls.Add(this.textBoxAttack5);
            this.panelKeys.Controls.Add(this.labelKeyDelay);
            this.panelKeys.Controls.Add(this.numericKeyDelay);
            this.panelKeys.Controls.Add(this.labelLootKey);
            this.panelKeys.Controls.Add(this.textBoxLootKey);

            this.labelKeysSec.AutoSize = true;
            this.labelKeysSec.Location = new System.Drawing.Point(12, 7);
            this.labelKeysSec.Text     = "KEY BINDINGS";
            this.labelKeysSec.Font     = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);

            this.labelTargetCoords.AutoSize = true;
            this.labelTargetCoords.Location = new System.Drawing.Point(12, 22);
            this.labelTargetCoords.Text     = "Click Coords";
            this.labelTargetCoords.Font     = new System.Drawing.Font("Segoe UI", 8F);

            this.labelCoordX.AutoSize = true;
            this.labelCoordX.Location = new System.Drawing.Point(12, 46);
            this.labelCoordX.Text     = "X";
            this.labelCoordX.Font     = new System.Drawing.Font("Segoe UI", 8F);

            this.numericTargetX.Location = new System.Drawing.Point(24, 42);
            this.numericTargetX.Size     = new System.Drawing.Size(52, 22);
            this.numericTargetX.Minimum  = 0;
            this.numericTargetX.Maximum  = 7680;
            this.numericTargetX.Value    = 960;
            this.numericTargetX.Font     = new System.Drawing.Font("Segoe UI", 9F);

            this.labelCoordY.AutoSize = true;
            this.labelCoordY.Location = new System.Drawing.Point(82, 46);
            this.labelCoordY.Text     = "Y";
            this.labelCoordY.Font     = new System.Drawing.Font("Segoe UI", 8F);

            this.numericTargetY.Location = new System.Drawing.Point(94, 42);
            this.numericTargetY.Size     = new System.Drawing.Size(52, 22);
            this.numericTargetY.Minimum  = 0;
            this.numericTargetY.Maximum  = 4320;
            this.numericTargetY.Value    = 540;
            this.numericTargetY.Font     = new System.Drawing.Font("Segoe UI", 9F);

            this.buttonPickCoords.Location  = new System.Drawing.Point(152, 40);
            this.buttonPickCoords.Size      = new System.Drawing.Size(44, 26);
            this.buttonPickCoords.Text      = "Pick";
            this.buttonPickCoords.Font      = new System.Drawing.Font("Segoe UI", 8F);
            this.buttonPickCoords.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPickCoords.Cursor    = System.Windows.Forms.Cursors.Hand;
            this.buttonPickCoords.Click    += new System.EventHandler(this.buttonPickCoords_Click);

            this.labelAttackKey.AutoSize = true;
            this.labelAttackKey.Location = new System.Drawing.Point(12, 166);
            this.labelAttackKey.Text     = "Attack Sequence  (RMB · LMB · F1 ...)";
            this.labelAttackKey.Font     = new System.Drawing.Font("Segoe UI", 8F);

            this.textBoxAttack1.Location  = new System.Drawing.Point(12,  184);
            this.textBoxAttack1.Size      = new System.Drawing.Size(50, 24);
            this.textBoxAttack1.Text      = "LMB";
            this.textBoxAttack1.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.textBoxAttack1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            this.textBoxAttack2.Location  = new System.Drawing.Point(68,  184);
            this.textBoxAttack2.Size      = new System.Drawing.Size(50, 24);
            this.textBoxAttack2.Text      = "F1";
            this.textBoxAttack2.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.textBoxAttack2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            this.textBoxAttack3.Location  = new System.Drawing.Point(124, 184);
            this.textBoxAttack3.Size      = new System.Drawing.Size(50, 24);
            this.textBoxAttack3.Text      = "F2";
            this.textBoxAttack3.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.textBoxAttack3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            this.textBoxAttack4.Location  = new System.Drawing.Point(180, 184);
            this.textBoxAttack4.Size      = new System.Drawing.Size(50, 24);
            this.textBoxAttack4.Text      = "";
            this.textBoxAttack4.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.textBoxAttack4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            this.textBoxAttack5.Location  = new System.Drawing.Point(236, 184);
            this.textBoxAttack5.Size      = new System.Drawing.Size(50, 24);
            this.textBoxAttack5.Text      = "";
            this.textBoxAttack5.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.textBoxAttack5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            this.labelKeyDelay.AutoSize = true;
            this.labelKeyDelay.Location = new System.Drawing.Point(296, 166);
            this.labelKeyDelay.Text     = "Delay (ms)";
            this.labelKeyDelay.Font     = new System.Drawing.Font("Segoe UI", 8F);
            this.numericKeyDelay.Location  = new System.Drawing.Point(296, 184);
            this.numericKeyDelay.Size      = new System.Drawing.Size(64, 24);
            this.numericKeyDelay.Minimum   = 0;
            this.numericKeyDelay.Maximum   = 2000;
            this.numericKeyDelay.Value     = 1500;
            this.numericKeyDelay.Increment = 50;
            this.numericKeyDelay.Font      = new System.Drawing.Font("Segoe UI", 9F);

            this.labelLootKey.AutoSize = true;
            this.labelLootKey.Location = new System.Drawing.Point(370, 166);
            this.labelLootKey.Text     = "Loot Key";
            this.labelLootKey.Font     = new System.Drawing.Font("Segoe UI", 8F);
            this.textBoxLootKey.Location  = new System.Drawing.Point(370, 184);
            this.textBoxLootKey.Size      = new System.Drawing.Size(90, 24);
            this.textBoxLootKey.Text      = "X";
            this.textBoxLootKey.Font      = new System.Drawing.Font("Segoe UI", 9.5F);
            this.textBoxLootKey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            // ── panelTiming (y=411, h=70) ────────────────────────────────
            this.panelTiming.Location = new System.Drawing.Point(10, 411);
            this.panelTiming.Size     = new System.Drawing.Size(480, 70);
            this.panelTiming.Controls.Add(this.labelTimingSec);
            this.panelTiming.Controls.Add(this.labelKillTime);
            this.panelTiming.Controls.Add(this.numericKillTime);
            this.panelTiming.Controls.Add(this.labelKillUnit);
            this.panelTiming.Controls.Add(this.labelLootTime);
            this.panelTiming.Controls.Add(this.numericLootTime);
            this.panelTiming.Controls.Add(this.labelLootUnit);

            this.labelTimingSec.AutoSize = true;
            this.labelTimingSec.Location = new System.Drawing.Point(12, 7);
            this.labelTimingSec.Text     = "TIMING";
            this.labelTimingSec.Font     = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);

            this.labelKillTime.AutoSize = true;
            this.labelKillTime.Location = new System.Drawing.Point(12, 30);
            this.labelKillTime.Text     = "Kill Time";
            this.labelKillTime.Font     = new System.Drawing.Font("Segoe UI", 8F);
            this.numericKillTime.Location = new System.Drawing.Point(12, 47);
            this.numericKillTime.Size     = new System.Drawing.Size(72, 24);
            this.numericKillTime.Minimum  = 1;
            this.numericKillTime.Maximum  = 60;
            this.numericKillTime.Value    = 10;
            this.numericKillTime.Font     = new System.Drawing.Font("Segoe UI", 9F);
            this.labelKillUnit.AutoSize = true;
            this.labelKillUnit.Location = new System.Drawing.Point(88, 50);
            this.labelKillUnit.Text     = "sec";
            this.labelKillUnit.Font     = new System.Drawing.Font("Segoe UI", 8.25F);

            this.labelLootTime.AutoSize = true;
            this.labelLootTime.Location = new System.Drawing.Point(240, 30);
            this.labelLootTime.Text     = "Loot Time";
            this.labelLootTime.Font     = new System.Drawing.Font("Segoe UI", 8F);
            this.numericLootTime.Location = new System.Drawing.Point(240, 47);
            this.numericLootTime.Size     = new System.Drawing.Size(72, 24);
            this.numericLootTime.Minimum  = 1;
            this.numericLootTime.Maximum  = 30;
            this.numericLootTime.Value    = 3;
            this.numericLootTime.Font     = new System.Drawing.Font("Segoe UI", 9F);
            this.labelLootUnit.AutoSize = true;
            this.labelLootUnit.Location = new System.Drawing.Point(316, 50);
            this.labelLootUnit.Text     = "sec";
            this.labelLootUnit.Font     = new System.Drawing.Font("Segoe UI", 8.25F);

            // ── panelButtons (y=491, h=68) ───────────────────────────────
            this.panelButtons.Location = new System.Drawing.Point(10, 491);
            this.panelButtons.Size     = new System.Drawing.Size(480, 68);
            this.panelButtons.Controls.Add(this.buttonLoot);
            this.panelButtons.Controls.Add(this.buttonKillLoot);

            this.buttonLoot.Location  = new System.Drawing.Point(12, 13);
            this.buttonLoot.Size      = new System.Drawing.Size(216, 42);
            this.buttonLoot.Text      = "  START LOOT";
            this.buttonLoot.Font      = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.buttonLoot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLoot.Cursor    = System.Windows.Forms.Cursors.Hand;
            this.buttonLoot.Click    += new System.EventHandler(this.buttonLoot_Click);

            this.buttonKillLoot.Location  = new System.Drawing.Point(252, 13);
            this.buttonKillLoot.Size      = new System.Drawing.Size(216, 42);
            this.buttonKillLoot.Text      = "  KILL + LOOT";
            this.buttonKillLoot.Font      = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.buttonKillLoot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonKillLoot.Cursor    = System.Windows.Forms.Cursors.Hand;
            this.buttonKillLoot.Click    += new System.EventHandler(this.buttonKillLoot_Click);

            // ── panelStatus (y=569, h=44) ────────────────────────────────
            this.panelStatus.Location = new System.Drawing.Point(0, 569);
            this.panelStatus.Size     = new System.Drawing.Size(500, 44);
            this.panelStatus.Controls.Add(this.labelStatus);

            this.labelStatus.AutoSize  = false;
            this.labelStatus.Location  = new System.Drawing.Point(16, 13);
            this.labelStatus.Size      = new System.Drawing.Size(468, 18);
            this.labelStatus.Text      = "●  Ready  —  press Start to begin";
            this.labelStatus.Font      = new System.Drawing.Font("Segoe UI", 9F);

            // ── Form ─────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize          = new System.Drawing.Size(500, 743);
            this.FormBorderStyle     = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview          = true;
            this.MaximizeBox         = false;
            this.Name                = "Form1";
            this.Text                = "RF Auto Loot";
            this.StartPosition       = System.Windows.Forms.FormStartPosition.CenterScreen;

            // ── panelDetect (y=616, h=122) ───────────────────────────────
            this.panelDetect.Location = new System.Drawing.Point(10, 616);
            this.panelDetect.Size     = new System.Drawing.Size(480, 122);
            this.panelDetect.Controls.Add(this.labelDetectSec);
            this.panelDetect.Controls.Add(this.labelThreshold);
            this.panelDetect.Controls.Add(this.numericThreshold);
            this.panelDetect.Controls.Add(this.buttonSnipTemplate);
            this.panelDetect.Controls.Add(this.buttonRemoveTemplate);
            this.panelDetect.Controls.Add(this.checkBoxAutoTarget);
            this.panelDetect.Controls.Add(this.listBoxTemplates);
            this.panelDetect.Controls.Add(this.pictureBoxTemplate);

            this.labelDetectSec.AutoSize = true;
            this.labelDetectSec.Location = new System.Drawing.Point(12, 7);
            this.labelDetectSec.Text     = "MOB DETECTION";
            this.labelDetectSec.Font     = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);

            this.labelThreshold.AutoSize = true;
            this.labelThreshold.Location = new System.Drawing.Point(330, 9);
            this.labelThreshold.Text     = "Threshold";
            this.labelThreshold.Font     = new System.Drawing.Font("Segoe UI", 8F);

            this.numericThreshold.Location      = new System.Drawing.Point(393, 5);
            this.numericThreshold.Size          = new System.Drawing.Size(72, 24);
            this.numericThreshold.DecimalPlaces = 2;
            this.numericThreshold.Increment     = 0.05M;
            this.numericThreshold.Minimum       = 0.50M;
            this.numericThreshold.Maximum       = 1.00M;
            this.numericThreshold.Value         = 0.80M;
            this.numericThreshold.Font          = new System.Drawing.Font("Segoe UI", 9F);
            this.numericThreshold.ValueChanged += new System.EventHandler(this.numericThreshold_ValueChanged);

            this.buttonSnipTemplate.Location  = new System.Drawing.Point(12, 26);
            this.buttonSnipTemplate.Size      = new System.Drawing.Size(138, 26);
            this.buttonSnipTemplate.Text      = "Snip Template";
            this.buttonSnipTemplate.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.buttonSnipTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSnipTemplate.Cursor    = System.Windows.Forms.Cursors.Hand;
            this.buttonSnipTemplate.Click    += new System.EventHandler(this.buttonSnipTemplate_Click);

            this.buttonRemoveTemplate.Location  = new System.Drawing.Point(156, 26);
            this.buttonRemoveTemplate.Size      = new System.Drawing.Size(80, 26);
            this.buttonRemoveTemplate.Text      = "Remove";
            this.buttonRemoveTemplate.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.buttonRemoveTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRemoveTemplate.Cursor    = System.Windows.Forms.Cursors.Hand;
            this.buttonRemoveTemplate.Click    += new System.EventHandler(this.buttonRemoveTemplate_Click);

            this.checkBoxAutoTarget.AutoSize = true;
            this.checkBoxAutoTarget.Location = new System.Drawing.Point(248, 30);
            this.checkBoxAutoTarget.Text     = "Auto-target";
            this.checkBoxAutoTarget.Font     = new System.Drawing.Font("Segoe UI", 8.5F);

            this.listBoxTemplates.Location              = new System.Drawing.Point(12, 58);
            this.listBoxTemplates.Size                  = new System.Drawing.Size(216, 58);
            this.listBoxTemplates.Font                  = new System.Drawing.Font("Segoe UI", 8.5F);
            this.listBoxTemplates.BorderStyle           = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxTemplates.SelectionMode         = System.Windows.Forms.SelectionMode.One;
            this.listBoxTemplates.SelectedIndexChanged += new System.EventHandler(this.listBoxTemplates_SelectedIndexChanged);
            this.listBoxTemplates.DoubleClick          += new System.EventHandler(this.listBoxTemplates_DoubleClick);

            this.pictureBoxTemplate.Location    = new System.Drawing.Point(234, 58);
            this.pictureBoxTemplate.Size        = new System.Drawing.Size(234, 58);
            this.pictureBoxTemplate.SizeMode    = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxTemplate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);

            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelWindow);
            this.Controls.Add(this.panelKeys);
            this.Controls.Add(this.panelTiming);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelStatus);
            this.Controls.Add(this.panelDetect);

            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelWindow.ResumeLayout(false);
            this.panelWindow.PerformLayout();
            this.panelKeys.ResumeLayout(false);
            this.panelKeys.PerformLayout();
            this.panelTiming.ResumeLayout(false);
            this.panelTiming.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelStatus.ResumeLayout(false);
            this.panelStatus.PerformLayout();
            this.panelDetect.ResumeLayout(false);
            this.panelDetect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericKillTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericLootTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTargetX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTargetY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericKeyDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTemplate)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel          panelHeader;
        private System.Windows.Forms.Panel          panelWindow;
        private System.Windows.Forms.Label          labelWindowSec;
        private System.Windows.Forms.Button         buttonRefreshWindows;
        private System.Windows.Forms.CheckBox       checkBoxWnd1;
        private System.Windows.Forms.ComboBox       comboBoxWindow;
        private System.Windows.Forms.Button         buttonPickWindow;
        private System.Windows.Forms.CheckBox       checkBoxWnd2;
        private System.Windows.Forms.ComboBox       comboBoxWindow2;
        private System.Windows.Forms.Button         buttonPickWindow2;
        private System.Windows.Forms.CheckBox       checkBoxWnd3;
        private System.Windows.Forms.ComboBox       comboBoxWindow3;
        private System.Windows.Forms.Button         buttonPickWindow3;
        private System.Windows.Forms.Panel          panelKeys;
        private System.Windows.Forms.Panel          panelTiming;
        private System.Windows.Forms.Panel          panelButtons;
        private System.Windows.Forms.Panel          panelStatus;
        private System.Windows.Forms.Label          labelTitle;
        private System.Windows.Forms.Label          labelSubtitle;
        private System.Windows.Forms.Label          labelKeysSec;
        private System.Windows.Forms.Label          labelTargetCoords;
        private System.Windows.Forms.Label          labelCoordX;
        private System.Windows.Forms.NumericUpDown  numericTargetX;
        private System.Windows.Forms.Label          labelCoordY;
        private System.Windows.Forms.NumericUpDown  numericTargetY;
        private System.Windows.Forms.Button         buttonPickCoords;
        private System.Windows.Forms.Label          labelAttackKey;
        private System.Windows.Forms.TextBox        textBoxAttack1;
        private System.Windows.Forms.TextBox        textBoxAttack2;
        private System.Windows.Forms.TextBox        textBoxAttack3;
        private System.Windows.Forms.TextBox        textBoxAttack4;
        private System.Windows.Forms.TextBox        textBoxAttack5;
        private System.Windows.Forms.Label          labelKeyDelay;
        private System.Windows.Forms.NumericUpDown  numericKeyDelay;
        private System.Windows.Forms.Label          labelLootKey;
        private System.Windows.Forms.TextBox        textBoxLootKey;
        private System.Windows.Forms.Label          labelTimingSec;
        private System.Windows.Forms.Label          labelKillTime;
        private System.Windows.Forms.NumericUpDown  numericKillTime;
        private System.Windows.Forms.Label          labelKillUnit;
        private System.Windows.Forms.Label          labelLootTime;
        private System.Windows.Forms.NumericUpDown  numericLootTime;
        private System.Windows.Forms.Label          labelLootUnit;
        private System.Windows.Forms.Button         buttonLoot;
        private System.Windows.Forms.Button         buttonKillLoot;
        private System.Windows.Forms.Label          labelStatus;
        private System.Windows.Forms.Panel          panelDetect;
        private System.Windows.Forms.Label          labelDetectSec;
        private System.Windows.Forms.Label          labelThreshold;
        private System.Windows.Forms.NumericUpDown  numericThreshold;
        private System.Windows.Forms.Button         buttonSnipTemplate;
        private System.Windows.Forms.Button         buttonRemoveTemplate;
        private System.Windows.Forms.CheckBox       checkBoxAutoTarget;
        private System.Windows.Forms.ListBox        listBoxTemplates;
        private System.Windows.Forms.PictureBox     pictureBoxTemplate;
    }
}
