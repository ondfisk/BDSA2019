using System;
using Microsoft.EntityFrameworkCore;

namespace BDSA2019.Lecture06.Entities
{
    public class SuperheroContext : DbContext
    {
        public DbSet<Superhero> Superheroes { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Power> Powers { get; set; }
        public DbSet<SuperheroPower> SuperheroPowers { get; set; }

        public DbSet<AuditInfo> AuditInfo { get; set; }

        public SuperheroContext() { }

        public SuperheroContext(DbContextOptions<SuperheroContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=database.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Superhero>()
                        .Property(e => e.Gender)
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
        }
    }
}
