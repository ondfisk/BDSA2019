using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDSA2019.Lecture09.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;
using static BDSA2019.Lecture09.Entities.Gender;
using static BDSA2019.Lecture09.Models.Response;

namespace BDSA2019.Lecture09.Models.Tests
{
    public partial class SuperheroRepositoryTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly SuperheroContext _context;
        private readonly SuperheroRepository _repository;

        public SuperheroRepositoryTests()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
            var builder = new DbContextOptionsBuilder<SuperheroContext>().UseSqlite(_connection);
            _context = new SuperheroTestContext(builder.Options);
            _context.Database.EnsureCreated();
            _repository = new SuperheroRepository(_context);
        }

        [Fact]
        public async Task CreateAsync_returns_Created()
        {
            var hero = new SuperheroCreateDTO
            {
                Name = "Diana",
                AlterEgo = "Wonder Woman"
            };

            var (response, _) = await _repository.CreateAsync(hero);

            Assert.Equal(Created, response);
        }

        [Fact]
        public async Task CreateAsync_creates_a_hero_with_basic_properties()
        {
            var hero = new SuperheroCreateDTO
            {
                Name = "Diana",
                AlterEgo = "Wonder Woman",
                Occupation = "Amazon Princess",
                Gender = Female,
                FirstAppearance = 1941
            };

            var (_, id) = await _repository.CreateAsync(hero);

            var created = await _context.Superheroes.FindAsync(id);

            Assert.Equal(4, created.Id);
            Assert.Equal("Diana", created.Name);
            Assert.Equal("Wonder Woman", created.AlterEgo);
            Assert.Equal("Amazon Princess", created.Occupation);
            Assert.Equal(Female, created.Gender);
            Assert.Equal(1941, created.FirstAppearance);
        }

        [Fact]
        public async Task CreateAsync_creates_a_hero_with_new_city()
        {
            var hero = new SuperheroCreateDTO
            {
                Name = "Diana",
                AlterEgo = "Wonder Woman",
                CityName = "Themyscira"
            };

            var (_, id) = await _repository.CreateAsync(hero);

            var created = await _context.Superheroes.Include(c => c.City).FirstOrDefaultAsync(c => c.Id == id);

            Assert.Equal(3, created.CityId);
            Assert.Equal(3, created.City.Id);
            Assert.Equal("Themyscira", created.City.Name);
        }

        [Fact]
        public async Task CreateAsync_creates_a_hero_with_existing_city()
        {
            var hero = new SuperheroCreateDTO
            {
                Name = "Kate Kane",
                AlterEgo = "Batwoman",
                CityName = "Gotham City"
            };

            var (_, id) = await _repository.CreateAsync(hero);

            var created = await _context.Superheroes.FindAsync(id);

            Assert.Equal(2, created.CityId);
        }

        [Fact]
        public async Task CreateAsync_creates_a_hero_with_powers()
        {
            var hero = new SuperheroCreateDTO
            {
                Name = "Diana",
                AlterEgo = "Wonder Woman",
                Powers = new HashSet<string> { "super strength", "invulnerability", "flight", "combat skill", "combat strategy", "superhuman agility", "healing factor", "magic weaponry" }
            };

            var (_, id) = await _repository.CreateAsync(hero);

            var powers = from p in _context.SuperheroPowers
                         where p.SuperheroId == id
                         select p.Power.Name;

            Assert.True(hero.Powers.SetEquals(powers));
        }

        [Fact]
        public async Task ReadAsync_given_existing_id_returns_a_hero_with_all_properties()
        {
            var batman = await _repository.ReadAsync(2);

            Assert.Equal(2, batman.Id);
            Assert.Equal("Bruce Wayne", batman.Name);
            Assert.Equal("Batman", batman.AlterEgo);
            Assert.Equal("CEO of Wayne Enterprises", batman.Occupation);
            Assert.Equal(Male, batman.Gender);
            Assert.Equal(1939, batman.FirstAppearance);
            Assert.Equal(2, batman.CityId);
            Assert.Equal("Gotham City", batman.CityName);
            Assert.True(batman.Powers.SetEquals(new[] { "exceptional martial artist", "combat strategy", "inexhaustible wealth", "brilliant deductive skills", "advanced technology" }));
        }

        [Fact]
        public async Task ReadAsync_given_non_existing_id_returns_null()
        {
            var hero = await _repository.ReadAsync(42);

            Assert.Null(hero);
        }

        [Fact]
        public async Task ReadAsync_returns_all_heroes_sorted_by_AlterEgo()
        {
            var heroes = await _repository.ReadAsync();

            Assert.Equal(new[] { "Batman", "Catwoman", "Superman" }, heroes.Select(s => s.AlterEgo));
        }

        [Fact]
        public async Task ReadAsync_returns_all_heroes_with_properties()
        {
            var heroes = await _repository.ReadAsync();

            var batman = heroes.FirstOrDefault();

            Assert.Equal(2, batman.Id);
            Assert.Equal("Bruce Wayne", batman.Name);
            Assert.Equal("Batman", batman.AlterEgo);
        }

        [Fact]
        public async Task UpdateAsync_given_non_existing_returns_NotFound()
        {
            var hero = new SuperheroUpdateDTO
            {
                Id = 42
            };

            var response = await _repository.UpdateAsync(hero);

            Assert.Equal(NotFound, response);
        }

        [Fact]
        public async Task UpdateAsync_updates_a_hero_with_basic_properties()
        {
            var hero = new SuperheroUpdateDTO
            {
                Id = 1,
                Name = "Diana",
                AlterEgo = "Wonder Woman",
                Occupation = "Amazon Princess",
                Gender = Female,
                FirstAppearance = 1941
            };

            await _repository.UpdateAsync(hero);

            var updated = _context.Superheroes.Find(1);

            Assert.Equal(1, updated.Id);
            Assert.Equal("Diana", updated.Name);
            Assert.Equal("Wonder Woman", updated.AlterEgo);
            Assert.Equal("Amazon Princess", updated.Occupation);
            Assert.Equal(Female, updated.Gender);
            Assert.Equal(1941, updated.FirstAppearance);
        }

        [Fact]
        public async Task UpdateAsync_updates_a_hero_with_new_city()
        {
            var hero = new SuperheroUpdateDTO
            {
                Id = 1,
                Name = "Superman",
                AlterEgo = "Superman",
                CityName = "New York City"
            };

            await _repository.UpdateAsync(hero);

            var updated = await _context.Superheroes.Include(c => c.City).FirstOrDefaultAsync(c => c.Id == 1);

            Assert.Equal(3, updated.CityId);
            Assert.Equal(3, updated.City.Id);
            Assert.Equal("New York City", updated.City.Name);
        }

        [Fact]
        public async Task UpdateAsync_updates_a_hero_with_existing_city()
        {
            var hero = new SuperheroUpdateDTO
            {
                Id = 1,
                Name = "Superman",
                AlterEgo = "Superman",
                CityName = "Gotham City"
            };

            await _repository.UpdateAsync(hero);

            var updated = _context.Superheroes.Find(1);

            Assert.Equal(2, updated.CityId);
        }

        [Fact]
        public async Task UpdateAsync_updates_a_hero_with_powers()
        {
            var hero = new SuperheroUpdateDTO
            {
                Id = 1,
                Name = "Diana",
                AlterEgo = "Wonder Woman",
                Powers = new HashSet<string> { "super strength", "invulnerability", "flight", "combat skill", "combat strategy", "superhuman agility", "healing factor", "magic weaponry" }
            };

            await _repository.UpdateAsync(hero);

            var powers = from p in _context.SuperheroPowers
                         where p.SuperheroId == 1
                         select p.Power.Name;

            Assert.True(hero.Powers.SetEquals(powers));
        }

        [Fact]
        public async Task DeleteAsync_given_existing_deletes_a_hero()
        {
            await _repository.DeleteAsync(1);

            Assert.Null(_context.Superheroes.Find(1));
        }

        [Fact]
        public async Task DeleteAsync_given_existing_returns_Deleted()
        {
            var response = await _repository.DeleteAsync(1);

            Assert.Equal(Deleted, response);
        }

        [Fact]
        public async Task DeleteAsync_given_non_existing_returns_NotFound()
        {
            var response = await _repository.DeleteAsync(42);

            Assert.Equal(NotFound, response);
        }

        public void Dispose()
        {
            _context.Dispose();
            _connection.Dispose();
        }
    }
}
