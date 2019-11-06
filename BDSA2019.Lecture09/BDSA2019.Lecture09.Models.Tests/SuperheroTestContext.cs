using System;
using System.Collections.Generic;
using System.Linq;
using BDSA2019.Lecture09.Entities;
using Microsoft.EntityFrameworkCore;
using static BDSA2019.Lecture09.Entities.Gender;

namespace BDSA2019.Lecture09.Models.Tests
{
    public class SuperheroTestContext : SuperheroContext
    {
        public SuperheroTestContext(DbContextOptions<SuperheroContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Superhero>()
                        .Property(e => e.Gender)
                        .HasMaxLength(50)
                        .HasConversion(
                            v => v.ToString(),
                            v => (Gender)Enum.Parse(typeof(Gender), v));

            modelBuilder.Entity<SuperheroPower>().HasKey(c => new { c.SuperheroId, c.PowerId });

            modelBuilder.Entity<City>()
                        .HasIndex(c => c.Name)
                        .IsUnique();

            modelBuilder.Entity<City>()
                        .HasMany(c => c.Superheroes)
                        .WithOne(s => s.City)
                        .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Power>()
                        .HasIndex(c => c.Name)
                        .IsUnique();

            modelBuilder.Entity<City>().HasData(
                new City { Id = 1, Name = "Metropolis" },
                new City { Id = 2, Name = "Gotham City" }
            );

            modelBuilder.Entity<Superhero>().HasData(
                new Superhero { Id = 1, Name = "Clark Kent", AlterEgo = "Superman", Occupation = "Reporter", Gender = Male, FirstAppearance = 1938, CityId = 1 },
                new Superhero { Id = 2, Name = "Bruce Wayne", AlterEgo = "Batman", Occupation = "CEO of Wayne Enterprises", Gender = Male, FirstAppearance = 1939, CityId = 2 },
                new Superhero { Id = 3, Name = "Selina Kyle", AlterEgo = "Catwoman", Occupation = "Thief", Gender = Female, FirstAppearance = 1940, CityId = 2 }
            );

            var powers = new[]
            {
                new Power { Id = 1, Name = "advanced technology"},
                new Power { Id = 2, Name = "brilliant deductive skills"},
                new Power { Id = 3, Name = "combat skill"},
                new Power { Id = 4, Name = "combat strategy"},
                new Power { Id = 5, Name = "exceptional martial artist"},
                new Power { Id = 6, Name = "flight"},
                new Power { Id = 7, Name = "freeze breath"},
                new Power { Id = 8, Name = "gymnastic ability"},
                new Power { Id = 9, Name = "healing factor"},
                new Power { Id = 10, Name = "heat vision"},
                new Power { Id = 11, Name = "inexhaustible wealth"},
                new Power { Id = 12, Name = "invulnerability"},
                new Power { Id = 13, Name = "super speed"},
                new Power { Id = 14, Name = "super strength"},
                new Power { Id = 15, Name = "superhuman hearing"},
                new Power { Id = 16, Name = "x-ray vision"}
            };

            modelBuilder.Entity<Power>().HasData(
                powers
            );

            var dictionary = powers.ToDictionary(p => p.Name, p => p.Id);

            ICollection<SuperheroPower> convertToSuperheroPowers(int superheroId, params string[] powers)
            {
                var projected = from p in powers
                                let powerId = dictionary[p]
                                select new SuperheroPower { SuperheroId = superheroId, PowerId = powerId };

                return projected.ToList();
            }

            var superheroPowers = new[]
            {
                convertToSuperheroPowers(1, new[] { "super strength", "flight", "invulnerability", "super speed", "heat vision", "freeze breath", "x-ray vision", "superhuman hearing", "healing factor" }),
                convertToSuperheroPowers(2, new[] { "exceptional martial artist", "combat strategy", "inexhaustible wealth", "brilliant deductive skills", "advanced technology" }),
                convertToSuperheroPowers(3, new[] { "exceptional martial artist", "gymnastic ability", "combat skill" }),
            };

            modelBuilder.Entity<SuperheroPower>().HasData(superheroPowers.SelectMany(p => p));
        }
    }
}
