using Microsoft.EntityFrameworkCore;

namespace BDSA2019.Lecture05.Entities
{
    public interface ISuperheroContext
    {
        DbSet<Superhero> Superheroes { get; set; }
        DbSet<City> Cities { get; set; }
        DbSet<Power> Powers { get; set; }
        DbSet<SuperheroPower> SuperheroPowers { get; set; }
        int SaveChanges();
    }
}
