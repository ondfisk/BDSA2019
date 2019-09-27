using System.Linq;
using BDSA2019.Lecture05.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using static BDSA2019.Lecture05.Entities.Gender;
using static BDSA2019.Lecture05.Models.Response;

namespace BDSA2019.Lecture05.Models.Tests
{
    public partial class SuperheroRepositoryTests
    {
        [Fact]
        public void Delete_given_existing_id_calls_SaveChanges()
        {
            // Arrange
            var context = new Mock<ISuperheroContext>();
            context.Setup(s => s.Superheroes.Find(42)).Returns(new Superhero());
            var repository = new SuperheroRepository(context.Object);

            // Act
            repository.Delete(42);

            // Assert
            context.Verify(c => c.SaveChanges());
        }

        [Fact]
        public void Delete_given_existing_entity_removes_it()
        {
            // Arrange
            var hero = new Superhero();
            var context = new Mock<ISuperheroContext>();
            context.Setup(s => s.Superheroes.Find(42)).Returns(hero);
            var repository = new SuperheroRepository(context.Object);

            // Act
            repository.Delete(42);

            // Assert
            context.Verify(c => c.Superheroes.Remove(hero));
        }

        [Fact]
        public void Delete_given_id_when_exists_returns_Deleted()
        {
            // Arrange
            var context = new Mock<ISuperheroContext>();
            context.Setup(s => s.Superheroes.Find(42)).Returns(new Superhero());
            var repository = new SuperheroRepository(context.Object);

            // Act
            var response = repository.Delete(42);

            // Assert
            Assert.Equal(Deleted, response);
        }

        [Fact]
        public void Delete_given_id_when_not_does_not_exist_does_not_call_SaveChanges()
        {
            // Arrange
            var context = new Mock<ISuperheroContext>();
            context.Setup(s => s.Superheroes.Find(42)).Returns(default(Superhero));
            var repository = new SuperheroRepository(context.Object);

            // Act
            repository.Delete(42);

            // Assert
            context.Verify(c => c.SaveChanges(), Times.Never);
        }

        [Fact]
        public void Delete_given_id_when_not_does_not_exist_returns_NotFound()
        {
            // Arrange
            var context = new Mock<ISuperheroContext>();
            context.Setup(s => s.Superheroes.Find(42)).Returns(default(Superhero));
            var repository = new SuperheroRepository(context.Object);

            // Act
            var response = repository.Delete(42);

            // Assert
            Assert.Equal(NotFound, response);
        }

        [Fact]
        public void Update_given_no_entity_returns_NotFound()
        {
            var builder = new DbContextOptionsBuilder<SuperheroContext>().UseInMemoryDatabase(nameof(Update_given_no_entity_returns_NotFound));
            var context = new SuperheroContext(builder.Options);
            var repository = new SuperheroRepository(context);

            var superhero = new SuperheroUpdateDTO { Id = 42 };

            var response = repository.Update(superhero);

            Assert.Equal(NotFound, response);
        }

        [Fact]
        public void Update_given_existing_entity_updates_entity()
        {
            var builder = new DbContextOptionsBuilder<SuperheroContext>().UseInMemoryDatabase(nameof(Update_given_no_entity_returns_NotFound));
            var context = new SuperheroContext(builder.Options);
            var entity = new Superhero
            {
                Name = "Bruce Wayne",
                AlterEgo = "Batman"
            };
            context.Superheroes.Add(entity);
            context.SaveChanges();
            var repository = new SuperheroRepository(context);

            var superhero = new SuperheroUpdateDTO
            {
                Id = entity.Id,
                Name = "Clark Kent",
                AlterEgo = "Superman",
                CityName = "Metropolis"
            };

            var response = repository.Update(superhero);

            var updated = context.Superheroes
                .Include(c => c.City)
                .FirstOrDefault(c => c.Id == entity.Id);

            Assert.Equal("Clark Kent", updated.Name);
            Assert.Equal("Superman", updated.AlterEgo);
            Assert.Equal("Metropolis", updated.City.Name);
        }

        [Fact]
        public void Update_given_existing_entity_uses_existing_city()
        {
            var builder = new DbContextOptionsBuilder<SuperheroContext>().UseInMemoryDatabase(nameof(Update_given_no_entity_returns_NotFound));
            var context = new SuperheroContext(builder.Options);
            var entity = new Superhero
            {
                Name = "Bruce Wayne",
                AlterEgo = "Batman"
            };
            context.Superheroes.Add(entity);
            var entityCity = new City { Name = "Metropolis" };
            context.Cities.Add(entityCity);
            context.SaveChanges();
            var repository = new SuperheroRepository(context);

            var superhero = new SuperheroUpdateDTO
            {
                Id = entity.Id,
                Name = "Clark Kent",
                AlterEgo = "Superman",
                CityName = "Metropolis"
            };

            var response = repository.Update(superhero);

            var updated = context.Superheroes.Find(entity.Id);

            Assert.Equal("Clark Kent", updated.Name);
            Assert.Equal("Superman", updated.AlterEgo);
            Assert.Equal(entityCity.Id, updated.CityId);
        }
    }
}
