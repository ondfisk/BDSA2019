using BDSA2019.Lecture11.MobileApp.Models;
using System.Threading.Tasks;

namespace BDSA2019.Lecture11.MobileApp.Services
{
    public interface INavigationService
    {
        Task BackAsync();
        Task NewAsync();
        Task ViewAsync(SuperheroListDTO superhero);
        Task EditAsync(SuperheroDetailsDTO superhero);
        Task CancelAsync();
    }
}
