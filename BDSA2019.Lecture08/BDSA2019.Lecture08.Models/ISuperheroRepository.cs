using System.Linq;
using System.Threading.Tasks;

namespace BDSA2019.Lecture08.Models
{
    public interface ISuperheroRepository
    {
        Task<(Response response, int superheroId)> CreateAsync(SuperheroCreateDTO superhero);
        Task<SuperheroDetailsDTO> ReadAsync(int superheroId);
        IQueryable<SuperheroListDTO> Read();
        Task<Response> UpdateAsync(SuperheroUpdateDTO superhero);
        Task<Response> DeleteAsync(int superheroId);
    }
}
