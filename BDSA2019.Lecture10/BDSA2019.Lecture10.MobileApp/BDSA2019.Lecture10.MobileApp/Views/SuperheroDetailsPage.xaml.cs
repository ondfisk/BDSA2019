using System.ComponentModel;
using Xamarin.Forms;
using BDSA2019.Lecture10.MobileApp.Models;
using BDSA2019.Lecture10.MobileApp.ViewModels;

namespace BDSA2019.Lecture10.MobileApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class SuperheroDetailsPage : ContentPage
    {
        private readonly SuperheroDetailsViewModel _viewModel;

        public SuperheroDetailsPage(SuperheroDetailsViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }
    }
}