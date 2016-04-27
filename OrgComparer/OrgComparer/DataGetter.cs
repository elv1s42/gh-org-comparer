using System;
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
                    Console.WriteLine(json);
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
            Console.WriteLine(jsonString);


            Console.WriteLine("_________________");


            var json = JObject.Parse(jsonString);


            Console.WriteLine($"json.HasValues: {json.HasValues}");
            Console.WriteLine($"json.Path: {json.Path}");
            Console.WriteLine($"count: {json.GetValue("total_count")}");


        }


    }
}
