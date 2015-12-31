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


            /// <summary>
            /// Check if we're an elevated process
            /// </summary>
            /// <returns>True if we're adminn</returns>
            public static bool IsUserAdministrator()
            {
                try
                {
                    WindowsIdentity user = WindowsIdentity.GetCurrent();
                    WindowsPrincipal principal = new WindowsPrincipal(user);
                    return principal.IsInRole(WindowsBuiltInRole.Administrator);
                }
                catch
                {
                    return false;
                }
            }


            /// <summary>
            /// Restarts the application as Admin
            /// </summary>
            public static void RestartAsAdmin()
            {
                var exeName = Process.GetCurrentProcess().MainModule.FileName;
                ProcessStartInfo startInfo = new ProcessStartInfo(Process.GetCurrentProcess().MainModule.FileName);
                startInfo.Verb = "runas";
                Process.Start(startInfo);
                Environment.Exit(1);
            }
        }
    }
}
