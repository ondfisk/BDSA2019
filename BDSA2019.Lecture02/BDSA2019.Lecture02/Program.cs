using System;
using System.Collections.Generic;
using System.Linq;

namespace BDSA2019.Lecture02
{
    class Program
    {
        static void Main(string[] args)
        {
            // var evens = CollectionUtilities.GetEven(StreamNumbers());

            // Console.WriteLine(evens.Count);
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
