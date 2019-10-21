using System;

namespace BDSA2019.Lecture07.Models.FactoryMethod
{
    public static class Game
    {
        public static void Run()
        {
            Console.Clear();

            var factory = new WeaponFactory();

            IWeapon weapon = null;

            do
            {
                Console.WriteLine("Please choose your weapon");

                foreach (var available in factory.Available())
                {
                    Console.WriteLine($"- {available}");
                }

                var input = Console.ReadLine();

                weapon = factory.Make(input);
            }
            while (weapon == null);

            Console.WriteLine("You have chosen wisely...");

            Console.WriteLine($"A {weapon.Name} with damage {weapon.Damage} and range {weapon.Range}");

            Console.WriteLine();

            Console.Write("Try again [y/n] ");

            var key = Console.ReadKey();
            Console.WriteLine();

            if (key.KeyChar == 'y')
            {
                Run();
            }
        }
    }
}
