using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Linq;
using System.Threading.Tasks;

namespace SteamDesktopAuth
{
    public partial class ConfirmForm : Form
    {
        /// <summary>
        /// General variables
        /// </summary>
        private List<Config.ConfirmationClass> confirmationList;
        public List<string> completedTrades;


        /// <summary>
        /// Imports
        /// </summary>
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="clist">List of confirmations</param>
        public ConfirmForm()
        {
            InitializeComponent();
            confirmationList = new List<Config.ConfirmationClass>();
            completedTrades = new List<string>();
        }


        /// <summary>
        /// Hides the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitButton_Click(object sender, EventArgs e)
        {
            Hide();
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
        /// Allows user to move form
        /// </summary>
        private void ConfirmForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }


        /// <summary>
        /// Refreshes the current trade that requires a confirmation
        /// </summary>
        /// <param name="list">List of trades</param>
        public void RefreshList(List<Config.ConfirmationClass> list)
        {
            confirmationList = list;
            if (!Visible)
            {
                confirmListBox.Items.Clear();
                foreach (var trade in list)
                {
                    if (!trade.done)
                    {
                        confirmListBox.Items.Add(string.Format("{0},{1}", trade.conf.Description, trade.conf.ID));
                    }
                }
            }
        }


        /// <summary>
        /// Refreshes all the existing trade confirmations pending
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void refreshButton_Click(object sender, EventArgs e)
        {
            confirmListBox.Items.Clear();
            foreach (var trade in confirmationList)
            {
                if (!trade.done)
                {
                    confirmListBox.Items.Add(string.Format("{0},{1}", trade.conf.Description, trade.conf.ID));
                }
            }
        }


        /// <summary>
        /// Confirms a trade from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void confirmButton_Click(object sender, EventArgs e)
        {
            HandleOffer(1);
        }


        /// <summary>
        /// Cancels a trade from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            HandleOffer(2);
        }


        /// <summary>
        /// Deals with a trade offer confirmation
        /// </summary>
        /// <param name="type">1 = confirm | 2 = cancel</param>
        /// <returns></returns>
        private void HandleOffer(int type)
        {
            var selected = confirmListBox.SelectedItem;
            if (selected != null)
            {
                string tradeId = ((string)selected).Split(',').LastOrDefault();
                if (!completedTrades.Contains(tradeId))
                {
                    var CC = confirmationList.First(o => o.conf.ID == tradeId);
                    switch (type)
                    {
                        case 1:
                            {
                                if (CC.account.AcceptConfirmation(CC.conf))
                                {
                                    confirmListBox.Items.Remove(selected);
                                    completedTrades.Add(CC.conf.ID);
                                }

                                break;
                            }
                        case 2:
                            {
                                if (CC.account.DenyConfirmation(CC.conf))
                                {
                                    confirmListBox.Items.Remove(selected);
                                    completedTrades.Add(CC.conf.ID);
                                }

                                break;
                            }
                    }
                }
            }
        }


        /// <summary>
        /// Hide/Show confirmations buttons depending on if an item has been selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void confirmListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            confirmButton.Visible = (confirmListBox.SelectedItem != null);
            cancelButton.Visible = (confirmListBox.SelectedItem != null);
        }
    }
}
