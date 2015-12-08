using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Text.RegularExpressions;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace SteamDesktopAuth
{
    public class GitHub
    {
        /// <summary>
        /// Stores the new update available if there is one available
        /// </summary>
        public Config.GithubRelease update;


        /// <summary>
        /// Checks if the application is up-to-date
        /// </summary>
        /// <returns>Returns true if up to date, false if not</returns>
        public bool ApplicationUpToDate()
        {
            /*Download release tags from github api*/
            /*If API changes this will break, so for future visitors - this might be your problem*/
            string refJson = DownloadString("https://api.github.com/repos/Ezzpify/SteamAuthenticator/git/refs/tags");
            Config.Github gitHub = null;
            if (refJson.Length > 0)
            {
                try
                {
                    /*Parse github response - get last release in json array*/
                    gitHub = JsonConvert.DeserializeObject<Config.Github[]>(refJson).Last();

                    /*Get versions*/
                    string gitLatestVersion = gitHub.@ref.Substring(gitHub.@ref.LastIndexOf('/') + 1);
                    string appCurrentVersion = Application.ProductVersion;

                    /*Check if version string from github is in the wrong format (not ie 1.5.5)*/
                    /*If it's not then something is broken or changed at github, but instead of bugging user we'll return true*/
                    if (!new Regex(@"^[0-9.]+$").IsMatch(gitLatestVersion)) return true;

                    /*If version are the same*/
                    if (gitLatestVersion == appCurrentVersion) return true;
                }
                catch
                { 
                    /*API response probably changed*/
                    /*But we want the application to be usable, so return true to not bug user*/
                    return true;
                }
            }

            /*There's an update available, set update variable class and return false*/
            string commitJson = DownloadString(gitHub.@object.url);
            update = JsonConvert.DeserializeObject<Config.GithubRelease>(commitJson);
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
