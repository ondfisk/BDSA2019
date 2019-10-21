using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BDSA2019.Lecture07.Models.Bridge
{   
    public interface ICharacterContext : IDisposable
    {
        DbSet<Character> Characters { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
