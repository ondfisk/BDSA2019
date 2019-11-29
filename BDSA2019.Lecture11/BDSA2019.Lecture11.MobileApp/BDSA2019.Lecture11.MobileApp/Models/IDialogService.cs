using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BDSA2019.Lecture11.MobileApp.Models
{
    public interface IDialogService
    {
        Task DisplayAlertAsync(string title, string message, string cancel);
        Task DisplayAlertAsync(Exception exception);
        Task<bool> DisplayAlertAsync(string title, string message, string accept, string cancel);
        Task<string> DisplayPromptAsync(string title, string message, string accept = "OK", string cancel = "Cancel", string placeholder = null, int maxLength = -1, Keyboard keyboard = null);
    }
}
