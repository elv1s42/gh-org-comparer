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
                Core.GetOrgs("EPAM Systems")
                    .Wait();
                Console.WriteLine("Exit? (y/n)");
                res = Console.ReadLine() ?? "n";
            }
        }
    }
}
