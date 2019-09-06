using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace BDSA2019.Lecture02
{
    class Program
    {
        static void Main(string[] args)
        {
            // var evens = CollectionUtilities.GetEven(StreamNumbers());

            // foreach (var number in evens)
            // {
            //     Console.WriteLine(number);
            // }

            // var found = CollectionUtilities.Find(new[] { 1, 2, 3}, 2);

            // WriteLine(found);

            var ducks = Duck.Ducks.ToList();

            CollectionUtilities.Sort(ducks);

            foreach (var duck in ducks)
            {
                WriteLine(duck);
            }
        }

        static IEnumerable<int> StreamNumbers()
        {
            var i = 0;

            while (true)
            {
                yield return i++;
            }
        }
    }
}
