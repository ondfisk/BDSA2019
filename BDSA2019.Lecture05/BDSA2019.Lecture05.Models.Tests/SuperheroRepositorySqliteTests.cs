using System;
using System.Linq;
using BDSA2019.Lecture05.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using static BDSA2019.Lecture05.Entities.Gender;
using static BDSA2019.Lecture05.Models.Response;

namespace BDSA2019.Lecture05.Models.Tests
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
        public void Create_creates_a_hero()
        {
            var hero = new SuperheroCreateDTO
            {
                Name = "Kara Zor-El",
                AlterEgo = "Supergirl",
                Occupation = "Actress",
                Gender = Female,
                FirstAppearance = 1959,
                CityName = "New York City"
            };

            var (_, id) = _repository.Create(hero);

            var created = _context.Superheroes.Find(id);

            Assert.Equal(9, created.Id);
            Assert.Equal("Kara Zor-El", created.Name);
            Assert.Equal("Supergirl", created.AlterEgo);
            Assert.Equal("Actress", created.Occupation);
            Assert.Equal(Female, created.Gender);
            Assert.Equal(1959, created.FirstAppearance);
            Assert.Equal(5, created.CityId);
        }

        [Fact]
        public void Delete_deletes()
        {
            var response = _repository.Delete(8);

            Assert.Equal(Deleted, response);
            Assert.Null(_context.Superheroes.Find(8));
        }

        public void Dispose()
        {
            _context.Dispose();
            _connection.Dispose();
        }
    }
}
