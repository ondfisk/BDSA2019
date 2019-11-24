using BDSA2019.Lecture11.MobileApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using Xamarin.Forms;

namespace BDSA2019.Lecture11.MobileApp.Services
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
            serviceCollection.AddSingleton(_ => MessagingCenter.Instance);
            serviceCollection.AddScoped(_ => Application.Current.MainPage.Navigation);
            serviceCollection.AddScoped<IRestClient, RestClient>();
            serviceCollection.AddScoped<INavigationService, NavigationService>();
            serviceCollection.AddTransient<SuperheroesViewModel>();
            serviceCollection.AddTransient<SuperheroDetailsViewModel>();
            serviceCollection.AddTransient<SuperheroCreateViewModel>();
            serviceCollection.AddTransient<SuperheroUpdateViewModel>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
