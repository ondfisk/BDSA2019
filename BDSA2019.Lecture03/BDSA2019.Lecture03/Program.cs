using System;
using System.Linq;
using BDSA2019.Lecture03.Models;

namespace BDSA2019.Lecture03
{
    class Program
    {
        delegate int BinaryOperation(int x, int y);

        static void Main(string[] args)
        {
        }

        public static int Add(int x, int y) => checked(x + y);
    }
}
