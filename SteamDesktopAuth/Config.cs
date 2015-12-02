using SteamAuth;

namespace SteamDesktopAuth
{
    public class Config
    {
        public class ConfirmationClass
        {
            public SteamGuardAccount account { get; set; }
            public Confirmation conf { get; set; }
            public string tradeid { get; set; }
            public bool displayed { get; set; }
            public bool done { get; set; }
        }
    }
}
