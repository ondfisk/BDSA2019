using System;
using BDSA2019.Lecture05.Entities;
using BDSA2019.Lecture05.Models;

namespace BDSA2019.Lecture05.App
{
    class Program
    {
        static void Main(string[] args)
        {
            using var context = new SuperheroContext();
            var repository = new SuperheroRepository(context);

            foreach (var hero in repository.Read())
            {
                Console.WriteLine($"{hero.Id}: {hero.Name} aka {hero.AlterEgo}");
            }

            Console.Write("Enter superhero id: ");
            var id = int.Parse(Console.ReadLine());

            var superhero = repository.Read(id);

            Console.WriteLine($"Name: {superhero.Name}"); 
            Console.WriteLine($"Alter Ego: {superhero.AlterEgo}"); 
            Console.WriteLine($"Occupation: {superhero.Occupation}"); 
            Console.WriteLine($"City: {superhero.CityName}"); 
            Console.WriteLine($"Gender: {superhero.Gender}"); 
            Console.WriteLine($"First Appearance: {superhero.FirstAppearance}"); 
            Console.WriteLine($"Powers: {string.Join(", ", superhero.Powers)}");
        }
    }
}
