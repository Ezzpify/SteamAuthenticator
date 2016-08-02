using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace SteamDesktopAuth
{
    public partial class NotloadedForm : Form
    {
        /// <summary>
        /// Adds the shadow boarders around the form
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }


        /// <summary>
        /// Form constructor
        /// </summary>
        /// <param name="list">List of accounts steamid that was not loaded</param>
        public NotloadedForm(List<string> list)
        {
            InitializeComponent();

            /*Set listbox*/
            accountListBox.Items.Clear();
            foreach (var account in list)
            {
                accountListBox.Items.Add(account);
            }
        }


        /// <summary>
        /// Close formm button
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Hide();
        }


        /// <summary>
        /// Opens the folder that contains our SGA save files
        /// </summary>
        private void btnFolder_Click(object sender, EventArgs e)
        {
            string directoryLoc = Path.Combine(Application.StartupPath, "SGAFiles");
            if (Directory.Exists(directoryLoc))
            {
                Process.Start("explorer.exe", directoryLoc);
            }
        }
    }
}
