namespace SteamDesktopAuth
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.topBorder = new System.Windows.Forms.PictureBox();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.controlPanel = new System.Windows.Forms.Panel();
            this.miniButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.confirmationButton = new System.Windows.Forms.Label();
            this.githubLink = new System.Windows.Forms.Label();
            this.loadingPicture = new System.Windows.Forms.PictureBox();
            this.loginAuthCodeText = new System.Windows.Forms.TextBox();
            this.authCodeProgressbar = new System.Windows.Forms.ProgressBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.accountListBox = new System.Windows.Forms.ListBox();
            this.accountMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.unlinkAuthenticator = new System.Windows.Forms.ToolStripMenuItem();
            this.loginButton = new System.Windows.Forms.Label();
            this.authTimeRemainingTimer = new System.Windows.Forms.Timer(this.components);
            this.popupFormClear = new System.Windows.Forms.Timer(this.components);
            this.confirmTimer = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.updateChecker = new System.ComponentModel.BackgroundWorker();
            this.versionLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.topBorder)).BeginInit();
            this.headerPanel.SuspendLayout();
            this.controlPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadingPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.accountMenu.SuspendLayout();
            this.notifyMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // topBorder
            // 
            this.topBorder.BackColor = System.Drawing.Color.DodgerBlue;
            this.topBorder.Dock = System.Windows.Forms.DockStyle.Top;
            this.topBorder.Location = new System.Drawing.Point(0, 0);
            this.topBorder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.topBorder.Name = "topBorder";
            this.topBorder.Size = new System.Drawing.Size(300, 2);
            this.topBorder.TabIndex = 0;
            this.topBorder.TabStop = false;
            // 
            // headerPanel
            // 
            this.headerPanel.Controls.Add(this.label1);
            this.headerPanel.Controls.Add(this.controlPanel);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 2);
            this.headerPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(300, 64);
            this.headerPanel.TabIndex = 1;
            this.headerPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.headerPanel_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.label1.Location = new System.Drawing.Point(39, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(222, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "STEAM AUTHENTICATOR";
            // 
            // controlPanel
            // 
            this.controlPanel.Controls.Add(this.miniButton);
            this.controlPanel.Controls.Add(this.exitButton);
            this.controlPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlPanel.Location = new System.Drawing.Point(0, 0);
            this.controlPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(300, 25);
            this.controlPanel.TabIndex = 2;
            this.controlPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.controlPanel_MouseDown);
            // 
            // miniButton
            // 
            this.miniButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.miniButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.miniButton.FlatAppearance.BorderSize = 0;
            this.miniButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.miniButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.miniButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.miniButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.miniButton.Location = new System.Drawing.Point(250, 0);
            this.miniButton.Name = "miniButton";
            this.miniButton.Size = new System.Drawing.Size(25, 25);
            this.miniButton.TabIndex = 1;
            this.miniButton.Text = "_";
            this.miniButton.UseVisualStyleBackColor = true;
            this.miniButton.Click += new System.EventHandler(this.miniButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exitButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.exitButton.FlatAppearance.BorderSize = 0;
            this.exitButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.exitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.exitButton.Location = new System.Drawing.Point(275, 0);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(25, 25);
            this.exitButton.TabIndex = 0;
            this.exitButton.Text = "x";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // contentPanel
            // 
            this.contentPanel.Controls.Add(this.versionLabel);
            this.contentPanel.Controls.Add(this.confirmationButton);
            this.contentPanel.Controls.Add(this.githubLink);
            this.contentPanel.Controls.Add(this.loadingPicture);
            this.contentPanel.Controls.Add(this.loginAuthCodeText);
            this.contentPanel.Controls.Add(this.authCodeProgressbar);
            this.contentPanel.Controls.Add(this.pictureBox1);
            this.contentPanel.Controls.Add(this.accountListBox);
            this.contentPanel.Controls.Add(this.loginButton);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 66);
            this.contentPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(300, 318);
            this.contentPanel.TabIndex = 2;
            this.contentPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.contentPanel_MouseDown);
            // 
            // confirmationButton
            // 
            this.confirmationButton.AutoSize = true;
            this.confirmationButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.confirmationButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmationButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.confirmationButton.Location = new System.Drawing.Point(60, 16);
            this.confirmationButton.Name = "confirmationButton";
            this.confirmationButton.Size = new System.Drawing.Size(180, 21);
            this.confirmationButton.TabIndex = 11;
            this.confirmationButton.Text = "OPEN CONFIRMATIONS";
            this.confirmationButton.Click += new System.EventHandler(this.confirmationButton_Click);
            // 
            // githubLink
            // 
            this.githubLink.AutoSize = true;
            this.githubLink.Cursor = System.Windows.Forms.Cursors.Hand;
            this.githubLink.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.githubLink.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.githubLink.Location = new System.Drawing.Point(183, 296);
            this.githubLink.Name = "githubLink";
            this.githubLink.Size = new System.Drawing.Size(105, 13);
            this.githubLink.TabIndex = 10;
            this.githubLink.Text = "github.com/ezzpify";
            this.githubLink.Click += new System.EventHandler(this.githubLink_Click);
            // 
            // loadingPicture
            // 
            this.loadingPicture.Image = ((System.Drawing.Image)(resources.GetObject("loadingPicture.Image")));
            this.loadingPicture.Location = new System.Drawing.Point(136, 297);
            this.loadingPicture.Name = "loadingPicture";
            this.loadingPicture.Size = new System.Drawing.Size(30, 12);
            this.loadingPicture.TabIndex = 9;
            this.loadingPicture.TabStop = false;
            this.loadingPicture.Visible = false;
            // 
            // loginAuthCodeText
            // 
            this.loginAuthCodeText.BackColor = System.Drawing.Color.White;
            this.loginAuthCodeText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.loginAuthCodeText.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginAuthCodeText.Location = new System.Drawing.Point(44, 82);
            this.loginAuthCodeText.Name = "loginAuthCodeText";
            this.loginAuthCodeText.ReadOnly = true;
            this.loginAuthCodeText.Size = new System.Drawing.Size(210, 28);
            this.loginAuthCodeText.TabIndex = 7;
            this.loginAuthCodeText.Text = "C52GY";
            this.loginAuthCodeText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.loginAuthCodeText.Visible = false;
            // 
            // authCodeProgressbar
            // 
            this.authCodeProgressbar.Location = new System.Drawing.Point(44, 89);
            this.authCodeProgressbar.Maximum = 30;
            this.authCodeProgressbar.Name = "authCodeProgressbar";
            this.authCodeProgressbar.Size = new System.Drawing.Size(210, 23);
            this.authCodeProgressbar.TabIndex = 8;
            this.authCodeProgressbar.Value = 30;
            this.authCodeProgressbar.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pictureBox1.Location = new System.Drawing.Point(44, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(210, 1);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // accountListBox
            // 
            this.accountListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.accountListBox.ContextMenuStrip = this.accountMenu;
            this.accountListBox.FormattingEnabled = true;
            this.accountListBox.IntegralHeight = false;
            this.accountListBox.ItemHeight = 17;
            this.accountListBox.Location = new System.Drawing.Point(12, 140);
            this.accountListBox.Name = "accountListBox";
            this.accountListBox.Size = new System.Drawing.Size(276, 140);
            this.accountListBox.TabIndex = 5;
            this.accountListBox.SelectedIndexChanged += new System.EventHandler(this.accountListBox_SelectedIndexChanged);
            this.accountListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.accountListBox_MouseDown);
            // 
            // accountMenu
            // 
            this.accountMenu.BackColor = System.Drawing.Color.White;
            this.accountMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.unlinkAuthenticator});
            this.accountMenu.Name = "accountMenu";
            this.accountMenu.ShowImageMargin = false;
            this.accountMenu.Size = new System.Drawing.Size(152, 26);
            this.accountMenu.Opening += new System.ComponentModel.CancelEventHandler(this.accountMenu_Opening);
            // 
            // unlinkAuthenticator
            // 
            this.unlinkAuthenticator.Name = "unlinkAuthenticator";
            this.unlinkAuthenticator.Size = new System.Drawing.Size(151, 22);
            this.unlinkAuthenticator.Text = "UNLINK ACCOUNT";
            this.unlinkAuthenticator.Click += new System.EventHandler(this.unlinkAuthenticator_Click);
            // 
            // loginButton
            // 
            this.loginButton.AutoSize = true;
            this.loginButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loginButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.loginButton.Location = new System.Drawing.Point(89, 39);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(119, 21);
            this.loginButton.TabIndex = 3;
            this.loginButton.Text = "ADD ACCOUNT";
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // authTimeRemainingTimer
            // 
            this.authTimeRemainingTimer.Enabled = true;
            this.authTimeRemainingTimer.Interval = 500;
            this.authTimeRemainingTimer.Tick += new System.EventHandler(this.authTimeRemainingTimer_Tick);
            // 
            // popupFormClear
            // 
            this.popupFormClear.Enabled = true;
            this.popupFormClear.Interval = 250;
            this.popupFormClear.Tick += new System.EventHandler(this.popupFormClear_Tick);
            // 
            // confirmTimer
            // 
            this.confirmTimer.Enabled = true;
            this.confirmTimer.Interval = 3000;
            this.confirmTimer.Tick += new System.EventHandler(this.confirmTimer_Tick);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipTitle = "Steam Authenticator";
            this.notifyIcon.ContextMenuStrip = this.notifyMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Steam Authenticator";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // notifyMenu
            // 
            this.notifyMenu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.notifyMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuExit});
            this.notifyMenu.Name = "notifyMenu";
            this.notifyMenu.Size = new System.Drawing.Size(100, 28);
            // 
            // menuExit
            // 
            this.menuExit.Image = ((System.Drawing.Image)(resources.GetObject("menuExit.Image")));
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(99, 24);
            this.menuExit.Text = "Exit";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // updateChecker
            // 
            this.updateChecker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.updateChecker_DoWork);
            this.updateChecker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.updateChecker_RunWorkerCompleted);
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.versionLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.versionLabel.Location = new System.Drawing.Point(12, 296);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(44, 13);
            this.versionLabel.TabIndex = 12;
            this.versionLabel.Text = "version";
            this.versionLabel.Click += new System.EventHandler(this.versionLabel_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(300, 384);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.topBorder);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.topBorder)).EndInit();
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.controlPanel.ResumeLayout(false);
            this.contentPanel.ResumeLayout(false);
            this.contentPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadingPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.accountMenu.ResumeLayout(false);
            this.notifyMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox topBorder;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Panel controlPanel;
        private System.Windows.Forms.Button miniButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label loginButton;
        private System.Windows.Forms.ListBox accountListBox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox loginAuthCodeText;
        private System.Windows.Forms.ProgressBar authCodeProgressbar;
        private System.Windows.Forms.Timer authTimeRemainingTimer;
        private System.Windows.Forms.PictureBox loadingPicture;
        private System.Windows.Forms.Label githubLink;
        private System.Windows.Forms.Timer popupFormClear;
        private System.Windows.Forms.ContextMenuStrip accountMenu;
        private System.Windows.Forms.ToolStripMenuItem unlinkAuthenticator;
        private System.Windows.Forms.Timer confirmTimer;
        private System.Windows.Forms.Label confirmationButton;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip notifyMenu;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.ComponentModel.BackgroundWorker updateChecker;
        private System.Windows.Forms.Label versionLabel;
    }
}

