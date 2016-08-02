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
                bool encrypt = true;
                string fileName = Path.Combine(Application.StartupPath, string.Format("SGAFiles\\{0}.SGA", account.Session.SteamID));
                string content = JsonConvert.SerializeObject(account, Formatting.Indented);

                /*File already exists, so we need to make sure we don't
                encrypt it if it's not meant to be encrypted*/
                if (File.Exists(fileName))
                {
                    if(!File.ReadAllText(fileName).EndsWith("="))
                    {
                        encrypt = false;
                    }
                }

                if (Crypto.crySecret.Length > 0 && encrypt) content = Crypto.EncryptStringAES(content);
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
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }


        /// <summary>
        /// Finds the account file that belongs to a file
        /// </summary>
        /// <param name="account">Account the owns the file</param>
        /// <returns>Returns null if none found</returns>
        private static FileInfo GetAccountFile(SteamGuardAccount account)
        {
            DirectoryInfo info = new DirectoryInfo(Path.Combine(Application.StartupPath, "SGAFiles"));
            FileInfo[] files = info.GetFiles("*.SGA");
            foreach (var file in files)
            {
                if (Path.GetFileNameWithoutExtension(file.Name) == account.Session.SteamID.ToString())
                {
                    return file;
                }
            }

            return null;
        }

        
        /// <summary>
        /// Checks if an SGA file is encrypted
        /// </summary>
        /// <param name="account">Account to check file of</param>
        /// <returns>Also returns false if file is not found</returns>
        public static bool IsSGAFileEncrypted(SteamGuardAccount account)
        {
            try
            {
                var file = GetAccountFile(account);
                if (File.Exists(file.FullName))
                {
                    /*I know this is a shit way to do it, but honestly it works*/
                    /*If a user wants to fuck with his files then so be it; I don't really care*/
                    string content = File.ReadAllText(file.FullName);
                    return content.EndsWith("=");
                }
            }
            catch
            {
                //
            }

            return false;
        }


        /// <summary>
        /// Edits a SGA file to either encrypt or decrypt the file
        /// </summary>
        /// <param name="account">Account to edit</param>
        /// <param name="secret">secret for the file</param>
        /// <returns></returns>
        public static bool DecryptSGAFile(SteamGuardAccount account)
        {
            /*First we find the file related to the account*/
            FileInfo sgaFile = GetAccountFile(account);
            if (sgaFile != null)
            {
                if (!File.Exists(sgaFile.FullName))
                    return false;

                /*Read the content of the file*/
                string fileContent = File.ReadAllText(sgaFile.FullName);
                if (IsSGAFileEncrypted(account))
                {
                    /*If the file looks encrypted, decrypt and save the file if decryption worked*/
                    string decryptedText = Crypto.DecryptStringAES(fileContent);
                    if (decryptedText.Length > 0)
                    {
                        File.WriteAllText(sgaFile.FullName, decryptedText);
                        return true;
                    }
                }
            }
            
            return false;
        }


        /// <summary>
        /// Returns all accounts available in a list
        /// </summary>
        /// <returns>Returns list of accounts</returns>
        public static List<Config.LoadSteamGuardAccount> GetAllAccounts()
        {
            /*Locate the .SGA save files*/
            var sgaList = new List<Config.LoadSteamGuardAccount>();
            DirectoryInfo info = new DirectoryInfo(Path.Combine(Application.StartupPath, "SGAFiles"));
            FileInfo[] files = info.GetFiles("*.SGA");

            foreach(FileInfo file in files)
            {
                bool skipDeserialize = false;
                try
                {
                    string contentStr = File.ReadAllText(file.FullName);
                    if (contentStr.Length > 0)
                    {
                        /*N1 way to determine if it's hashed*/
                        if (contentStr.EndsWith("="))
                        {
                            /*If user skips password the secret will be empty, so don't bother with these accounts*/
                            if (Crypto.crySecret.Length > 0)
                            {
                                contentStr = Crypto.DecryptStringAES(contentStr);
                            }
                            else
                            {
                                skipDeserialize = true;
                            }
                        }

                        /*Try to deserialize content to account class*/
                        var accountHolder = new Config.LoadSteamGuardAccount();
                        SteamGuardAccount account = null;
                        if (!skipDeserialize)
                        {
                            /*We don't want to attempt to deserialize this string, but we still want to add the other information*/
                            account = JsonConvert.DeserializeObject<SteamGuardAccount>(contentStr);
                            accountHolder.account = account;
                        }
                        
                        /*Load the rest of information about the file*/
                        accountHolder.loaded = (account != null);
                        accountHolder.filename = Path.GetFileNameWithoutExtension(file.Name);

                        /*Add account holder to list that we will return*/
                        sgaList.Add(accountHolder);
                    }
                }
                catch(Exception ex)
                {
                    /*Whoops - what happend here?*/
                    MessageBox.Show(ex.ToString());
                }
            }

            return sgaList;
        }
    }
}
