using System;
using System.Linq;
using NUnit.Framework;
using Octokit;

namespace OrgComparerTests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test()
        {
            var githubClient = new GitHubClient(new ProductHeaderValue("test"));
            var request = new SearchUsersRequest("EPAM Systems")
            {
                AccountType = AccountSearchType.Org,
                In = new[] { UserInQualifier.Username }
            };

            var res = githubClient.Search.SearchUsers(request);
            res.Wait();

            var user = res.Result.Items.First();

            var nl = Environment.NewLine;
            Console.WriteLine($"Org '{user.Name}' has {user.PublicRepos} public repos." + nl 
                + $"Org api url: '{user.Url}'" + nl
                + $"Org home url: '{user.HtmlUrl}'");
        }
    }
}
