using System;
using System.Collections.Generic;

namespace BDSA2019.Lecture02
{
    public class DuckAgeComparer : IComparer<Duck>
    {
        public int Compare(Duck x, Duck y)
        {
            throw new NotImplementedException();
        }

        public static Comparison<Duck> Comparison => throw new NotImplementedException();
    }
}
