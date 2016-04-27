using System;
using System.Collections.Generic;
using System.Linq;
using Octokit;
using OrgComparer;
using OrgComparer.CustomClasses;
using OrgComparer.Helpers;

namespace OrgComparerConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = "y";
            while (!input.Equals("n") && !input.Equals("N"))
            {
                var organizations = new List<Org>();

                var githubClient = new GitHubClient(new ProductHeaderValue("my-cool-app"));

                var list = githubClient.GetOrgsBySearch("Accenture", "Luxoft", "mailru", "yandex", "Cocaine", "Artezio", "Epam");
                //var list = githubClient.GetOrgsBySearch("EPAM Systems");

                //var list = githubClient.GetOrgsByLogin("epam");

                //var list = githubClient.GetOrgsCustom();

                Console.WriteLine($"Returned: {list.Count}");
                
                foreach (var org in list)
                {
                    UserHelper.WriteUserInfo(org);

                    var repos = githubClient.GetPublicSourceRepos(org).ToList();
                    Console.WriteLine($"    Repos count: {repos.Count}");

                    organizations.Add(new Org {Organization = org, Repos = repos});

                    foreach (var repo in repos)
                    {
                        RepoHelper.WriteRepoInfo(repo);
                    }
                }
                
                //var lastUpdate = DateTime.Now.AddYears(-1);
                var lastUpdate = DateTime.Now.AddMonths(-6);
                foreach (var organization in organizations)
                {
                    StatInfoHelper.WriteRepoInfo(organization.GetStats(lastUpdate));
                }

                Console.WriteLine("Continue? (y/n)");
                input = Console.ReadLine() ?? "y";
            }
        }
    }
}
