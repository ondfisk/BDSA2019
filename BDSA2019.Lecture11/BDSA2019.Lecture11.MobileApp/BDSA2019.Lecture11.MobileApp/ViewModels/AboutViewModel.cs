using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BDSA2019.Lecture11.MobileApp.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel() 
        {
            Title = "About";

            OpenWebCommand = new Command(() => Launcher.OpenAsync(new Uri("https://xamarin.com/platform")), () => !IsBusy);
        }

        public Command OpenWebCommand { get; }
    }
}
