using System;
using Xamarin.Forms;
using BDSA2019.Lecture11.MobileApp.Services;
using Xamarin.Essentials;

namespace BDSA2019.Lecture11.MobileApp
{
    public partial class App : Application
    {
        public static Uri BackendUrl => DeviceInfo.Platform == DevicePlatform.Android
            ? new Uri("http://10.0.2.2:5000")
            : new Uri("http://localhost:5000");

        public static IServiceProvider Container => IoCContainer.Container;

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
