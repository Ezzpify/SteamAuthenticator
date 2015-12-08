using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
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
        private GitHub gitHub;
        private ConfirmForm confirmForm;
        private SteamGuardAccount accountCurrent;
        private List<SteamGuardAccount> accountList;
        private List<Config.ConfirmationClass> confirmationList;
        private List<PopupForm> popupForms;

        private long steamTime;
        private long currentSteamChunk;

        private bool minimizedNotificationShown;
        private bool applicationUpdateAvailable;


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
            gitHub              = new GitHub();
            accountList         = new List<SteamGuardAccount>();
            popupForms          = new List<PopupForm>();
            confirmationList    = new List<Config.ConfirmationClass>();
            confirmForm         = new ConfirmForm();

            versionLabel.Text   = "v" + Application.ProductVersion;

            /*We'll ask for a password here*/
            /*The password will be used as the secret for encrypting the data we'll store along with a generated salt*/
            /*It will be asked for every time the application starts and cannot be recovered*/
            /*User can also chose not to enter a password with a button, but this is not secure*/
            InputForm passwordForm = new InputForm("Enter password. If you are a new user, enter a new password (6-25 chars). It can not be recovered.", true);
            passwordForm.ShowDialog();

            if (passwordForm.inputCancelled)
            {
                Environment.Exit(1);
            }
            else if (!passwordForm.inputNoPassword)
            {
                string password = passwordForm.inputText.Text;
                if (password.Length >= 6 && password.Length <= 25)
                {
                    Crypto.crySecret = password;
                    Crypto.crySalt = Encoding.ASCII.GetBytes("RandomNumberFour"); //Guaranteed to be random - temp
                }
                else
                {
                    MessageBox.Show("Password is not between 6-25 chars.", "Error");
                    Environment.Exit(1);
                }
            }

            /*Check if application is up-to-date*/
            updateChecker.RunWorkerAsync();
            IsLoading(true);

            /*Create folder that we'll store all save files in*/
            Directory.CreateDirectory(Path.Combine(Application.StartupPath, "SGAFiles"));
            loadAccounts();
        }


        /// <summary>
        /// Notifyicon for main foorm
        /// </summary>
        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                if (WindowState == FormWindowState.Minimized)
                {
                    ShowInTaskbar = true;
                    WindowState = FormWindowState.Normal;
                }
                else if (WindowState == FormWindowState.Normal)
                {
                    ShowInTaskbar = false;
                    WindowState = FormWindowState.Minimized;
                }
            }
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
        /// Exit button for the notify menu
        /// </summary>
        private void menuExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }


        /// <summary>
        /// Runs the update check via GitHub.cs
        /// </summary>
        private void updateChecker_DoWork(object sender, DoWorkEventArgs e)
        {
            applicationUpdateAvailable = !gitHub.ApplicationUpToDate();
        }


        /// <summary>
        /// Update check is done, so stop the loading animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateChecker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (applicationUpdateAvailable)
            {
                versionLabel.Text = versionLabel.Text + " - Update available";

                /*Show messagebox containing info of update*/
                DialogResult diagResult =
                MessageBox.Show(string.Format(
                      "An update is available on GitHub!\nWant to download it now?\n\n"
                    + "Note:\n{0}\n\n---\n{1}\n{2}\n{3}", 
                      gitHub.update.message, 
                      gitHub.update.committer.name, 
                      gitHub.update.committer.email, 
                      gitHub.update.committer.date), 
                      "Hey, listen",
                      MessageBoxButtons.YesNo);

                /*Go to latest release if pressed yes*/
                if(diagResult == DialogResult.Yes)
                {
                    Process.Start("https://github.com/Ezzpify/SteamAuthenticator/releases/latest");
                }
            }
            IsLoading(false);
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
            LoginForm loginForm = new LoginForm(new Point(Location.X + 10, Location.Y + 80));
            loginForm.ShowDialog();
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
                    if (!notifyMenu.Items.ContainsKey(account.AccountName))
                    {
                        /*Add account to notifyMenu (contextMenuStrip attatched to notifyIcon) so user can access things quickly*/
                        ToolStripMenuItem tsm = new ToolStripMenuItem();
                        tsm.Text = account.AccountName;
                        tsm.DropDownItems.Add("Copy Steam Guard code", null, new EventHandler(menuGetCode_Click));
                        tsm.DropDownItems.Add("Accept all trades", null, new EventHandler(menuAcceptTrades_Click));
                        notifyMenu.Items.Add(tsm);
                    }
                }

                accountListBox.SelectedIndex = 0;
            }
        }


        /// <summary>
        /// Gets an account from the global list from a sub-menu item from notifyMenu
        /// </summary>
        /// <param name="sender">sender from click event</param>
        /// <returns>Returns SteamGuardAccount if found, else null</returns>
        private SteamGuardAccount GetAccountFromMenuItem(object sender)
        {
            ToolStripMenuItem clickedItem = sender as ToolStripMenuItem;
            if (clickedItem != null)
            {
                string accountClicked = clickedItem.OwnerItem.Text;
                if (accountClicked != null && accountClicked.Length > 0)
                {
                    /*Try get account*/
                    SteamGuardAccount account = accountList.First(o => o.AccountName.ToLower() == accountClicked.ToLower());
                    if (account != null)
                    {
                        return account;
                    }
                }
            }

            return null;
        }


        /// <summary>
        /// Gets the auth code from an account by clicking on the submenu item for the account
        /// </summary>
        private void menuGetCode_Click(object sender, EventArgs e)
        {
            SteamGuardAccount account = GetAccountFromMenuItem(sender);
            if(account != null)
            {
                Clipboard.SetText(account.GenerateSteamGuardCodeForTime(steamTime));
                Console.Beep(800, 50);
            }
        }


        /// <summary>
        /// Accepts all pending trade for the account from clicking on the submenu item for the account
        /// </summary>
        private void menuAcceptTrades_Click(object sender, EventArgs e)
        {
            SteamGuardAccount account = GetAccountFromMenuItem(sender);
            if (account != null)
            {
                Confirmation[] confirmations = LoadConfirmations(account);
                foreach(Confirmation confirmation in confirmations)
                {
                    account.AcceptConfirmation(confirmation);
                }
                Console.Beep(800, 50);
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
        /// Loads the confirmations pending for the account
        /// Will refresh cookies if needed
        /// </summary>
        /// <param name="account">account to load confirmations from</param>
        /// <returns></returns>
        private Confirmation[] LoadConfirmations(SteamGuardAccount account)
        {
            Confirmation[] confirmations = { };

            try
            {
                confirmations = account.FetchConfirmations();
            }
            catch(SteamGuardAccount.WGTokenInvalidException)
            {
                if (account.RefreshSession())
                {
                    FileHandler.SaveSGAFile(account);
                }
            }

            return confirmations;
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
                        Confirmation[] conftemp = LoadConfirmations(accountList[i]);
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
                /*We want to confirm the user is unlinking the right account, and not the wrong one by accident*/
                InputForm confirmForm = new InputForm("Enter the username of the account you want to unlink to proceed.");
                confirmForm.ShowDialog();

                if (!confirmForm.inputCancelled)
                {
                    if (confirmForm.inputText.Text.ToLower() == accountCurrent.AccountName.ToLower())
                    {
                        DialogResult dialogResult = MessageBox.Show(string.Format("This action will unlink the authenticator for the account: {0}\n"
                            + "Are you sure you want to proceed?",
                            accountCurrent.AccountName),
                            "Confirm selection",
                            MessageBoxButtons.YesNo);

                        if (dialogResult == DialogResult.Yes)
                        {
                            if (accountCurrent.DeactivateAuthenticator())
                            {
                                MessageBox.Show("Authenticator has been unlinked.", "Success");
                                confirmTimer.Stop();
                                Thread.Sleep(1000);

                                FileHandler.DeleteSGAFile(accountCurrent);
                                loadAccounts();
                                confirmTimer.Start();
                            }
                            else
                            {
                                MessageBox.Show("Unable to unlink authenticator. Please try again.", "Error");
                                return;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Confirmation failed", "Boop beep.");
                    }
                }
            }
        }


        /// <summary>
        /// Version label takes user to github release page
        /// </summary>
        private void versionLabel_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/Ezzpify/SteamAuthenticator/releases/latest");
        }
    }
}
