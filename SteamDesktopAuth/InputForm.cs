using System;
using System.Windows.Forms;

namespace SteamDesktopAuth
{
    public partial class InputForm : Form
    {
        /// <summary>
        /// General variables
        /// </summary>
        public bool inputCancelled;
        public bool inputNoPassword;


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
        /// Constructor
        /// </summary>
        /// <param name="title">Title that will be displayed</param>
        public InputForm(string title, bool password = false)
        {
            InitializeComponent();
            questionLabel.Text = title;

            if(password)
            {
                inputText.PasswordChar = '*';
                nopwButton.Visible = true;
                cancelButton.Visible = false;
            }
        }


        /// <summary>
        /// OK button, user decided to continue
        /// </summary>
        private void okButton_Click(object sender, EventArgs e)
        {
            if(inputText.Text.Length > 0) Close();
        }


        /// <summary>
        /// Cancel button, report cancel process
        /// </summary>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            inputCancelled = true;
            Close();
        }


        /// <summary>
        /// Sets no password to true, means user skips password input
        /// Will only load save files which are not encrypted
        /// </summary>
        private void nopwButton_Click(object sender, EventArgs e)
        {
            if (nopwButton.Visible)
            {
                inputNoPassword = true;
                Close();
            }
            else
            {
                inputCancelled = true;
                Close();
            }
        }


        /// <summary>
        /// Allow for user to press enter/esc key to continue/skip
        /// </summary>
        private void inputText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (inputText.Text.Length > 0) Close();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                inputNoPassword = true;
                Close();
            }
        }
    }
}
