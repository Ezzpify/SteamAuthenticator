using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SteamAuth;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace SteamDesktopAuth
{
    public class FileHandler
    {
        /// <summary>
        /// Saves a SteamGuardAccount to file in formatted json
        /// </summary>
        /// <param name="account">Account to save</param>
        /// <returns>Returns true if successful</returns>
        public static bool SaveSGAFile(SteamGuardAccount account)
        {
            try
            {
                string fileName = Path.Combine(Application.StartupPath, string.Format("SGAFiles\\{0}.SGA", account.Session.SteamID));
                string content = JsonConvert.SerializeObject(account, Formatting.Indented);
                if(Crypto.crySecret.Length > 0) content = Crypto.EncryptStringAES(content);
                File.WriteAllText(fileName, content);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }


        /// <summary>
        /// Deletes a SGA file of an account if it exists
        /// </summary>
        /// <param name="account">Account to delete files of</param>
        /// <returns>Returns true if successful</returns>
        public static bool DeleteSGAFile(SteamGuardAccount account)
        {
            try
            {
                string fileName = string.Format(Path.Combine(Application.StartupPath, "SGAFiles\\{0}.SGA"), account.Session.SteamID);
                if(File.Exists(fileName))
                    File.Delete(fileName);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }


        /// <summary>
        /// Returns all accounts available in a list
        /// </summary>
        /// <returns>Returns list of accounts</returns>
        public static List<SteamGuardAccount> GetAllAccounts()
        {
            List<SteamGuardAccount> sgaList = new List<SteamGuardAccount>();
            DirectoryInfo info = new DirectoryInfo(Path.Combine(Application.StartupPath, "SGAFiles"));
            FileInfo[] files = info.GetFiles("*.SGA");

            foreach(FileInfo file in files)
            {
                try
                {
                    string contentStr = File.ReadAllText(file.FullName);
                    if (contentStr.Length > 0)
                    {
                        /*N1 way to determine if it's hashed*/
                        if (contentStr.EndsWith("=="))
                        {
                            if (Crypto.crySecret.Length > 0) contentStr = Crypto.DecryptStringAES(contentStr);
                            else continue;
                        }

                        SteamGuardAccount account = JsonConvert.DeserializeObject<SteamGuardAccount>(contentStr);
                        if (account != null)
                            sgaList.Add(account);
                    }
                    else
                    {
                        /*String was returned empty*/
                        /*This means password (secret) was incorrect for the account*/
                        MessageBox.Show(string.Format("Could not open save for SteamID64: {0}", Path.GetFileNameWithoutExtension(file.Name)), "Wrong password");
                    }
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }

            return sgaList;
        }
    }
}
