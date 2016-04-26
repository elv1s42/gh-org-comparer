using System;
using Octokit;
using OrgComparer;

namespace OrgComparerConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = "n";
            while (input.Equals("n") || input.Equals("N"))
            {
                var githubClient = new GitHubClient(new ProductHeaderValue("my-cool-app"));

                //var list = Core.GetOrgs("Accenture", "Luxoft", "mailru", "yandex", "Cocaine", "Artezio", "Epam");

                var list = githubClient.GetOrgs("EPAM Systems");

                Console.WriteLine($"Returned: {list.Count}");

                foreach (var org in list)
                {
                    var repos = githubClient.GetPublicRepos(org);
                    Console.WriteLine($"Repos count: {repos.Count}");

                }

                Console.WriteLine("Exit? (y/n)");
                input = Console.ReadLine() ?? "y";
            }
        }
    }
}
