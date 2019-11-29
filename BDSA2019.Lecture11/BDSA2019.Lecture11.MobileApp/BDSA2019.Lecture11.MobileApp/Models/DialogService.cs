using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BDSA2019.Lecture11.MobileApp.Models
{
    public class DialogService : IDialogService
    {
        private readonly Page _page;

        public DialogService(Page page)
        {
            _page = page;
        }

        public async Task DisplayAlertAsync(string title, string message, string cancel)
        {
            await _page.DisplayAlert(title, message, cancel);
        }

        public async Task DisplayAlertAsync(Exception exception)
        {
            await DisplayAlertAsync(exception.GetType().Name, exception.Message, "OK");
        }

        public async Task<bool> DisplayAlertAsync(string title, string message, string accept, string cancel)
        {
            return await _page.DisplayAlert(title, message, accept, cancel);
        }

        public async Task<string> DisplayPromptAsync(string title, string message, string accept = "OK", string cancel = "Cancel", string placeholder = null, int maxLength = -1, Keyboard keyboard = null)
        {
            return await _page.DisplayPromptAsync(title, message, accept, cancel, placeholder, maxLength, keyboard);
        }
    }
}
