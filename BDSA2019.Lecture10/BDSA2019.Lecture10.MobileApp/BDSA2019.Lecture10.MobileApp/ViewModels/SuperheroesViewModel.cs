using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using BDSA2019.Lecture10.MobileApp.Models;
using BDSA2019.Lecture10.MobileApp.Views;
using BDSA2019.Lecture10.MobileApp.Services;
using System.Linq;

namespace BDSA2019.Lecture10.MobileApp.ViewModels
{
    public class SuperheroesViewModel : BaseViewModel
    {
        private readonly INavigationService _navigation;
        private readonly IMessagingCenter _messaging;
        private readonly IRestClient _client;

        public ObservableCollection<SuperheroListDTO> Items { get; } = new ObservableCollection<SuperheroListDTO>();

        private SuperheroListDTO _selectedItem;
        public SuperheroListDTO SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;

                if (_selectedItem == null)
                {
                    return;
                }

                ViewCommand.Execute(_selectedItem);
            }
        }

        public Command LoadCommand { get; set; }
        public Command NewCommand { get; set; }
        public Command ViewCommand { get; set; }

        public SuperheroesViewModel(INavigationService navigation, IMessagingCenter messaging, IRestClient client)
        {
            _navigation = navigation;
            _messaging = messaging;
            _client = client;

            Title = "Browse";

            LoadCommand = new Command(async () => await ExecuteLoadCommand());
            NewCommand = new Command(async () => await ExecuteNewCommand());
            ViewCommand = new Command(async o => await ExecuteViewCommand(o as SuperheroListDTO));

            _messaging.Subscribe<NewSuperheroPage, SuperheroListDTO>(this, "AddSuperhero", (obj, superhero) =>
            {
                Items.Add(superhero);
            });
            _messaging.Subscribe<EditSuperheroPage, SuperheroListDTO>(this, "UpdateSuperhero", (obj, superhero) =>
            {
                var existing = Items.FirstOrDefault(h => h.Id == superhero.Id);
                if (existing != null)
                {
                    Items.Remove(existing);
                }
                Items.Add(superhero);
            });
            _messaging.Subscribe<SuperheroDetailsPage, int>(this, "DeleteSuperhero", (obj, superheroId) =>
            {
                var existing = Items.FirstOrDefault(h => h.Id == superheroId);
                if (existing != null)
                {
                    Items.Remove(existing);
                }
            });
        }

        private async Task ExecuteLoadCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            Items.Clear();

            var items = await _client.GetAllAsync<SuperheroListDTO>("superheroes");

            foreach (var item in items)
            {
                Items.Add(item);
            }

            IsBusy = false;
        }

        private async Task ExecuteNewCommand()
        {
            await _navigation.NewAsync();
        }

        private async Task ExecuteViewCommand(SuperheroListDTO superhero)
        {
            SelectedItem = null;

            await _navigation.ViewAsync(superhero);
        }
    }
}