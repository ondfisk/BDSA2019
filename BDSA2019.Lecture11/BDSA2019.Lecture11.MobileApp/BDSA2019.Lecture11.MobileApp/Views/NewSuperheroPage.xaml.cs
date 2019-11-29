using System.ComponentModel;
using Xamarin.Forms;
using BDSA2019.Lecture11.MobileApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace BDSA2019.Lecture11.MobileApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewSuperheroPage : ContentPage
    {
        private readonly SuperheroCreateViewModel _viewModel;

        public NewSuperheroPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = App.Container.GetRequiredService<SuperheroCreateViewModel>(); BindingContext = App.Container.GetRequiredService<SuperheroCreateViewModel>();
        }
    }
}
