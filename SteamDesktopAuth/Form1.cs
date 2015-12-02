using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SteamAuth;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Media;
using System.Text;

namespace SteamDesktopAuth
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// General variables
        /// </summary>
        private ConfirmForm confirmForm;
        private SteamGuardAccount accountCurrent;
        private List<SteamGuardAccount> accountList;
        private List<Config.ConfirmationClass> confirmationList;
        private List<PopupForm> popupForms;

        private long steamTime;
        private long currentSteamChunk;

        private bool minimizedNotificationShown;


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
        public Form1()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Form load event
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            accountList         = new List<SteamGuardAccount>();
            popupForms          = new List<PopupForm>();
            confirmationList    = new List<Config.ConfirmationClass>();
            confirmForm         = new ConfirmForm();

            /*We'll ask for a password here*/
            /*The password will be used as the secret for encrypting the data we'll store along with a generated salt*/
            /*It will be asked for every time the application starts and cannot be recovered*/
            InputForm passwordForm = new InputForm("Submit password. If you are a new user, enter a new password (6-25 chars). It can not be recovered.", true);
            passwordForm.ShowDialog();

            if (passwordForm.inputCancelled)
            {
                MessageBox.Show("Need a password in order to proceed. Exiting...");
                Environment.Exit(1);
            }
            else
            {
                string password = passwordForm.inputText.Text;
                if (password.Length >= 6 && password.Length <= 25)
                {
                    Crypto.crySecret = password;
                    Crypto.crySalt = Encoding.ASCII.GetBytes("RandomNumberFour"); //Guaranteed to be random
                }
                else
                {
                    MessageBox.Show("Password is not between 6-25 chars. Exiting...");
                    Environment.Exit(1);
                }
            }

            /*Create folder that we'll store all save files in*/
            Directory.CreateDirectory(Path.Combine(Application.StartupPath, "SGAFiles"));
            loadAccounts();
        }


        /// <summary>
        /// Notifyicon for main foorm
        /// </summary>
        private void notifyIcon_Click(object sender, EventArgs e)
        {
            ShowInTaskbar = true;
            WindowState = FormWindowState.Normal;
        }


        /// <summary>
        /// Listbox items can now be selected by pressing any mouse button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accountListBox_MouseDown(object sender, MouseEventArgs e)
        {
            accountListBox.SelectedIndex = accountListBox.IndexFromPoint(e.X, e.Y);
        }


        /// <summary>
        /// Exit button to exit the application
        /// </summary>
        private void exitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }


        /// <summary>
        /// Minimize button to minimize the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miniButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;

            if (!minimizedNotificationShown)
            {
                minimizedNotificationShown = true;
                notifyIcon.ShowBalloonTip(600, "Steam Authenticator", "I'm still running down here!", ToolTipIcon.Info);
            }
        }


        /// <summary>
        /// Displays the Confirmation form
        /// </summary>
        private void confirmationButton_Click(object sender, EventArgs e)
        {
            confirmForm.Show();
        }


        /// <summary>
        /// Changes the color of a progress bar
        /// </summary>
        /// <param name="pBar">Progress bar to modift</param>
        /// <param name="state">New color (1 = GREEN | 2 = RED | 3 = YELLOW)</param>
        private void SetProgressBarColor(ProgressBar pBar, int state)
        {
            SendMessage(pBar.Handle, 1040, state, 0);
        }


        /// <summary>
        /// Allow movement of form
        /// </summary>
        private void contentPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }


        /// <summary>
        /// Allow movement of form
        /// </summary>
        private void headerPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }


        /// <summary>
        /// Allow movement of form
        /// </summary>
        private void controlPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }


        /// <summary>
        /// Login button
        /// </summary>
        private void loginButton_Click(object sender, EventArgs e)
        {
            IsLoading(true);

            LoginForm loginForm = new LoginForm(new Point(Location.X + 10, Location.Y + 80));
            loginForm.ShowDialog();

            IsLoading(false);
            loadAccounts();
        }


        /// <summary>
        /// Loads all account from file
        /// </summary>
        private void loadAccounts()
        {
            /*Reset current values*/
            accountCurrent = null;
            accountList.Clear();
            accountListBox.Items.Clear();

            /*Get accounts from save list and load them up*/
            accountList = FileHandler.GetAllAccounts();
            if(accountList.Count > 0)
            {
                foreach(SteamGuardAccount account in accountList)
                {
                    accountListBox.Items.Add(account.AccountName);
                }

                accountListBox.SelectedIndex = 0;
            }
        }


        /// <summary>
        /// Calculates the time remaining of the Two-Way auth code
        /// </summary>
        private void authTimeRemainingTimer_Tick(object sender, EventArgs e)
        {
            if (accountCurrent != null)
            {
                /*Only want to display these controls if we have an active current account*/
                loginAuthCodeText.Visible = true;
                authCodeProgressbar.Visible = true;

                steamTime = TimeAligner.GetSteamTime();
                currentSteamChunk = steamTime / 30L;

                int secondsUntilChange = (int)(steamTime - (currentSteamChunk * 30L));

                loadAccountInfo();
                authCodeProgressbar.Value = 30 - secondsUntilChange;

                /*Change progressbar color to red if time is close to running out*/
                if (authCodeProgressbar.Value < 6) { SetProgressBarColor(authCodeProgressbar, 2); }
                else { SetProgressBarColor(authCodeProgressbar, 1); }
            }
            else
            {
                authCodeProgressbar.Visible = false;
                loginAuthCodeText.Visible = false;
            }
        }


        /// <summary>
        /// Loads info about the current account for Two-Way auth code
        /// </summary>
        private void loadAccountInfo()
        {
            if(accountCurrent != null && steamTime != 0)
            {
                /*Fetch steamguard code for the account*/
                loginAuthCodeText.Text = accountCurrent.GenerateSteamGuardCodeForTime(steamTime);
            }
        }


        /// <summary>
        /// Shows/Hides a loading animation at the bottom of the screen
        /// </summary>
        /// <param name="b">True to show, False to hide</param>
        private void IsLoading(bool b)
        {
            loadingPicture.Visible = b;
        }
        

        /// <summary>
        /// Launches my GitHub profile in default browser
        /// </summary>
        private void githubLink_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/Ezzpify");
        }
        

        /// <summary>
        /// Shows a popup form at bottom right side of screen
        /// </summary>
        /// <param name="head">Title text</param>
        /// <param name="body">Body text</param>
        private void DoPopup(Config.ConfirmationClass CC)
        {
            /*Find a good position on screen*/
            Rectangle workingArea = Screen.GetWorkingArea(this);
            Point cPoint = new Point(workingArea.Right - 290, workingArea.Bottom - 110 - (popupForms.Count * 110));

            /*Show form and add to list*/
            PopupForm popupForm = new PopupForm(CC.conf.ConfirmationDescription, CC.conf.ConfirmationID, CC.account.AccountName, cPoint);
            popupForms.Add(popupForm);
            popupForm.Show();
        }


        /// <summary>
        /// Checks the responses of the popup forms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popupFormClear_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < popupForms.Count; i++)
            {
                PopupForm pf = popupForms[i];
                if (pf.wasAccepted)
                {
                    /*Get the trade from list of trades that matches trade id and accept offer*/
                    var trade = confirmationList.First(o => o.conf.ConfirmationID == pf.tradeId);
                    if (trade.account.AcceptConfirmation(trade.conf))
                    {
                        Console.Beep(800, 100);
                        trade.done = true;
                    }
                }
                else if (pf.wasCancelled)
                {
                    /*... decline offer*/
                    var trade = confirmationList.First(o => o.conf.ConfirmationID == pf.tradeId);
                    if (trade.account.DenyConfirmation(trade.conf))
                    {
                        Console.Beep(800, 100);
                        trade.done = true;
                    }
                }

                /*Check if it just closed*/
                /*User will have to approve the trade from main gui*/
                if (pf.wasClosed)
                {
                    popupForms.RemoveAt(i);
                }
            }
        }


        /// <summary>
        /// Changes the current account depending on which is selected in the listbox
        /// </summary>
        private void accountListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach(SteamGuardAccount account in accountList)
            {
                if(account.AccountName == (string)accountListBox.SelectedItem)
                {
                    accountCurrent = account;
                    loadAccountInfo();
                    break;
                }
            }
        }


        /// <summary>
        /// Disable menu opening if no item is selected
        /// </summary>
        private void accountMenu_Opening(object sender, CancelEventArgs e)
        {
            if (accountListBox.SelectedItem == null)
                e.Cancel = true;
        }


        /// <summary>
        /// Searches for new trade confirmations
        /// </summary>
        private void confirmTimer_Tick(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                for(int i = 0; i < accountList.Count; i++)
                {
                    try
                    {
                        /*Fetch all confirmations pending for current account*/
                        Confirmation[] conftemp = accountList[i].FetchConfirmations();
                        if (conftemp.Length > 0)
                        {
                            foreach (Confirmation conf in conftemp)
                            {
                                /*Go through all and set some cool info in new class...*/
                                Config.ConfirmationClass CC = new Config.ConfirmationClass()
                                {
                                    account = accountList[i],
                                    conf = conf,
                                    tradeid = conf.ConfirmationID,
                                    displayed = false
                                };

                                if (!confirmationList.Any(o => o.tradeid == CC.tradeid))
                                {
                                    confirmationList.Add(CC);
                                }
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        //Value cannot be null for FetchConformations
                        Debug.WriteLine(ex.ToString());
                    }
                }

                /*Go through all confirmations pending and see if we should make a popup about the trade*/
                foreach(Config.ConfirmationClass CC in confirmationList)
                {
                    /*Have to make an additional check here because user might accept from ConfirmForm*/
                    if (confirmForm.completedTrades.Contains(CC.conf.ConfirmationID))
                        CC.done = true;

                    if(!CC.displayed)
                    {
                        CC.displayed = true;
                        Invoke(new Action(() =>
                        {
                            /*Invoke because yolo*/
                            DoPopup(CC);

                            /*Play notification sound from resources*/
                            SoundPlayer notifyPlayer = new SoundPlayer();
                            notifyPlayer.Stream = Properties.Resources.notifysound;
                            notifyPlayer.Play();

                        }));
                    }
                }
            });

            confirmForm.RefreshList(confirmationList);
        }


        /// <summary>
        /// Unlink selected account
        /// </summary>
        private void unlinkAuthenticator_Click(object sender, EventArgs e)
        {
            if(accountCurrent != null)
            {
                /*Get current Steam Guard code and have user input the code as a sort of extra confirmation...*/
                string faAuthCode = accountCurrent.GenerateSteamGuardCode();
                InputForm confirmForm = new InputForm(string.Format("Type in the following code to remove the authenticator for {0}: {1}", accountCurrent.AccountName, faAuthCode));
                confirmForm.ShowDialog();

                if(!confirmForm.inputCancelled)
                {
                    /*See if input matches*/
                    string inputStr = confirmForm.inputText.Text.Trim().ToUpper();
                    if(inputStr == faAuthCode.ToUpper())
                    {
                        DialogResult dialogResult = MessageBox.Show("This action will unlink the authenticator for the account: {0}\n"
                            + "If you want to be able to trade without waiting 3 days you will need to link another authenticator to your account and wait 7 days before you can continue trading.\n\n"
                            + "Are you sure you want to proceed?", "Confirm selection", MessageBoxButtons.YesNo);

                        if (dialogResult == DialogResult.Yes)
                        {
                            /*Try to unlink*/
                            if (accountCurrent.DeactivateAuthenticator())
                            {
                                MessageBox.Show("Authenticator has been successfully unlinked.");

                                /*Delete files for that account*/
                                FileHandler.DeleteSGAFile(accountCurrent);
                                loadAccounts();
                            }
                            else
                            {
                                MessageBox.Show("Unable to unlink authenticator. Please try again.");
                                return;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Confirmation failed.");
                        return;
                    }
                }
            }
        }
    }
}
