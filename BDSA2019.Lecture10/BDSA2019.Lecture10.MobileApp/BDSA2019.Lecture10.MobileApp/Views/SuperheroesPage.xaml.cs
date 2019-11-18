using System;
using System.ComponentModel;
using Xamarin.Forms;
using BDSA2019.Lecture10.MobileApp.Models;
using BDSA2019.Lecture10.MobileApp.ViewModels;
using BDSA2019.Lecture10.MobileApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BDSA2019.Lecture10.MobileApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class SuperheroesPage : ContentPage
    {
        private readonly SuperheroesViewModel _viewModel;

        public SuperheroesPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = IoCContainer.Container.GetService<SuperheroesViewModel>();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as SuperheroListDTO;
            if (item == null)
            {
                return;
            }

            await Navigation.PushAsync(new SuperheroDetailsPage(new SuperheroDetailsViewModel(item)));

            // Manually deselect item.
            //SuperheroesListView.SelectedItem = null;
        }

        async void AddSuperhero_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewSuperheroPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel.Items.Count == 0)
            {
                _viewModel.LoadItemsCommand.Execute(null);
            }
        }
    }
}