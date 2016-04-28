using System;
using System.Linq;
using System.Net;
using Newtonsoft.Json.Linq;

namespace OrgComparer
{
    public class DataGetter
    {
        public static string LoadJsonString(string url)
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


    }
}
