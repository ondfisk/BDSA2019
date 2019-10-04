using BDSA2019.Lecture07.Entities;
using BDSA2019.Lecture07.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BDSA2019.Lecture07.Tests
{
    public class CharacterRepositoryTests
    {
        [Fact]
        public async Task CreateAsync_given_dto_creates_new_Character()
        {
            using (var connection = await CreateConnectionAsync())
            using (var context = await CreateContextAsync(connection))
            {
                context.Actors.Add(new Actor { Name = "John DiMaggio" });
                await context.SaveChangesAsync();

                var repository = new CharacterRepository(context);
                var dto = new CharacterCreateUpdateDTO
                {
                    ActorId = 1,
                    Name = "Bender",
                    Species = "Robot",
                    Planet = "Earth"
                };

                var id = await repository.CreateAsync(dto);

                Assert.Equal(1, id);

                var entity = await context.Characters.FindAsync(1);

                Assert.Equal(1, entity.ActorId);
                Assert.Equal("Bender", entity.Name);
                Assert.Equal("Robot", entity.Species);
                Assert.Equal("Earth", entity.Planet);
            }
        }

        [Fact]
        public async Task FindAsync_given_id_exists_returns_dto()
        {
            using (var connection = await CreateConnectionAsync())
            using (var context = await CreateContextAsync(connection))
            {
                var entity = new Character
                {
                    Name = "Fry",
                    Species = "Human",
                    Planet = "Earth",
                    Actor = new Actor { Name = "Billy West" },
                    EpisodeCharacters = new[]
                    {
                        new EpisodeCharacter { Episode = new Episode { Title = "Space Pilot 3000" } },
                        new EpisodeCharacter { Episode = new Episode { Title = "The Series Has Landed" } }
                    }
                };
                context.Characters.Add(entity);
                await context.SaveChangesAsync();

                var repository = new CharacterRepository(context);

                var character = await repository.FindAsync(1);

                Assert.Equal(1, character.Id);
                Assert.Equal("Fry", character.Name);
                Assert.Equal("Human", character.Species);
                Assert.Equal("Earth", character.Planet);
                Assert.Equal(1, character.ActorId);
                Assert.Equal("Billy West", character.ActorName);
                Assert.Equal(2, character.NumberOfEpisodes);
            }
        }

        [Fact]
        public async Task Read_returns_projection_of_all_characters()
        {
            using (var connection = await CreateConnectionAsync())
            using (var context = await CreateContextAsync(connection))
            {
                var episode1 = new Episode { Title = "Space Pilot 3000" };
                var episode2 = new Episode { Title = "The Series Has Landed" };
                context.Episodes.AddRange(episode1, episode2);

                var entity = new Character
                {
                    Name = "Fry",
                    Species = "Human",
                    Planet = "Earth",
                    Actor = new Actor { Name = "Billy West" },
                    EpisodeCharacters = new[]
                    {
                        new EpisodeCharacter { Episode = new Episode { Title = "Space Pilot 3000" } },
                        new EpisodeCharacter { Episode = new Episode { Title = "The Series Has Landed" } }
                    }
                };
                context.Characters.Add(entity);
                await context.SaveChangesAsync();

                var repository = new CharacterRepository(context);

                var characters = repository.Read();

                var character = await characters.SingleAsync();

                Assert.Equal(1, character.Id);
                Assert.Equal("Fry", character.Name);
                Assert.Equal("Human", character.Species);
                Assert.Equal("Earth", character.Planet);
                Assert.Equal(1, character.ActorId);
                Assert.Equal("Billy West", character.ActorName);
                Assert.Equal(2, character.NumberOfEpisodes);
            }
        }

        [Fact]
        public async Task UpdateAsync_given_non_existing_dto_returns_false()
        {
            using (var connection = await CreateConnectionAsync())
            using (var context = await CreateContextAsync(connection))
            {
                context.Actors.Add(new Actor { Name = "John DiMaggio" });
                await context.SaveChangesAsync();

                var repository = new CharacterRepository(context);
                var dto = new CharacterCreateUpdateDTO
                {
                    Id = 1,
                    ActorId = 1,
                    Name = "Bender",
                    Species = "Robot",
                    Planet = "Earth"
                };

                var updated = await repository.UpdateAsync(dto);

                Assert.False(updated);
            }
        }

        [Fact]
        public async Task UpdateAsync_given_existing_dto_updates_entity()
        {
            using (var connection = await CreateConnectionAsync())
            using (var context = await CreateContextAsync(connection))
            {
                context.Actors.Add(new Actor { Name = "John DiMaggio" });
                await context.SaveChangesAsync();

                context.Characters.Add(new Character { Name = "Fry", Species = "Human" });
                await context.SaveChangesAsync();

                var repository = new CharacterRepository(context);
                var dto = new CharacterCreateUpdateDTO
                {
                    Id = 1,
                    ActorId = 1,
                    Name = "Bender",
                    Species = "Robot",
                    Planet = "Earth"
                };

                var updated = await repository.UpdateAsync(dto);

                Assert.True(updated);

                var entity = await context.Characters.FindAsync(1);

                Assert.Equal(1, entity.ActorId);
                Assert.Equal("Bender", entity.Name);
                Assert.Equal("Robot", entity.Species);
                Assert.Equal("Earth", entity.Planet);
            }
        }

        [Fact]
        public async Task DeleteAsync_given_id_not_exists_return_false()
        {
            var mock = new Mock<IFuturamaContext>();
            mock.Setup(s => s.Characters.FindAsync(42)).ReturnsAsync(default(Character));

            var repository = new CharacterRepository(mock.Object);

            var deleted = await repository.DeleteAsync(42);

            Assert.False(deleted);
        }

        [Fact]
        public async Task DeleteAsync_given_id_exists_character_is_removed_from_context()
        {
            var entity = new Character();
            var mock = new Mock<IFuturamaContext>();
            mock.Setup(s => s.Characters.FindAsync(42)).ReturnsAsync(entity);

            var repository = new CharacterRepository(mock.Object);

            await repository.DeleteAsync(42);

            mock.Verify(s => s.Characters.Remove(entity));
        }

        [Fact]
        public async Task DeleteAsync_given_id_exists_context_SaveChanges()
        {
            var entity = new Character();
            var mock = new Mock<IFuturamaContext>();
            mock.Setup(s => s.Characters.FindAsync(42)).ReturnsAsync(entity);

            var repository = new CharacterRepository(mock.Object);

            await repository.DeleteAsync(42);

            mock.Verify(s => s.SaveChangesAsync(default));
        }

        [Fact]
        public async Task DeleteAsync_given_id_exists_return_true()
        {
            var entity = new Character();
            var mock = new Mock<IFuturamaContext>();
            mock.Setup(s => s.Characters.FindAsync(42)).ReturnsAsync(entity);

            var repository = new CharacterRepository(mock.Object);

            var deleted = await repository.DeleteAsync(42);

            Assert.True(deleted);
        }

        private async Task<DbConnection> CreateConnectionAsync()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            await connection.OpenAsync();

            return connection;
        }

        private async Task<IFuturamaContext> CreateContextAsync(DbConnection connection)
        {
            var builder = new DbContextOptionsBuilder<FuturamaContext>()
                              .UseSqlite(connection);

            var context = new FuturamaContext(builder.Options);
            await context.Database.EnsureCreatedAsync();

            return context;
        }
    }
}
