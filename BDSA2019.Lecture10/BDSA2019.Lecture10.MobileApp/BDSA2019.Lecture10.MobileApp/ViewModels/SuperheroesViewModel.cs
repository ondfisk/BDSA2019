using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using BDSA2019.Lecture10.MobileApp.Models;
using BDSA2019.Lecture10.MobileApp.Views;
using BDSA2019.Lecture10.MobileApp.Services;

namespace BDSA2019.Lecture10.MobileApp.ViewModels
{
    public class SuperheroesViewModel : BaseViewModel
    {
        private readonly IRestClient _client;

        public ObservableCollection<SuperheroListDTO> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public SuperheroesViewModel(IRestClient client)
        {
            _client = client;

            Title = "Browse";
            Items = new ObservableCollection<SuperheroListDTO>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadCommand());

            MessagingCenter.Subscribe<NewSuperheroPage, SuperheroListDTO>(this, "AddSuperhero", (obj, superhero) =>
            {
                Items.Add(superhero);
            });
        }

        private async Task ExecuteLoadCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                Items.Clear();

                var items = await _client.GetAllAsync<SuperheroListDTO>("superheroes");

                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex);
            //}
            finally
            {
                IsBusy = false;
            }
        }
    }
}