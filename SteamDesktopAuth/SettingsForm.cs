using System;
using System.Windows.Forms;

namespace SteamDesktopAuth
{
    public partial class SettingsForm : Form
    {
        /// <summary>
        /// Form constructor
        /// </summary>
        public SettingsForm()
        {
            InitializeComponent();
        }


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
        /// Closes the settings form
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Hide();
        }


        /// <summary>
        /// Saves the settings
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cbAutostart.Checked)
            {
                CRegistry.StartUp.AddApp();
            }
            else
            {
                CRegistry.StartUp.RemoveApp();
            }

            Properties.Settings.Default.bSendStatistics = cbStatistics.Checked;
            Properties.Settings.Default.bCheckForUpdates = cbUpdates.Checked;
            Properties.Settings.Default.bAskForPassword = cbPassword.Checked;
            Properties.Settings.Default.bStartMinimized = cbStartMinimized.Checked;
            Properties.Settings.Default.Save();
            Close();
        }


        /// <summary>
        /// Information label about the statistics
        /// </summary>
        private void lblInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This allows the app to send user statistics to my website.\n"
                + "This is simply to keep track of how many users this app has.\n"
                + "It does NOT send any information at all about your pc or your account.\n\n"
                + "It simply tells the website: 'Someone started the app'\n"
                + "For more information, check out the source code at:\nhttps://github.com/Ezzpify/SteamAuthenticator", 
                "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        /// <summary>
        /// Minimize at start checkbox
        /// Displays information how it will work is Ask for password is enabled
        /// </summary>
        private void cbStartMinimized_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPassword.Checked)
            {
                MessageBox.Show("If 'Ask for password' is enabled, the password window will still be shown at start.", "Information");
            }

            if (cbStartMinimized.Checked)
            {
                cbAutostart.Checked = true;
            }
        }
    }
}
