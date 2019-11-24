using BDSA2019.Lecture11.Shared;
using BDSA2019.Lecture11.MobileApp.Views;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BDSA2019.Lecture11.MobileApp.Services
{
    public class NavigationService : INavigationService
    {
        private readonly INavigation _navigation;

        public NavigationService(INavigation navigation)
        {
            _navigation = navigation;
        }

        public async Task BackAsync()
        {
            await _navigation.PopAsync();
        }        

        public async Task NewAsync()
        {
            await _navigation.PushModalAsync(new NavigationPage(new NewSuperheroPage()));
        }

        public async Task EditAsync(SuperheroDetailsDTO superhero)
        {
            await _navigation.PushModalAsync(new NavigationPage(new EditSuperheroPage(superhero)));
        }

        public async Task ViewAsync(SuperheroListDTO superhero)
        {
            await _navigation.PushAsync(new SuperheroDetailsPage(superhero));
        }

        public async Task CancelAsync()
        {
            await _navigation.PopModalAsync();
        }
    }
}
