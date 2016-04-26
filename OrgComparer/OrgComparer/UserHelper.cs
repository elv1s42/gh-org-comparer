using System;
using Octokit;

namespace OrgComparer
{
    public static class UserHelper
    {
        public static void WriteUserInfo(User user)
        {
            Console.WriteLine($"    --- Organization Info: ---");
            Console.WriteLine($"    Org '{user.Name}' has {user.PublicRepos} public repos.");
            Console.WriteLine($"    Org api url: '{user.Url}'");
            Console.WriteLine($"    Org home url: '{user.HtmlUrl}'");
        }
    }
}
