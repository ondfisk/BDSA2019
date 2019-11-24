using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using BDSA2019.Lecture11.Shared;
using BDSA2019.Lecture11.MobileApp.Services;
using System.Linq;
using static BDSA2019.Lecture11.MobileApp.Services.Events;

namespace BDSA2019.Lecture11.MobileApp.ViewModels
{
    public class SuperheroesViewModel : BaseViewModel
    {
        private const string Message = UpdateSuperhero;
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
                SetProperty(ref _selectedItem, value);
                if (value != null)
                {
                    ViewCommand.Execute(null);
                    SelectedItem = null;
                }
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

            LoadCommand = new Command(async () => await ExecuteLoadCommand(), () => !IsBusy);
            NewCommand = new Command(async () => await ExecuteNewCommand(), () => !IsBusy);
            ViewCommand = new Command(async () => await ExecuteViewCommand(), () => !IsBusy);

            _messaging.Subscribe<SuperheroCreateViewModel, SuperheroListDTO>(this, AddSuperhero, (obj, superhero) =>
            {
                Items.Add(superhero);
            });
            _messaging.Subscribe<SuperheroUpdateViewModel, SuperheroListDTO>(this, Message, (obj, superhero) =>
            {
                var existing = Items.FirstOrDefault(h => h.Id == superhero.Id);
                if (existing != null)
                {
                    Items.Remove(existing);
                }
                Items.Add(superhero);
            });
            _messaging.Subscribe<SuperheroDetailsViewModel, int>(this, DeleteSuperhero, (obj, superheroId) =>
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

        private async Task ExecuteViewCommand()
        {
            await _navigation.ViewAsync(SelectedItem);
        }
    }
}