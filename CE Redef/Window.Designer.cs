
namespace CE_Redef
{
    partial class Window
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ceDirButton = new System.Windows.Forms.Button();
            this.ceDirTB = new System.Windows.Forms.TextBox();
            this.backupPathButton = new System.Windows.Forms.Button();
            this.backupPathTB = new System.Windows.Forms.TextBox();
            this.backupButton = new System.Windows.Forms.RadioButton();
            this.restoreButton = new System.Windows.Forms.RadioButton();
            this.redefineButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.warmupTB = new System.Windows.Forms.TextBox();
            this.meleeCooldownTB = new System.Windows.Forms.TextBox();
            this.rangedCooldownTB = new System.Windows.Forms.TextBox();
            this.reloadTimeTB = new System.Windows.Forms.TextBox();
            this.reloadSpeedTB = new System.Windows.Forms.TextBox();
            this.wornBulkTB = new System.Windows.Forms.TextBox();
            this.carryBulkTB = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bulkTB = new System.Windows.Forms.TextBox();
            this.runButton = new System.Windows.Forms.Button();
            this.progressBar = new CustomProgressBar();
            this.updateForm = new System.Windows.Forms.Timer(this.components);
            this.execute = new System.ComponentModel.BackgroundWorker();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // ceDirButton
            // 
            this.ceDirButton.ForeColor = System.Drawing.Color.Black;
            this.ceDirButton.Location = new System.Drawing.Point(5, 5);
            this.ceDirButton.Name = "ceDirButton";
            this.ceDirButton.Size = new System.Drawing.Size(165, 23);
            this.ceDirButton.TabIndex = 1;
            this.ceDirButton.Text = "Combat Extended Dir";
            this.ceDirButton.UseVisualStyleBackColor = true;
            this.ceDirButton.Click += new System.EventHandler(this.CeDirButton_Click);
            // 
            // ceDirTB
            // 
            this.ceDirTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ceDirTB.Location = new System.Drawing.Point(176, 6);
            this.ceDirTB.Name = "ceDirTB";
            this.ceDirTB.Size = new System.Drawing.Size(590, 20);
            this.ceDirTB.TabIndex = 2;
            this.ceDirTB.TextChanged += new System.EventHandler(this.CeDirTB_TextChanged);
            // 
            // backupPathButton
            // 
            this.backupPathButton.ForeColor = System.Drawing.Color.Black;
            this.backupPathButton.Location = new System.Drawing.Point(5, 34);
            this.backupPathButton.Name = "backupPathButton";
            this.backupPathButton.Size = new System.Drawing.Size(165, 23);
            this.backupPathButton.TabIndex = 3;
            this.backupPathButton.Text = "Backup Path";
            this.backupPathButton.UseVisualStyleBackColor = true;
            this.backupPathButton.Click += new System.EventHandler(this.BackupPathButton_Click);
            // 
            // backupPathTB
            // 
            this.backupPathTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.backupPathTB.Location = new System.Drawing.Point(176, 35);
            this.backupPathTB.Name = "backupPathTB";
            this.backupPathTB.Size = new System.Drawing.Size(590, 20);
            this.backupPathTB.TabIndex = 4;
            this.backupPathTB.TextChanged += new System.EventHandler(this.BackupPathTB_TextChanged);
            // 
            // backupButton
            // 
            this.backupButton.AutoSize = true;
            this.backupButton.Location = new System.Drawing.Point(22, 18);
            this.backupButton.Name = "backupButton";
            this.backupButton.Size = new System.Drawing.Size(62, 17);
            this.backupButton.TabIndex = 5;
            this.backupButton.TabStop = true;
            this.backupButton.Text = "Backup";
            this.backupButton.UseVisualStyleBackColor = true;
            this.backupButton.CheckedChanged += new System.EventHandler(this.BackupButton_CheckedChanged);
            // 
            // restoreButton
            // 
            this.restoreButton.AutoSize = true;
            this.restoreButton.Location = new System.Drawing.Point(145, 18);
            this.restoreButton.Name = "restoreButton";
            this.restoreButton.Size = new System.Drawing.Size(62, 17);
            this.restoreButton.TabIndex = 6;
            this.restoreButton.TabStop = true;
            this.restoreButton.Text = "Restore";
            this.restoreButton.UseVisualStyleBackColor = true;
            this.restoreButton.CheckedChanged += new System.EventHandler(this.RestoreButton_CheckedChanged);
            // 
            // redefineButton
            // 
            this.redefineButton.AutoSize = true;
            this.redefineButton.Location = new System.Drawing.Point(268, 18);
            this.redefineButton.Name = "redefineButton";
            this.redefineButton.Size = new System.Drawing.Size(68, 17);
            this.redefineButton.TabIndex = 7;
            this.redefineButton.TabStop = true;
            this.redefineButton.Text = "Redefine";
            this.redefineButton.UseVisualStyleBackColor = true;
            this.redefineButton.CheckedChanged += new System.EventHandler(this.RedefineButton_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Bulk multiplier";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(35)))), ((int)(((byte)(45)))));
            this.panel2.Controls.Add(this.ceDirTB);
            this.panel2.Controls.Add(this.ceDirButton);
            this.panel2.Controls.Add(this.backupPathButton);
            this.panel2.Controls.Add(this.backupPathTB);
            this.panel2.Location = new System.Drawing.Point(2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(2);
            this.panel2.Size = new System.Drawing.Size(772, 62);
            this.panel2.TabIndex = 11;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(83)))), ((int)(((byte)(100)))));
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Location = new System.Drawing.Point(12, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(776, 66);
            this.panel3.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(83)))), ((int)(((byte)(100)))));
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Location = new System.Drawing.Point(12, 84);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 53);
            this.panel1.TabIndex = 13;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(35)))), ((int)(((byte)(45)))));
            this.panel4.Controls.Add(this.backupButton);
            this.panel4.Controls.Add(this.redefineButton);
            this.panel4.Controls.Add(this.restoreButton);
            this.panel4.Location = new System.Drawing.Point(2, 2);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(2);
            this.panel4.Size = new System.Drawing.Size(772, 49);
            this.panel4.TabIndex = 11;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(83)))), ((int)(((byte)(100)))));
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Location = new System.Drawing.Point(12, 143);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(776, 232);
            this.panel5.TabIndex = 14;
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(35)))), ((int)(((byte)(45)))));
            this.panel6.Controls.Add(this.warmupTB);
            this.panel6.Controls.Add(this.meleeCooldownTB);
            this.panel6.Controls.Add(this.rangedCooldownTB);
            this.panel6.Controls.Add(this.reloadTimeTB);
            this.panel6.Controls.Add(this.reloadSpeedTB);
            this.panel6.Controls.Add(this.wornBulkTB);
            this.panel6.Controls.Add(this.carryBulkTB);
            this.panel6.Controls.Add(this.label9);
            this.panel6.Controls.Add(this.label7);
            this.panel6.Controls.Add(this.label6);
            this.panel6.Controls.Add(this.label5);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Controls.Add(this.label3);
            this.panel6.Controls.Add(this.label2);
            this.panel6.Controls.Add(this.bulkTB);
            this.panel6.Controls.Add(this.label1);
            this.panel6.Location = new System.Drawing.Point(2, 2);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(2);
            this.panel6.Size = new System.Drawing.Size(772, 228);
            this.panel6.TabIndex = 11;
            // 
            // warmupTB
            // 
            this.warmupTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.warmupTB.Location = new System.Drawing.Point(145, 202);
            this.warmupTB.Name = "warmupTB";
            this.warmupTB.Size = new System.Drawing.Size(621, 20);
            this.warmupTB.TabIndex = 24;
            this.warmupTB.TextChanged += new System.EventHandler(this.WarmupTB_TextChanged);
            // 
            // meleeCooldownTB
            // 
            this.meleeCooldownTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.meleeCooldownTB.Location = new System.Drawing.Point(145, 174);
            this.meleeCooldownTB.Name = "meleeCooldownTB";
            this.meleeCooldownTB.Size = new System.Drawing.Size(621, 20);
            this.meleeCooldownTB.TabIndex = 23;
            this.meleeCooldownTB.TextChanged += new System.EventHandler(this.MeleeCooldownTB_TextChanged);
            // 
            // rangedCooldownTB
            // 
            this.rangedCooldownTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rangedCooldownTB.Location = new System.Drawing.Point(145, 146);
            this.rangedCooldownTB.Name = "rangedCooldownTB";
            this.rangedCooldownTB.Size = new System.Drawing.Size(621, 20);
            this.rangedCooldownTB.TabIndex = 22;
            this.rangedCooldownTB.TextChanged += new System.EventHandler(this.RangedCooldownTB_TextChanged);
            // 
            // reloadTimeTB
            // 
            this.reloadTimeTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reloadTimeTB.Location = new System.Drawing.Point(145, 118);
            this.reloadTimeTB.Name = "reloadTimeTB";
            this.reloadTimeTB.Size = new System.Drawing.Size(621, 20);
            this.reloadTimeTB.TabIndex = 21;
            this.reloadTimeTB.TextChanged += new System.EventHandler(this.ReloadTimeTB_TextChanged);
            // 
            // reloadSpeedTB
            // 
            this.reloadSpeedTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reloadSpeedTB.Location = new System.Drawing.Point(145, 90);
            this.reloadSpeedTB.Name = "reloadSpeedTB";
            this.reloadSpeedTB.Size = new System.Drawing.Size(621, 20);
            this.reloadSpeedTB.TabIndex = 20;
            this.reloadSpeedTB.TextChanged += new System.EventHandler(this.ReloadSpeedTB_TextChanged);
            // 
            // wornBulkTB
            // 
            this.wornBulkTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wornBulkTB.Location = new System.Drawing.Point(145, 62);
            this.wornBulkTB.Name = "wornBulkTB";
            this.wornBulkTB.Size = new System.Drawing.Size(621, 20);
            this.wornBulkTB.TabIndex = 19;
            this.wornBulkTB.TextChanged += new System.EventHandler(this.WornBulkTB_TextChanged);
            // 
            // carryBulkTB
            // 
            this.carryBulkTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.carryBulkTB.Location = new System.Drawing.Point(145, 34);
            this.carryBulkTB.Name = "carryBulkTB";
            this.carryBulkTB.Size = new System.Drawing.Size(621, 20);
            this.carryBulkTB.TabIndex = 18;
            this.carryBulkTB.TextChanged += new System.EventHandler(this.CarryBulkTB_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 206);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Warmup multiplier";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 178);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Melee cooldown multiplier";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 150);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(137, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Ranged cooldown multiplier";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Reload speed multiplier";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Reload time multiplier";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Worn bulk multiplier";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Carry bulk multiplier";
            // 
            // bulkTB
            // 
            this.bulkTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bulkTB.Location = new System.Drawing.Point(145, 6);
            this.bulkTB.Name = "bulkTB";
            this.bulkTB.Size = new System.Drawing.Size(621, 20);
            this.bulkTB.TabIndex = 9;
            this.bulkTB.TextChanged += new System.EventHandler(this.BulkTB_TextChanged);
            // 
            // runButton
            // 
            this.runButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.runButton.Enabled = false;
            this.runButton.ForeColor = System.Drawing.Color.Black;
            this.runButton.Location = new System.Drawing.Point(11, 387);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(778, 23);
            this.runButton.TabIndex = 15;
            this.runButton.Text = "Please Wait...";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(12, 416);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(776, 22);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 13;
            // 
            // updateForm
            // 
            this.updateForm.Interval = 15;
            this.updateForm.Tick += new System.EventHandler(this.UpdateForm_Tick);
            // 
            // execute
            // 
            this.execute.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Execute_DoWork);
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(35)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.panel3);
            this.ForeColor = System.Drawing.Color.White;
            this.MinimumSize = new System.Drawing.Size(400, 483);
            this.Name = "Window";
            this.Text = "CE Redef v1.3";
            this.Load += new System.EventHandler(this.Window_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button ceDirButton;
        private System.Windows.Forms.TextBox ceDirTB;
        private System.Windows.Forms.Button backupPathButton;
        private System.Windows.Forms.TextBox backupPathTB;
        private System.Windows.Forms.RadioButton backupButton;
        private System.Windows.Forms.RadioButton restoreButton;
        private System.Windows.Forms.RadioButton redefineButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private CustomProgressBar progressBar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TextBox bulkTB;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.TextBox warmupTB;
        private System.Windows.Forms.TextBox meleeCooldownTB;
        private System.Windows.Forms.TextBox rangedCooldownTB;
        private System.Windows.Forms.TextBox reloadTimeTB;
        private System.Windows.Forms.TextBox reloadSpeedTB;
        private System.Windows.Forms.TextBox wornBulkTB;
        private System.Windows.Forms.TextBox carryBulkTB;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer updateForm;
        private System.ComponentModel.BackgroundWorker execute;
    }
}

