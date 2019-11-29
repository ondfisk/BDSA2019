using BDSA2019.Lecture11.MobileApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using System;
using System.Net.Http;
using Xamarin.Forms;

namespace BDSA2019.Lecture11.MobileApp.Models
{
    public class IoCContainer
    {
        private static readonly Lazy<IServiceProvider> _lazyProvider = new Lazy<IServiceProvider>(() => ConjureServices());

        public static IServiceProvider Container { get => _lazyProvider.Value; }

        private static IServiceProvider ConjureServices()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            // Register services here
            var settings = new Settings();

            serviceCollection.AddSingleton<ISettings>(settings);
            serviceCollection.AddSingleton<HttpClient>();
            serviceCollection.AddTransient(_ => MessagingCenter.Instance);
            serviceCollection.AddTransient(_ => PublicClientApplicationBuilder.Create(settings.ClientId)
                    .WithTenantId(settings.TenantId)
                    .WithAuthority(AadAuthorityAudience.AzureAdMyOrg)
                    .WithRedirectUri($"msal{settings.ClientId}://auth")
                    .WithParentActivityOrWindow(() => App.ParentWindow)
                    .Build());
            serviceCollection.AddTransient(_ => Application.Current.MainPage.Navigation);
            serviceCollection.AddTransient<IDialogService>(_ => new DialogService(Application.Current.MainPage));
            serviceCollection.AddTransient<IRestClient, RestClient>();
            serviceCollection.AddTransient<IAuthenticationService, AuthenticationService>();
            serviceCollection.AddTransient<INavigationService, NavigationService>();
            serviceCollection.AddTransient<SuperheroesViewModel>();
            serviceCollection.AddTransient<SuperheroDetailsViewModel>();
            serviceCollection.AddTransient<SuperheroCreateViewModel>();
            serviceCollection.AddTransient<SuperheroUpdateViewModel>();
            serviceCollection.AddTransient<AboutViewModel>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
