using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace SteamDesktopAuth
{
    public class GitHub
    {
        /// <summary>
        /// Checks if the application is up-to-date
        /// </summary>
        /// <returns>Returns true if up to date, false if not</returns>
        public bool ApplicationUpToDate()
        {
            /*Download release tags from github api*/
            /*If API changes this will break, so for future visitors - this might be your problem*/
            string refJson = DownloadString("https://api.github.com/repos/Ezzpify/SteamAuthenticator/git/refs/tags");
            if (refJson.Length > 0)
            {
                try
                {
                    /*Parse github response*/
                    Config.Github gitHub = JsonConvert.DeserializeObject<Config.Github[]>(refJson).Last();

                    /*Get versions*/
                    string gitLatestVersion = gitHub.@ref.Substring(gitHub.@ref.LastIndexOf('/') + 1);
                    string appCurrentVersion = Application.ProductVersion;
                    if (gitLatestVersion == appCurrentVersion) return true;
                }
                catch
                { 
                    /*API response probably changed*/
                    /*But we want the application to be usable, so return true to not bug user*/
                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// Downloads a string from the internet from the specified url
        /// </summary>
        /// <param name="URL">Url to download string from</param>
        /// <returns>Returns string of url website source</returns>
        private string DownloadString(string URL)
        {
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    string HtmlResult = wc.DownloadString(URL);
                    return HtmlResult;
                }
                catch
                {
                    return "0";
                }
            }
        }
    }
}
