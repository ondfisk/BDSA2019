using BDSA2019.Lecture10.MobileApp.Models;
using System.Threading.Tasks;

namespace BDSA2019.Lecture10.MobileApp.Services
{
    public interface INavigationService
    {
        Task BackAsync();
        Task NewAsync();
        Task ViewAsync(SuperheroListDTO superhero);
        Task EditAsync(SuperheroDetailsDTO superhero);
    }
}
