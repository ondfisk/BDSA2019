using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BDSA2019.Lecture03
{
    public static class Extensions
    {
        public static void Print<T>(this T stuff)
        {
            if (stuff is string)
            {
                Console.WriteLine(stuff);
                return;
            }

            var numbers = stuff as IEnumerable<int>;

            if (numbers != null)
            {
                var str = string.Join(", ", numbers);

                Console.WriteLine(str);

                return;
            }

            var enumerable = stuff as IEnumerable;

            if (enumerable != null)
            {
                foreach (var s in enumerable)
                {
                    Console.WriteLine(s);
                }
                return;
            }

            Console.WriteLine(stuff);
        }
    }
}
