using System.ComponentModel;
using Xamarin.Forms;
using BDSA2019.Lecture11.MobileApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using BDSA2019.Lecture11.MobileApp.Models;
using Microsoft.Identity.Client;
using System;

namespace BDSA2019.Lecture11.MobileApp.Views
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

            BindingContext = _viewModel = App.Container.GetRequiredService<SuperheroesViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel.Items.Count == 0)
            {
                _viewModel.LoadCommand.Execute(null);
            }
        }
    }
}