using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using BDSA2019.Lecture11.Shared;
using BDSA2019.Lecture11.MobileApp.Models;
using System.Linq;
using static BDSA2019.Lecture11.MobileApp.Models.Events;
using System.Net;
using Microsoft.Identity.Client;
using System;

namespace BDSA2019.Lecture11.MobileApp.ViewModels
{
    public class SuperheroesViewModel : BaseViewModel
    {
        private const string Message = UpdateSuperhero;
        private readonly INavigationService _navigation;
        private readonly IMessagingCenter _messaging;
        private readonly IRestClient _client;
        private readonly IAuthenticationService _service;
        private readonly IDialogService _dialog;

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

        public Command LoadCommand { get; }
        public Command NewCommand { get; }
        public Command ViewCommand { get; }

        public Command LogoutCommand { get; }

        public SuperheroesViewModel(INavigationService navigation, IMessagingCenter messaging, IRestClient client, IAuthenticationService service, IDialogService dialog)
        {
            _navigation = navigation;
            _messaging = messaging;
            _client = client;
            _service = service;
            _dialog = dialog;

            Title = "Browse";

            LoadCommand = new Command(async () => await ExecuteLoadCommand(), () => !IsBusy);
            NewCommand = new Command(async () => await ExecuteNewCommand(), () => !IsBusy);
            ViewCommand = new Command(async () => await ExecuteViewCommand(), () => !IsBusy);
            LogoutCommand = new Command(async () => await ExecuteLogoutCommand(), () => !IsBusy);

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

            try
            {
                var (status, items) = await _client.GetAllAsync<SuperheroListDTO>("superheroes");

                if (status != HttpStatusCode.OK)
                {
                    await _dialog.DisplayAlertAsync("Error", $"Error from api: {status}", "OK");
                }
                else
                {
                    Items.Clear();
                    foreach (var item in items)
                    {
                        Items.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                await _dialog.DisplayAlertAsync(e);
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

        private async Task ExecuteLogoutCommand()
        {
            IsBusy = true;

            await _service.LogoutAsync();

            IsBusy = false;
        }
    }
}
