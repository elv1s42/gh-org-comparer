using System;
using System.Collections.Generic;
using System.Linq;
using Octokit;

namespace OrgComparer
{
    public static class Core
    {
        public static List<User> GetOrgs(this GitHubClient githubClient, params string [] orgs)
        {
            var list = new List<User>();

            foreach (var org in orgs)
            {
                var request = new SearchUsersRequest(org)
                {
                    AccountType = AccountSearchType.Org,
                    In = new[] {UserInQualifier.Username}
                };

                var r = githubClient.Search.SearchUsers(request);
                r.Wait();

                var result = r.Result;

                var count = result.TotalCount;
                if (count == 0)
                {
                    Console.WriteLine($"Org '{org}' was not found, skipping this org name");
                }
                else
                {
                    if (count == 1)
                    {
                        Console.WriteLine($"Org '{org}' was found.");
                    }
                    else if (count > 1)
                    {
                        Console.WriteLine($"Result for '{org}' search has {count} organizations, taking first one");
                    }
                    
                    var user = result.Items.First();
                    list.Add(user);
                }
            }

            if (list.Count < orgs.Length)
            {
                Console.WriteLine("Warning! Not all orgs were found.");
            }

            return list;
        }

        public static IReadOnlyList<Repository> GetPublicRepos(this GitHubClient client, User user)
        {
            var r = client.Repository.GetAllForOrg(user.Login);
            r.Wait();

            var list = r.Result;
            return list;
        }
    }
}
