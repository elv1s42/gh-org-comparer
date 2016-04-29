using System;

namespace ComparerConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = "y";
            while (!input.Equals("n") && !input.Equals("N"))
            {
                DataGetter.GetTop1000("csharp");

                Console.WriteLine("Continue? (y/n)");
                input = Console.ReadLine() ?? "y";
            }
        }
    }
}
