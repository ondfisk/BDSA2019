using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BDSA2019.Lecture10.MobileApp.Models;
using BDSA2019.Lecture10.MobileApp.Services;
using BDSA2019.Lecture10.MobileApp.Views;
using Xamarin.Forms;

namespace BDSA2019.Lecture10.MobileApp.ViewModels
{
    public class SuperheroDetailsViewModel : BaseViewModel
    {
        private readonly INavigationService _navigation;
        private readonly IMessagingCenter _messaging;
        private readonly IRestClient _client;

        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _alterEgo;
        public string AlterEgo
        {
            get { return _alterEgo; }
            set { SetProperty(ref _alterEgo, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _portraitUrl;
        public string PortraitUrl
        {
            get { return _portraitUrl; }
            set { SetProperty(ref _portraitUrl, value); }
        }

        private string _occupation;
        public string Occupation
        {
            get { return _occupation; }
            set { SetProperty(ref _occupation, value); }
        }

        private int? _cityId;
        public int? CityId
        {
            get { return _cityId; }
            set { SetProperty(ref _cityId, value); }
        }

        private string _cityName;
        public string CityName
        {
            get { return _cityName; }
            set { SetProperty(ref _cityName, value); }
        }

        private Gender _gender;
        public Gender Gender
        {
            get { return _gender; }
            set { SetProperty(ref _gender, value); }
        }

        private int? _firstAppearance;
        public int? FirstAppearance
        {
            get { return _firstAppearance; }
            set { SetProperty(ref _firstAppearance, value); }
        }

        private string _backgroundUrl;
        public string BackgroundUrl
        {
            get { return _backgroundUrl; }
            set { SetProperty(ref _backgroundUrl, value); }
        }

        private SuperheroDetailsDTO _superhero;
        private SuperheroDetailsDTO Superhero
        {
            get { return _superhero; }
            set
            {
                _superhero = value;

                Title = _superhero.AlterEgo;
                Id = _superhero.Id;
                Name = _superhero.Name;
                AlterEgo = _superhero.AlterEgo;
                PortraitUrl = _superhero.PortraitUrl; 
                Occupation = _superhero.Occupation;
                CityId = _superhero.CityId;
                CityName = _superhero.CityName;
                Gender = _superhero.Gender;
                FirstAppearance = _superhero.FirstAppearance;
                BackgroundUrl = _superhero.BackgroundUrl;

                Powers.Clear();
                foreach (var power in _superhero.Powers)
                {
                    Powers.Add(power);
                }
            }
        }

        public ObservableCollection<string> Powers { get; } = new ObservableCollection<string>();

        public Command LoadCommand { get; set; }
        public Command EditCommand { get; set; }
        public Command DeleteCommand { get; set; }

        public SuperheroDetailsViewModel(INavigationService navigation, IMessagingCenter messaging, IRestClient client)
        {
            _navigation = navigation;
            _messaging = messaging;
            _client = client;

            LoadCommand = new Command(async o => await ExecuteLoadCommand(o as SuperheroListDTO));
            EditCommand = new Command(async _ => await ExecuteEditCommand());
            DeleteCommand = new Command(async _ => await ExecuteDeleteCommand());

            _messaging.Subscribe<EditSuperheroPage, SuperheroDetailsDTO>(this, "UpdateSuperhero", (obj, superhero) =>
            {
                Superhero = superhero;
            });
        }

        private async Task ExecuteLoadCommand(SuperheroListDTO superhero)
        {
            Title = superhero.AlterEgo;
            Id = superhero.Id;
            Name = superhero.Name;
            AlterEgo = superhero.AlterEgo;
            PortraitUrl = superhero.PortraitUrl;

            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            Superhero = await _client.GetAsync<SuperheroDetailsDTO>($"superheroes/{Id}");

            IsBusy = false;
        }

        private async Task ExecuteEditCommand()
        {
            await _navigation.EditAsync(_superhero);
        }

        private async Task ExecuteDeleteCommand()
        {
            // TODO: Implement confirm dialog

            await _client.DeleteAsync($"superheroes/{Id}");

            await _navigation.BackAsync();
        }
    }
}
