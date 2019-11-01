using BDSA2019.Lecture07.App;
using BDSA2019.Lecture08.Entities;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace BDSA2019.Lecture08
{
    /// <summary>
    /// Factory enable running Update-Database -Project BDSA2019.Lecture09.Entities -StartupProject BDSA2019.Lecture08
    /// </summary>
    public class SuperheroContextFactory : IDesignTimeDbContextFactory<SuperheroContext>
    {
        public SuperheroContext CreateDbContext(string[] args)
        {
            var container = IoCContainer.Container;

            return container.GetService<SuperheroContext>();
        }
    }
}
