using System;
using System.Collections.Generic;

namespace BDSA2019.Lecture07.Models.Facade
{
    public class Notifier
    {
        public void Notify(Article article, IEnumerable<Person> people)
        {
            Console.WriteLine("Notifying:");
            foreach (var person in people)
            {
                Console.WriteLine($"- {person.Name}");
            }
        }
    }
}
