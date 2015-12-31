namespace SteamDesktopAuth
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.topBorder = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Label();
            this.cbAutostart = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cbPassword = new System.Windows.Forms.CheckBox();
            this.cbUpdates = new System.Windows.Forms.CheckBox();
            this.cbStatistics = new System.Windows.Forms.CheckBox();
            this.lblInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.topBorder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // topBorder
            // 
            this.topBorder.BackColor = System.Drawing.Color.DodgerBlue;
            this.topBorder.Dock = System.Windows.Forms.DockStyle.Top;
            this.topBorder.Location = new System.Drawing.Point(0, 0);
            this.topBorder.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.topBorder.Name = "topBorder";
            this.topBorder.Size = new System.Drawing.Size(283, 2);
            this.topBorder.TabIndex = 2;
            this.topBorder.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.AutoSize = true;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.btnCancel.Location = new System.Drawing.Point(203, 169);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 21);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.AutoSize = true;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.btnSave.Location = new System.Drawing.Point(150, 169);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(47, 21);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "SAVE";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbAutostart
            // 
            this.cbAutostart.AutoSize = true;
            this.cbAutostart.Location = new System.Drawing.Point(23, 22);
            this.cbAutostart.Name = "cbAutostart";
            this.cbAutostart.Size = new System.Drawing.Size(138, 21);
            this.cbAutostart.TabIndex = 9;
            this.cbAutostart.Text = "Start with Windows";
            this.cbAutostart.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pictureBox1.Location = new System.Drawing.Point(17, 160);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(248, 1);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // cbPassword
            // 
            this.cbPassword.AutoSize = true;
            this.cbPassword.Location = new System.Drawing.Point(23, 76);
            this.cbPassword.Name = "cbPassword";
            this.cbPassword.Size = new System.Drawing.Size(129, 21);
            this.cbPassword.TabIndex = 11;
            this.cbPassword.Text = "Ask for password";
            this.cbPassword.UseVisualStyleBackColor = true;
            // 
            // cbUpdates
            // 
            this.cbUpdates.AutoSize = true;
            this.cbUpdates.Location = new System.Drawing.Point(23, 49);
            this.cbUpdates.Name = "cbUpdates";
            this.cbUpdates.Size = new System.Drawing.Size(133, 21);
            this.cbUpdates.TabIndex = 12;
            this.cbUpdates.Text = "Check for updates";
            this.cbUpdates.UseVisualStyleBackColor = true;
            // 
            // cbStatistics
            // 
            this.cbStatistics.AutoSize = true;
            this.cbStatistics.Location = new System.Drawing.Point(23, 103);
            this.cbStatistics.Name = "cbStatistics";
            this.cbStatistics.Size = new System.Drawing.Size(232, 21);
            this.cbStatistics.TabIndex = 13;
            this.cbStatistics.Text = "Send app statistics to casperbod.in";
            this.cbStatistics.UseVisualStyleBackColor = true;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblInfo.Location = new System.Drawing.Point(52, 127);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(81, 17);
            this.lblInfo.TabIndex = 14;
            this.lblInfo.Text = "What is this?";
            this.lblInfo.Click += new System.EventHandler(this.lblInfo_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(283, 199);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.cbStatistics);
            this.Controls.Add(this.cbUpdates);
            this.Controls.Add(this.cbPassword);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cbAutostart);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.topBorder);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SA Settings";
            ((System.ComponentModel.ISupportInitialize)(this.topBorder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox topBorder;
        private System.Windows.Forms.Label btnCancel;
        private System.Windows.Forms.Label btnSave;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.CheckBox cbAutostart;
        public System.Windows.Forms.CheckBox cbPassword;
        public System.Windows.Forms.CheckBox cbUpdates;
        public System.Windows.Forms.CheckBox cbStatistics;
        private System.Windows.Forms.Label lblInfo;
    }
}