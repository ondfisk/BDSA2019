using System.Threading.Tasks;

namespace BDSA2019.Lecture11.MobileApp.Models
{
    public interface IAuthenticationService
    {
        Task<(string token, string errorMessage)> AcquireTokenAsync();
        Task LogoutAsync();
    }
}