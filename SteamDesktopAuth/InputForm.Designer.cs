namespace SteamDesktopAuth
{
    partial class InputForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputForm));
            this.topBorder = new System.Windows.Forms.PictureBox();
            this.inputText = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Label();
            this.questionLabel = new System.Windows.Forms.Label();
            this.nopwButton = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.topBorder)).BeginInit();
            this.SuspendLayout();
            // 
            // topBorder
            // 
            this.topBorder.BackColor = System.Drawing.Color.DodgerBlue;
            this.topBorder.Dock = System.Windows.Forms.DockStyle.Top;
            this.topBorder.Location = new System.Drawing.Point(0, 0);
            this.topBorder.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.topBorder.Name = "topBorder";
            this.topBorder.Size = new System.Drawing.Size(331, 2);
            this.topBorder.TabIndex = 2;
            this.topBorder.TabStop = false;
            // 
            // inputText
            // 
            this.inputText.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputText.Location = new System.Drawing.Point(31, 89);
            this.inputText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inputText.Name = "inputText";
            this.inputText.Size = new System.Drawing.Size(271, 25);
            this.inputText.TabIndex = 3;
            this.inputText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.inputText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.inputText_KeyDown);
            // 
            // okButton
            // 
            this.okButton.AutoSize = true;
            this.okButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.okButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.okButton.Location = new System.Drawing.Point(176, 127);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(31, 21);
            this.okButton.TabIndex = 7;
            this.okButton.Text = "OK";
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.AutoSize = true;
            this.cancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.cancelButton.Location = new System.Drawing.Point(234, 127);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(68, 21);
            this.cancelButton.TabIndex = 8;
            this.cancelButton.Text = "CANCEL";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // questionLabel
            // 
            this.questionLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.questionLabel.Location = new System.Drawing.Point(28, 20);
            this.questionLabel.Name = "questionLabel";
            this.questionLabel.Size = new System.Drawing.Size(274, 65);
            this.questionLabel.TabIndex = 9;
            this.questionLabel.Text = "Text will magically appear here, also multiple lines. Fuck phreak xD.";
            // 
            // nopwButton
            // 
            this.nopwButton.AutoSize = true;
            this.nopwButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nopwButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nopwButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.nopwButton.Location = new System.Drawing.Point(27, 127);
            this.nopwButton.Name = "nopwButton";
            this.nopwButton.Size = new System.Drawing.Size(130, 21);
            this.nopwButton.TabIndex = 10;
            this.nopwButton.Text = "SKIP PASSWORD";
            this.nopwButton.Visible = false;
            this.nopwButton.Click += new System.EventHandler(this.nopwButton_Click);
            // 
            // InputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(331, 169);
            this.Controls.Add(this.nopwButton);
            this.Controls.Add(this.questionLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.inputText);
            this.Controls.Add(this.topBorder);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "InputForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InputForm";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.topBorder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox topBorder;
        private System.Windows.Forms.Label okButton;
        private System.Windows.Forms.Label cancelButton;
        private System.Windows.Forms.Label questionLabel;
        public System.Windows.Forms.TextBox inputText;
        private System.Windows.Forms.Label nopwButton;
    }
}