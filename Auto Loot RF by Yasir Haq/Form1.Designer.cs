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

            ((System.ComponentModel.ISupportInitialize)(this.numericKillTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericLootTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTargetX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTargetY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericKeyDelay)).BeginInit();
            this.panelHeader.SuspendLayout();
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

            // ── panelKeys (y=65, h=215) ──────────────────────────────────
            this.panelKeys.Location = new System.Drawing.Point(10, 65);
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

            // Click coords
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

            // Pick button ends at y≈66; Attack Sequence starts 100px below → y=166
            this.labelAttackKey.AutoSize = true;
            this.labelAttackKey.Location = new System.Drawing.Point(12, 166);
            this.labelAttackKey.Text     = "Attack Sequence  (RMB · LMB · F1 ...)";
            this.labelAttackKey.Font     = new System.Drawing.Font("Segoe UI", 8F);

            // 5 attack boxes: 50px wide, 6px gap → x=12,68,124,180,236
            this.textBoxAttack1.Location  = new System.Drawing.Point(12,  184);
            this.textBoxAttack1.Size      = new System.Drawing.Size(50, 24);
            this.textBoxAttack1.Text      = "RMB";
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
            this.numericKeyDelay.Location = new System.Drawing.Point(296, 184);
            this.numericKeyDelay.Size     = new System.Drawing.Size(64, 24);
            this.numericKeyDelay.Minimum  = 0;
            this.numericKeyDelay.Maximum  = 2000;
            this.numericKeyDelay.Value    = 0;
            this.numericKeyDelay.Increment = 50;
            this.numericKeyDelay.Font     = new System.Drawing.Font("Segoe UI", 9F);

            this.labelLootKey.AutoSize = true;
            this.labelLootKey.Location = new System.Drawing.Point(370, 166);
            this.labelLootKey.Text     = "Loot Key";
            this.labelLootKey.Font     = new System.Drawing.Font("Segoe UI", 8F);
            this.textBoxLootKey.Location  = new System.Drawing.Point(370, 184);
            this.textBoxLootKey.Size      = new System.Drawing.Size(90, 24);
            this.textBoxLootKey.Text      = "X";
            this.textBoxLootKey.Font      = new System.Drawing.Font("Segoe UI", 9.5F);
            this.textBoxLootKey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            // ── panelTiming (y=290, h=70) ────────────────────────────────
            this.panelTiming.Location = new System.Drawing.Point(10, 290);
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

            // ── panelButtons (y=370, h=68) ───────────────────────────────
            this.panelButtons.Location = new System.Drawing.Point(10, 370);
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

            // ── panelStatus (y=448, h=44) ────────────────────────────────
            this.panelStatus.Location = new System.Drawing.Point(0, 448);
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
            this.ClientSize          = new System.Drawing.Size(500, 492);
            this.FormBorderStyle     = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox         = false;
            this.Name                = "Form1";
            this.Text                = "RF Auto Loot";
            this.StartPosition       = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelKeys);
            this.Controls.Add(this.panelTiming);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelStatus);

            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelKeys.ResumeLayout(false);
            this.panelKeys.PerformLayout();
            this.panelTiming.ResumeLayout(false);
            this.panelTiming.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelStatus.ResumeLayout(false);
            this.panelStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericKillTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericLootTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTargetX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTargetY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericKeyDelay)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel          panelHeader;
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
    }
}
