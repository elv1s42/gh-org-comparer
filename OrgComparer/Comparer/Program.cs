﻿using System;
using System.Collections.Generic;
using OrgComparer;

namespace Comparer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = "y";
            while (!input.Equals("n") && !input.Equals("N"))
            {
                DataGetter.GetOrgs();

                Console.WriteLine("Continue? (y/n)");
                input = Console.ReadLine() ?? "y";
            }
        }
    }
}
