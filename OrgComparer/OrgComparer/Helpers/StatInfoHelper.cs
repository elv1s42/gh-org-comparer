using System;
using OrgComparer.CustomClasses;

namespace OrgComparer.Helpers
{
    public class StatInfoHelper
    {
        public static void WriteRepoInfo(StatInfo stats)
        {
            Console.WriteLine($"--- Stats Info: ---");
            Console.WriteLine($"Login: '{stats.Login}'");
            Console.WriteLine($"Last update after: '{stats.LastRepoUpdateTime.ToString("U")}'.");
            Console.WriteLine($"Total repos {stats.ReposCount}");
            Console.WriteLine($"Total forks {stats.ForksCount}");
            Console.WriteLine($"Total stars {stats.StarsCount}");
        }
    }
}
