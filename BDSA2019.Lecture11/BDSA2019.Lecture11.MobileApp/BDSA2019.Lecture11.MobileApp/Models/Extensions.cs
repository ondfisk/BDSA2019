using System;
using Xamarin.Forms;

namespace BDSA2019.Lecture11.MobileApp.Models
{
    public static class Extensions
    {
        public static void DisplayAlert(this Exception exception)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Application.Current.MainPage.DisplayAlert(exception.GetType().Name, exception.Message, "OK");
            });
        }
    }
}
