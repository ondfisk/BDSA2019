using BDSA2019.Lecture07.Models.Bridge;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Cryptography;

namespace BDSA2019.Lecture07.App
{
    public class IoCContainer
    {
        private static readonly Lazy<IServiceProvider> _lazyProvider = new Lazy<IServiceProvider>(() => ConfigureServices());

        public static IServiceProvider Container { get => _lazyProvider.Value; }

        private static IServiceProvider ConfigureServices()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            // Register services here

            return serviceCollection.BuildServiceProvider();
        }
    }
}
