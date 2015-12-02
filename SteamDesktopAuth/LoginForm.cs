using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using SteamAuth;
using System.Diagnostics;

namespace SteamDesktopAuth
{
    public partial class LoginForm : Form
    {
        /// <summary>
        /// General variables
        /// </summary>
        private string cryHash;
        private Point formStartupLocationPoint;
        private UserLogin userLogin;


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
        /// <param name="xy">Form location values as point</param>
        public LoginForm(Point xy)
        {
            InitializeComponent();
            formStartupLocationPoint = xy;
        }


        /// <summary>
        /// Form load event
        /// </summary>
        private void LoginForm_Load(object sender, EventArgs e)
        {
            Location = formStartupLocationPoint;
        }


        /// <summary>
        /// Validates if a provided phone number is in the correct format
        /// </summary>
        /// <param name="input">Phone number as string</param>
        /// <returns>returns formatted number if valid, empty string if invalid</returns>
        private string FilterPhoneNumber(string input)
        {
            if (input.Length > 0)
            {
                return Regex.Replace(input, "[^0-9a-zA-Z+]", "");
            }

            return string.Empty;
        }


        /// <summary>
        /// Checks if phone number input is valid
        /// </summary>
        /// <param name="str">Phone number as string</param>
        /// <returns></returns>
        private bool ValidPhoneNumberInput(string str)
        {
            if (str == null || str.Length == 0 || str[0] != '+')
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// Close button
        /// </summary>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// <summary>
        /// Login button
        /// </summary>
        private void loginButton_Click(object sender, EventArgs e)
        {
            string susername = usernameText.Text;
            string spassword = passwordText.Text;

            if(susername.Length > 0 && spassword.Length > 0)
            {
                userLogin = new UserLogin(susername, spassword);
                LoginResult loginres = LoginResult.BadCredentials;

                while((loginres = userLogin.DoLogin()) != LoginResult.LoginOkay)
                {
                    /*We need to enter the email code to access the account*/
                    if(loginres == LoginResult.NeedEmail)
                    {
                        InputForm emailForm = new InputForm("Enter the code sent to the email account associated with the account.");
                        emailForm.ShowDialog();
                        if(emailForm.inputCancelled)
                        {
                            Close();
                            return;
                        }

                        userLogin.EmailCode = emailForm.inputText.Text;
                        continue;
                    }

                    /*We need the captcha ...*/
                    if(loginres == LoginResult.NeedCaptcha)
                    {
                        Process.Start(string.Format("{0}/public/captcha.php?gid={1}", APIEndpoints.COMMUNITY_BASE, userLogin.CaptchaGID));

                        InputForm captchaForm = new InputForm("Enter the captcha code that is showing in your browser.");
                        captchaForm.ShowDialog();
                        if(captchaForm.inputCancelled)
                        {
                            Close();
                            return;
                        }

                        userLogin.CaptchaText = captchaForm.inputText.Text;
                        continue;
                    }

                    /*We need mobile auth code ...*/
                    /*The user needs to remove existing authenticator before we can proceed, so we'll just bail out*/
                    if(loginres == LoginResult.Need2FA)
                    {
                        MessageBox.Show("Please remove the existing authenticator device you have attatched to your account to in order to proceed.");
                        Close();
                        return;
                    }

                    /*Incorrect password or similar*/
                    if(loginres == LoginResult.GeneralFailure)
                    {
                        MessageBox.Show("Trouble logging in.\nWrong password?");
                        Close();
                        return;
                    }
                }

                /*Login successful, proceed*/
                SessionData sessionData = userLogin.Session;
                AuthenticatorLinker authLinker = new AuthenticatorLinker((sessionData));
                AuthenticatorLinker.LinkResult authResult = AuthenticatorLinker.LinkResult.GeneralFailure;
                while ((authResult = authLinker.AddAuthenticator()) != AuthenticatorLinker.LinkResult.AwaitingFinalization)
                {
                    /*We need phone number to proceed*/
                    if(authResult == AuthenticatorLinker.LinkResult.MustProvidePhoneNumber)
                    {
                        string phoneNumber = string.Empty;
                        while(!ValidPhoneNumberInput(phoneNumber))
                        {
                            InputForm phoneNumberForm = new InputForm("Enter your phone number in the following format:\n+1 123-456-7890");
                            phoneNumberForm.inputText.Text = "+1 ";
                            phoneNumberForm.ShowDialog();
                            if(phoneNumberForm.inputCancelled)
                            {
                                Close();
                                return;
                            }

                            phoneNumber = FilterPhoneNumber(phoneNumberForm.inputText.Text);
                        }

                        authLinker.PhoneNumber = phoneNumber;
                        continue;
                    }

                    /*Remove previous number attatched to the account*/
                    if(authResult == AuthenticatorLinker.LinkResult.MustRemovePhoneNumber)
                    {
                        authLinker.PhoneNumber = string.Empty;
                        continue;
                    }

                    /*Oops*/
                    if(authResult == AuthenticatorLinker.LinkResult.GeneralFailure)
                    {
                        MessageBox.Show("Something bad happened...");
                        Close();
                        return;
                    }
                }

                /*Taking a pause to save the information that we've gathered thus far*/
                if(!FileHandler.SaveSGAFile(authLinker.LinkedAccount))
                {
                    MessageBox.Show("Unable to save the current data. The authenticator has not been linked.", "Error");
                    Close();
                    return;
                }

                MessageBox.Show(string.Format("Before finishing, write down your revocation code incase you wish to remove Steam Authenticator from your account:\n"
                    + "{0}\n\n"
                    + "I'm going to ask for it later again.", authLinker.LinkedAccount.RevocationCode), "Almost there...");

                /*Final checks*/
                AuthenticatorLinker.FinalizeResult finalRes = AuthenticatorLinker.FinalizeResult.GeneralFailure;
                while(finalRes != AuthenticatorLinker.FinalizeResult.Success)
                {
                    /*Get SMS code that was sent to users phone, providing the number was correct*/
                    InputForm smsForm = new InputForm("Enter the SMS code you received on your phone.");
                    smsForm.ShowDialog();
                    if(smsForm.inputCancelled)
                    {
                        /*Delete file here*/
                        FileHandler.DeleteSGAFile(authLinker.LinkedAccount);
                        Close();
                        return;
                    }

                    /*Reminder to make sure the user wrote down the revocation code*/
                    InputForm confirmRecocationForm = new InputForm("Enter your revocation code that you wrote down.");
                    confirmRecocationForm.ShowDialog();
                    if(confirmRecocationForm.inputCancelled)
                    {
                        Close();
                        return;
                    }

                    /*Check if the revocation code that was entered by user matches with the correct code*/
                    if(confirmRecocationForm.inputText.Text.ToUpper() != authLinker.LinkedAccount.RevocationCode.ToUpper())
                    {
                        MessageBox.Show("Incorrect revocation code - the authentication was not linked.\nYou need to remember the code.", "Error");

                        /*Delete file here*/
                        FileHandler.DeleteSGAFile(authLinker.LinkedAccount);
                        Close();
                        return;
                    }

                    /*Finalize the process and check last things*/
                    finalRes = authLinker.FinalizeAddAuthenticator(smsForm.inputText.Text);

                    /*Check if the SMS code was bad*/
                    if(finalRes == AuthenticatorLinker.FinalizeResult.BadSMSCode)
                    {
                        MessageBox.Show("Incorrect SMS code. Try again.");
                        continue;
                    }

                    /*General failure number one*/
                    if(finalRes == AuthenticatorLinker.FinalizeResult.UnableToGenerateCorrectCodes)
                    {
                        MessageBox.Show(string.Format("Unable to generate correct codes.\nThe authenticator has not been linked.\n\n"
                            + "However, please write the revocation code down just incase:\n\n  {0}", authLinker.LinkedAccount.RevocationCode), "Error");

                        /*Delete file here*/
                        FileHandler.DeleteSGAFile(authLinker.LinkedAccount);
                        Close();
                        return;
                    }

                    /*General failure number two*/
                    if(finalRes == AuthenticatorLinker.FinalizeResult.GeneralFailure)
                    {
                        MessageBox.Show(string.Format("Unable to generate correct codes.\nThe authenticator has not been linked.\n\n"
                            + "However, please write the revocation code down just incase:\n\n  {0}", authLinker.LinkedAccount.RevocationCode), "Error");

                        /*Delete file here*/
                        FileHandler.DeleteSGAFile(authLinker.LinkedAccount);
                        Close();
                        return;
                    }
                }

                /*Finally done - save everything*/
                if(!FileHandler.SaveSGAFile(authLinker.LinkedAccount))
                {
                    MessageBox.Show("Save failed.");
                    //Do something about it
                }

                MessageBox.Show("Mobile authenticator successfully linked.", "Success!");
                Close();
            }
            else
            {
                MessageBox.Show("Missing login details.");
            }
        }
    }
}
