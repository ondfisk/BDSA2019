using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BDSA2019.Lecture08.Entities
{
    public interface ISuperheroContext
    {
        DbSet<Superhero> Superheroes { get; set; }
        DbSet<City> Cities { get; set; }
        DbSet<Power> Powers { get; set; }
        DbSet<SuperheroPower> SuperheroPowers { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
