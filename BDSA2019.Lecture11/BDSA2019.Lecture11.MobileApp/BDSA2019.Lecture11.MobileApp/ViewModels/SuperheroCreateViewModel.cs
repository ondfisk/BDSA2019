using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BDSA2019.Lecture11.Shared;
using BDSA2019.Lecture11.MobileApp.Models;
using Xamarin.Forms;
using static BDSA2019.Lecture11.MobileApp.Models.Events;
using Microsoft.Identity.Client;
using System.Net;

namespace BDSA2019.Lecture11.MobileApp.ViewModels
{
    public class SuperheroCreateViewModel : BaseViewModel
    {
        private readonly INavigationService _navigation;
        private readonly IMessagingCenter _messaging;
        private readonly IRestClient _client;
        private readonly IDialogService _dialog;
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

        private string _powers;
        public string Powers
        {
            get { return _powers; }
            set { SetProperty(ref _powers, value); }
        }

        public ObservableCollection<Gender> GenderNames { get; } = new ObservableCollection<Gender> { Gender.Female, Gender.Male };

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public SuperheroCreateViewModel(INavigationService navigation, IMessagingCenter messaging, IRestClient client, IDialogService dialog)
        {
            _navigation = navigation;
            _messaging = messaging;
            _client = client;
            _dialog = dialog;

            Title = "New Superhero";

            SaveCommand = new Command(async () => await ExecuteSaveCommand(), () => !IsBusy);
            CancelCommand = new Command(async () => await ExecuteCancelCommand(), () => !IsBusy);
        }

        private async Task ExecuteSaveCommand()
        {
            IsBusy = true;

            var powers = Powers?.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()) ?? new string[0];

            var superhero = new SuperheroCreateDTO
            {
                Name = Name,
                AlterEgo = AlterEgo,
                Occupation = Occupation,
                CityName = CityName,
                PortraitUrl = PortraitUrl,
                BackgroundUrl = BackgroundUrl,
                FirstAppearance = FirstAppearance,
                Gender = Gender,
                Powers = new HashSet<string>(powers)
            };

            try
            {
                var (status, uri) = await _client.PostAsync("superheroes", superhero);

                if (status != HttpStatusCode.Created)
                {
                    await _dialog.DisplayAlertAsync("Error", $"Error from api: {status}", "OK");
                }
                else
                {
                    var id = int.Parse(uri.AbsoluteUri.Substring(uri.AbsoluteUri.LastIndexOf("/") + 1));

                    var superheroListDTO = new SuperheroListDTO
                    {
                        Id = id,
                        Name = Name,
                        AlterEgo = AlterEgo,
                        PortraitUrl = PortraitUrl
                    };

                    _messaging.Send(this, AddSuperhero, superheroListDTO);

                    await _navigation.CancelAsync();
                }
            }
            catch (Exception e)
            {
                await _dialog.DisplayAlertAsync(e);
            }
             
            IsBusy = false;
        }

        private async Task ExecuteCancelCommand()
        {
            IsBusy = true;

            await _navigation.CancelAsync();

            IsBusy = false;
        }
    }
}
