using Microsoft.Win32;
using System.Security.Principal;
using System;
using System.Diagnostics;

namespace SteamDesktopAuth
{
    /// <summary>
    /// Registry management class
    /// </summary>
    public static class CRegistry
    {
        /// <summary>
        /// Start up class
        /// </summary>
        public static class StartUp
        {
            /// <summary>
            /// General variables
            /// </summary>
            private static string appName = "Steam Authenticator";


            /// <summary>
            /// If our application is registered to start at startup
            /// </summary>
            /// <returns>Returns true if it's registered</returns>
            public static bool IsRegistered()
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    return (key.GetValue(appName) != null);
                }
            }


            /// <summary>
            /// Adds our application to startup
            /// </summary>
            public static void AddApp()
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    key.SetValue(appName, "\"" + System.Reflection.Assembly.GetExecutingAssembly().Location + "\"");
                }
            }


            /// <summary>
            /// Removes our application from startup
            /// </summary>
            public static void RemoveApp()
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    key.DeleteValue(appName, false);
                }
            }
        }
    }
}
