using Microsoft.EntityFrameworkCore;

namespace BDSA2019.Lecture07.Entities
{
    public class FuturamaContext : DbContext, IFuturamaContext
    {
        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Character> Characters { get; set; }
        public virtual DbSet<Episode> Episodes { get; set; }
        public virtual DbSet<EpisodeCharacter> EpisodeCharacters { get; set; }

        public FuturamaContext(DbContextOptions<FuturamaContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EpisodeCharacter>()
                .HasKey(e => new { e.EpisodeId, e.CharacterId });
        }
    }
}
