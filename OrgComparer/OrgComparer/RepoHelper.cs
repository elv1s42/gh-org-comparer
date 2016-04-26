using System;
using Octokit;

namespace OrgComparer
{
    public static class RepoHelper
    {
        public static void WriteRepoInfo(Repository repo)
        {
            Console.WriteLine($"        --- Repo Info: ---");
            Console.WriteLine($"        Repo Id: '{repo.Id}'");
            Console.WriteLine($"        Repo '{repo.Name}' has {repo.ForksCount} forks and {repo.StargazersCount} stars.");
            Console.WriteLine($"        Repo api url: '{repo.Url}'");
            Console.WriteLine($"        Repo home url: '{repo.HtmlUrl}'");
            Console.WriteLine($"        Repo created at {repo.CreatedAt.DateTime.ToString("yy-MM-dd")}");
            Console.WriteLine($"        Repo updated at {repo.UpdatedAt.DateTime.ToString("yy-MM-dd")}");
        }
    }
}
