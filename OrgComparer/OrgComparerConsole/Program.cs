using System;
using System.Collections.Generic;
using System.Linq;
using Octokit;
using OrgComparer;
using OrgComparer.CustomClasses;

namespace OrgComparerConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = "n";
            while (input.Equals("n") || input.Equals("N"))
            {
                var organizations = new List<Org>();

                var githubClient = new GitHubClient(new ProductHeaderValue("my-cool-app"));

                var list = githubClient.GetOrgsBySearch("Accenture", "Luxoft", "mailru", "yandex", "Cocaine", "Artezio", "Epam");
                //var list = githubClient.GetOrgs("EPAM Systems");

                Console.WriteLine($"Returned: {list.Count}");
                
                foreach (var org in list)
                {
                    UserHelper.WriteUserInfo(org);

                    var repos = githubClient.GetPublicRepos(org).ToList();
                    Console.WriteLine($"    Repos count: {repos.Count}");

                    organizations.Add(new Org {Organization = org, Repos = repos});

                    foreach (var repo in repos)
                    {
                        RepoHelper.WriteRepoInfo(repo);
                    }
                }

                var lastUpdate = DateTime.Now.AddYears(-1);
                foreach (var organization in organizations)
                {
                    Console.WriteLine($"---- Login: {organization.Organization.Login}");
                    Console.WriteLine($"Count: {organization.GetNewReposNumber(lastUpdate)}");
                    Console.WriteLine($"Forks: {organization.GetNewReposForks(lastUpdate)}");
                    Console.WriteLine($"Stars: {organization.GetNewReposStars(lastUpdate)}");
                }

                Console.WriteLine("Exit? (y/n)");
                input = Console.ReadLine() ?? "y";
            }
        }
    }
}
