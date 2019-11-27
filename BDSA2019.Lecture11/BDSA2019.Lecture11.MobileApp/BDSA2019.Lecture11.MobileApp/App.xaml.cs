using System;
using Xamarin.Forms;
using BDSA2019.Lecture11.MobileApp.Models;

namespace BDSA2019.Lecture11.MobileApp
{
    public partial class App : Application
    {
        public static IServiceProvider Container => IoCContainer.Container;

        public static object ParentWindow { get; set; }

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
