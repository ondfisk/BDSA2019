using Microsoft.EntityFrameworkCore;

namespace BDSA2019.Lecture07.Models.Bridge
{
    public class CharacterContext : DbContext, ICharacterContext
    {
        public CharacterContext(DbContextOptions<CharacterContext> options)
                : base(options)
        {
        }

        public DbSet<Character> Characters { get; private set; }
    }
}
