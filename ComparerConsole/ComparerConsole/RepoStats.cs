using System;
using System.Linq;

namespace ComparerConsole
{
    public class RepoStats
    {
        public DateTime QueryDate;

        public string RepoUrl;
        public int Stars;
        public int Forks;
        public int Watchs;

        public string RepoOwner => RepoUrl.Split(Convert.ToChar("/")).Reverse().Skip(1).First();
        public string RepoName => RepoUrl.Split(Convert.ToChar("/")).Reverse().First();
    }
}
