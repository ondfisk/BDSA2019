using BDSA2019.Lecture10.MobileApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Net.Http;

namespace BDSA2019.Lecture10.MobileApp.Services
{
    public class IoCContainer
    {
        private static readonly Lazy<IServiceProvider> _lazyProvider = new Lazy<IServiceProvider>(() => ConjureServices());

        public static IServiceProvider Container { get => _lazyProvider.Value; }

        private static IServiceProvider ConjureServices()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            // Register services here
            var client = new HttpClient { BaseAddress = App.BackendUrl };
            serviceCollection.AddSingleton(client);
            serviceCollection.AddScoped<IRestClient, RestClient>();
            serviceCollection.AddTransient<SuperheroesViewModel>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
