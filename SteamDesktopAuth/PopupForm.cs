using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteamDesktopAuth
{
    public partial class PopupForm : Form
    {
        /// <summary>
        /// General variables
        /// </summary>
        private Point formStartupLocationPoint;
        public bool wasClosed;
        public bool wasAccepted;
        public bool wasCancelled;
        public string tradeId;



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="head">Will be displayed as title</param>
        /// <param name="tradeid">TradeID of this confirmation</param>
        /// <param name="account">Account that sent the trade</param>
        /// <param name="location">Location of form</param>
        public PopupForm(string head, string tradeid, string account, Point location)
        {
            InitializeComponent();

            tradeId = tradeid;
            Text = head;
            headerText.Text = head;
            bodyText.Text = string.Format("From account {0} | {1}", account, tradeid);
            formStartupLocationPoint = location;
        }


        /// <summary>
        /// Form load event
        /// Set location of form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopupForm_Load(object sender, EventArgs e)
        {
            Location = formStartupLocationPoint;
        }


        /// <summary>
        /// Fades out the form
        /// Will close form at 0 opacity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fadeTimer_Tick(object sender, EventArgs e)
        {
            FadeOut(20);
        }


        /// <summary>
        /// Function to fade out
        /// </summary>
        /// <param name="interval"></param>
        private async void FadeOut(int interval = 80)
        {
            //Object is fully visible. Fade it out
            while (Opacity > 0.0)
            {
                await Task.Delay(interval);
                Opacity -= 0.01;
            }
            Opacity = 0;
            wasClosed = true;
            Close();  
        }


        /// <summary>
        /// Exit button
        /// </summary>
        private void exitButton_Click(object sender, EventArgs e)
        {
            fadeTimer.Stop();
            wasClosed = true;
            Close();
        }


        /// <summary>
        /// Mouseenter event
        /// Will stop fading and bring back to 100 opaciity
        /// </summary>
        private void PopupForm_MouseEnter(object sender, EventArgs e)
        {
            Opacity = 100;
            fadeTimer.Stop();
        }


        /// <summary>
        /// Confirm button
        /// This will accept the trade offer
        /// </summary>
        private void confirmButton_Click(object sender, EventArgs e)
        {
            wasClosed = true;
            wasAccepted = true;
            Close();
        }


        /// <summary>
        /// Cancel button
        /// This will cancel the trade offer
        /// </summary>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            wasClosed = true;
            wasCancelled = true;
            Close();
        }
    }
}
