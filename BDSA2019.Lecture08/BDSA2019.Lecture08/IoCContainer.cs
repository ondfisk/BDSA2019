using BDSA2019.Lecture08.Entities;
using BDSA2019.Lecture08.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BDSA2019.Lecture07.App
{
    public class IoCContainer
    {
        private static readonly Lazy<IServiceProvider> _lazyProvider = new Lazy<IServiceProvider>(() => ConjureServices());

        public static IServiceProvider Container { get => _lazyProvider.Value; }

        private static IServiceProvider ConjureServices()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                      .AddJsonFile("appsettings.json")
                      .Build();

            IServiceCollection serviceCollection = new ServiceCollection();

            // Register services here

            var connectionString = configuration.GetConnectionString("Superheroes");

            serviceCollection.AddDbContext<SuperheroContext>(o => o.UseSqlServer(connectionString));
            serviceCollection.AddScoped<ISuperheroContext, SuperheroContext>();
            serviceCollection.AddScoped<ISuperheroRepository, SuperheroRepository>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
