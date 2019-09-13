using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using BDSA2019.Lecture03.Models;
using static BDSA2019.Lecture03.Models.Gender;

namespace BDSA2019.Lecture03
{
    class Program
    {
        delegate double UnaryOperation(double x);

        static void Main(string[] args)
        {
            // Func<int, bool> notNumber42 = x => x != 42;
            // Predicate<int> number42 = x => x == 42;

            // Action<string> print = Console.WriteLine;

            // double power(double x)
            // {
            //     return Math.Pow(x, 2);
            // }

            // var number = Calc(power, Math.Sqrt(42));

            // print(number.ToString());

            // var gotham = new City();
            // gotham.Id = 42;
            // gotham.Name = "Gotham City";

            // var metropolis = new City(42, "Metropolis");

            // var centralCity = new City
            // {
            //     Id = 42,
            //     Name = "Central City"
            // };

            // var atlantis = new
            // {
            //     Id = 42,
            //     Name = "Atlantis"
            // };

            // DoUglyStuff(atlantis);

            IEnumerable<int> numbers = Enumerable.Range(0, 43);
            // "the question".Print();
            // 42.Print();
            // numbers.Print();

            //numbers.Where(x => x % 2 == 0).Print();

            // numbers.Where(x => x % 2 == 0)
            //        .Where(x => x > 20)
            //        .OrderBy(x => -x)
            //        .Select(x => -x)
            //        .Print();

            // Console.WriteLine();

            // var evenReversed = from x in numbers
            //                    orderby -x
            //                    select x;

            // var greaterThan20 = from x in evenReversed
            //                     where x > 20
            //                     select x;

            // var evenGreaterThan20 = greaterThan20.Where(x => x % 2 == 0);

            // evenGreaterThan20.Print();

            var repo = new Repository();

            var heroes = repo.Superheroes;

            // var female = from h in heroes
            //              let p = string.Join(", ", h.Powers)
            //              where h.Powers.Contains("combat strategy")
            //              select new { h.Name, h.AlterEgo, Powers = p };

            // female.Print();

            // var h2 = from h in heroes
            //          join c in repo.Cities on h.CityId equals c.Id
            //          orderby c.Name descending, h.AlterEgo
            //          select (name: h.AlterEgo, city: c.Name);

            // h2.Print();

            var h2 = heroes
                        .Join(repo.Cities, 
                        h => h.CityId, 
                        c => c.Id, 
                        (h, c) => (h.AlterEgo, c.Name));

            //h2.Print();

            // var cities = repo.Cities;

            // var group1 = from h in heroes
            //             from c in cities
            //             where c.Id == h.CityId
            //             select new { Name = h.AlterEgo, City = c.Name };

            // var group2 = from x in heroes
            //              group x by new { Power = x.Powers.First() } into g
            //              select new 
            //              {
            //                  Power = g.Key,
            //                  Count = g.Count()
            //              };
           
            // group2.Print();

                    //     var text = File.ReadAllText("Hamlet.txt");

            var text = File.ReadAllText("Hamlet.txt");
            var words = Regex.Split(text, @"\P{L}+");

            var histogram = from w in words
                            group w by w.ToLowerInvariant() into h
                            let c = h.Count()
                            orderby c descending
                            select new { Word = h.Key, Count = c };

            var dict = histogram.ToLookup(x => x.Count);

            dict[42].Print();
        }

        static double Calc(Func<double, double, double> op, double x, double y)
        {
            return op(x, y);
        }

        static double Calc(Func<double, double> op, double x)
        {
            return op(x);
        }

        public static double Add(double x, double y) => checked(x + y);
    }
}
