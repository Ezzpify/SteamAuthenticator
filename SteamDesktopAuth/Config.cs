using SteamAuth;
using System.Collections.Generic;

namespace SteamDesktopAuth
{
    public class Config
    {
        public class LoadSteamGuardAccount
        {
            public bool loaded { get; set; }
            public string filename { get; set; }
            public SteamGuardAccount account { get; set; }
        }

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

        public class Author
        {
            public string name { get; set; }
            public string email { get; set; }
            public string date { get; set; }
        }

        public class Committer
        {
            public string name { get; set; }
            public string email { get; set; }
            public string date { get; set; }
        }

        public class Tree
        {
            public string sha { get; set; }
            public string url { get; set; }
        }

        public class Parent
        {
            public string sha { get; set; }
            public string url { get; set; }
            public string html_url { get; set; }
        }

        public class GithubRelease
        {
            public string sha { get; set; }
            public string url { get; set; }
            public string html_url { get; set; }
            public Author author { get; set; }
            public Committer committer { get; set; }
            public Tree tree { get; set; }
            public string message { get; set; }
            public List<Parent> parents { get; set; }
        }
    }
}
