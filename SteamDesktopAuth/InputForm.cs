using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteamDesktopAuth
{
    public partial class InputForm : Form
    {
        /// <summary>
        /// General variables
        /// </summary>
        public bool inputCancelled;


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
            }
        }


        /// <summary>
        /// OK button, user decided to continue
        /// </summary>
        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// <summary>
        /// Cancel button, report cancel process
        /// </summary>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            inputCancelled = true;
            Close();
        }
    }
}
