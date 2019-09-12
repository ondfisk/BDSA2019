using System;

namespace BDSA2019.Lecture01.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var name = args.Length > 0 ? args[0] : "World";

            Console.Write($"Hello {name}!");
        }
    }
}
