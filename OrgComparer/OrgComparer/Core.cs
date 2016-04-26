using System;
using System.Linq;
using System.Threading.Tasks;
using Octokit;

namespace OrgComparer
{
    public class Core
    {
        public static async Task<bool> GetOrgs(params string [] orgs)
        {
            var success = true;

            var githubClient = new GitHubClient(new ProductHeaderValue("my-cool-app"));

            foreach (var org in orgs)
            {
                var request = new SearchUsersRequest(org)
                {
                    AccountType = AccountSearchType.Org,
                    In = new[] {UserInQualifier.Username}
                };

                var result = await githubClient.Search.SearchUsers(request);

                var count = result.TotalCount;
                if (count == 0)
                {
                    Console.WriteLine($"Org '{org}' was not found, skipping this org name");
                    success = false;
                }
                else
                {
                    if (count == 1)
                    {
                        Console.WriteLine($"Org '{org}' was found.");
                    }
                    else if (count > 1)
                    {
                        Console.WriteLine($"Result for '{org}' search has {count} results, taking first result");
                        success = false;
                    }
                    
                    var user = result.Items.First();
                    UserHelper.WriteUserInfo(user);
                }
            }
            return success;
        }
    }
}
