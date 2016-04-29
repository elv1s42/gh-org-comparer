using System;
using System.IO;
using Newtonsoft.Json;

namespace ComparerConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = "y";
            while (!input.Equals("n") && !input.Equals("N"))
            {
                var lang = "csharp";
                var list = DataGetter.GetTop1000(lang);

                list.WriteToConsole(1);

                var json = JsonConvert.SerializeObject(list, Formatting.Indented);

                File.WriteAllText(Path.Combine(DataGetter.GetPath(), $"{lang}.json"), json);
                
                Console.WriteLine("Continue? (y/n)");
                input = Console.ReadLine() ?? "y";
            }
        }
    }
}
