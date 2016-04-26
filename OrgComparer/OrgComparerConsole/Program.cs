using System;
using OrgComparer;

namespace OrgComparerConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var res = string.Empty;
            while (!res.Equals("y") && !res.Equals("Y"))
            {
                //Core.GetOrgs("Accenture", "Luxoft", "mailru", "yandex", "Cocaine", "Artezio", "Epam")
                //    .Wait();
                var success = Core.GetOrgs("EPAM Systems");
                success.Wait();

                Console.WriteLine($"Result: {success.Result}");

                Console.WriteLine("Exit? (y/n)");
                res = Console.ReadLine() ?? "n";
            }
        }
    }
}
