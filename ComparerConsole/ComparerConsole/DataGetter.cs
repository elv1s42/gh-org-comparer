using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace ComparerConsole
{
    public class DataGetter
    {
        private static string LoadJsonString(string url)
        {
            var json = "";
            using (var wc = new WebClient())
            {
                try
                {
                    wc.Headers.Add("User-Agent: Other");
                    var r = wc.DownloadStringTaskAsync(url);
                    r.Wait();

                    json = r.Result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message} in {ex.StackTrace}");
                    Console.WriteLine($"Inner: {ex.InnerException?.Message} in {ex.InnerException?.StackTrace}");
                }
            }
            return json;
        }

        private static JObject GetJObject(string url)
        {
            return JObject.Parse(LoadJsonString(url));
        }

        public static void GetOrgs()
        {
            var jsonString = LoadJsonString("https://api.github.com/search/users?q=epam+type:org");
            var jsonResponse = JObject.Parse(jsonString);

            Console.WriteLine($"json.HasValues: {jsonResponse.HasValues}");
            Console.WriteLine($"json.Path: {jsonResponse.Path}");
            Console.WriteLine($"count: {jsonResponse.GetValue("total_count")}");

            var orgs = jsonResponse["items"].Children().ToList().Select(jT => JObject.Parse(jT.ToString()));

            foreach (var org in orgs)
            {
                var jsonOrg = JObject.Parse(org.ToString());
                Console.WriteLine($"Home url: {jsonOrg.GetValue("html_url")}");

                var orgReposUrl = jsonOrg.GetValue("repos_url").ToString();
                var jsonReposString = LoadJsonString(orgReposUrl);
                var jsonReposResponse = JObject.Parse(jsonReposString);
                var repos = jsonReposResponse.Children().ToList().Select(jT => JObject.Parse(jT.ToString()));


            }
        }

        public static string GetSearchRepoString(string language, int page, int perPage)
        {
            return
                $"https://api.github.com/search/repositories?q=+language:{language}&sort=stars&order=desc&page={page}&per_page={perPage}";
        }

        public static List<RepoStats> GetTop1000(string language)
        {
            var list = new List<RepoStats>();
            for (var i = 1; i <= 10; i++)
            {
                Console.Write($"({i}) Getting 100 repos... ");
                
                var  jObj = GetJObject(GetSearchRepoString(language, i, 100));
                var jRepos = jObj["items"].Children().ToList().Select(jT => JObject.Parse(jT.ToString()));

                var j = 0;

                foreach (var jRepo in jRepos)
                {
                    j++;
                    var position = (i - 1) * 100 + j;

                    var repoStats = new RepoStats
                    {
                        RepoUrl = jRepo.GetValue("html_url").ToString(),
                        RepoId = jRepo.GetValue("id").ToString(),
                        RepoDescription = jRepo.GetValue("description").ToString(),
                        Stars = int.Parse(jRepo.GetValue("stargazers_count").ToString()),
                        Forks = int.Parse(jRepo.GetValue("forks").ToString()),
                        Watchs = int.Parse(jRepo.GetValue("watchers_count").ToString()),
                        PositionInTop = position,
                        QueryDate = DateTime.Today
                    };

                    list.Add(repoStats);
                }

                Console.WriteLine(" Done.");
            }

            Console.WriteLine($"Getting stats done. Total items: {list.Count}");

            return list;
        }

        public static string GetPath()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }
}
