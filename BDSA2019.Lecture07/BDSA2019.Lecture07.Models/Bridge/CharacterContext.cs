using Microsoft.EntityFrameworkCore;

namespace BDSA2019.Lecture07.Models.Bridge
{
    public class CharacterContext : DbContext, ICharacterContext
    {
        private readonly string _connectionString;

        public CharacterContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<Character> Characters { get; private set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
