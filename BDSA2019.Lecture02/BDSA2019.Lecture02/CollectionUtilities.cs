using System;
using System.Collections.Generic;

namespace BDSA2019.Lecture02
{
    public class CollectionUtilities
    {
        public static IEnumerable<int> GetEven(IEnumerable<int> list)
        {
            foreach (var number in list)
            {
                if (number % 2 == 0)
                {
                    yield return number;

                    if (number == 42)
                    {
                        yield break;
                    }
                }
            }
        }

        public static bool Find(int[] list, int number)
        {
            foreach (var i in list)
            {
                if (i == number)
                {
                    return true;
                }
            }
            return false;
        }

        public static ISet<int> Unique(IEnumerable<int> numbers)
        {
            return new HashSet<int>(numbers);
        }

        public static IEnumerable<int> Reverse(IEnumerable<int> numbers)
        {
            throw new NotImplementedException();
        }

        public static void Sort(List<Duck> ducks, IComparer<Duck> comparer = null)
        {
            ducks.Sort(new DuckAgeComparer());
        }

        public static IDictionary<int, Duck> ToDictionary(IEnumerable<Duck> ducks)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Duck> GetOlderThan(IEnumerable<Duck> ducks, int age)
        {
            throw new NotImplementedException();
        }
    }
}
