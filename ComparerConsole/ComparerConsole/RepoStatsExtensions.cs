using System;
using System.Collections.Generic;
using System.Linq;

namespace ComparerConsole
{
    public static class RepoStatsExtensions
    {
        public static string GetRepoOwner(this RepoStats rs)
        {
            return rs.RepoUrl.Split(Convert.ToChar("/")).Reverse().Skip(1).First();
        }

        public static string GetRepoName(this RepoStats rs)
        {
            return rs.RepoUrl.Split(Convert.ToChar("/")).Reverse().First();
        }

        public static void WriteToConsole(this List<RepoStats> list, int number)
        {
            var repoStat = list.Skip(number - 1).First();
            var nl = Environment.NewLine;

            Console.WriteLine($"Repo '{repoStat.GetRepoOwner()}/{repoStat.GetRepoName()}' with url '{repoStat.RepoUrl}' and id '{repoStat.RepoId}'" + nl
                        + $"has {repoStat.Stars} stars and {repoStat.Forks} forks.");
        }
    }
}
