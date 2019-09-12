using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BDSA2019.Lecture03
{
    static class Notes
    {
        public static void Hamlet()
        {
            var text = File.ReadAllText("Hamlet.txt");

            var words = Regex.Split(text, @"\P{L}+");

            var histogram = from w in words
                            group w by w into h
                            let c = h.Count()
                            orderby c descending
                            select new { Word = h.Key, Count = c };

            histogram.Take(5).Print();

            // Dictionary
            // LO
        }

        public static void Print<T>(this T stuff)
        {
            var enumerable = stuff as IEnumerable;

            if (enumerable != null)
            {
                foreach (var item in enumerable)
                {
                    Console.WriteLine(item);
                }
            }
            else
            {
                Console.WriteLine(stuff);
            }
        }
    }
}
