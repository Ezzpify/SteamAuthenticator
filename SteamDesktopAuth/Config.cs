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

        public class Object
        {
            public string sha { get; set; }
            public string type { get; set; }
            public string url { get; set; }
        }

        public class Github
        {
            public string @ref { get; set; }
            public string url { get; set; }
            public Object @object { get; set; }
        }
    }
}
