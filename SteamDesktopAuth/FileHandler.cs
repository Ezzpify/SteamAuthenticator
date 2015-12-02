using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                File.WriteAllText(fileName, Crypto.EncryptStringAES(content));
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
                    SteamGuardAccount account = JsonConvert.DeserializeObject<SteamGuardAccount>(Crypto.DecryptStringAES(File.ReadAllText(file.FullName)));
                    sgaList.Add(account);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            return sgaList;
        }
    }
}
