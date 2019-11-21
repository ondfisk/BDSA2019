using Xamarin.Essentials;
using Xamarin.Forms;
using System;
using BDSA2019.Lecture10.MobileApp.Services;

namespace BDSA2019.Lecture10.MobileApp
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //To debug on Android emulators run the web backend against .NET Core not IIS
        //If using other emulators besides stock Google images you may need to adjust the IP address
        public static Uri BackendUrl =>
            DeviceInfo.Platform == DevicePlatform.Android ? new Uri("http://10.0.2.2:5000") : new Uri("http://localhost:5000");

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
