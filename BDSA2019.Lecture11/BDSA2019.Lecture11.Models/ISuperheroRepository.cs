using BDSA2019.Lecture11.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BDSA2019.Lecture11.Models
{
    public interface ISuperheroRepository
    {
        Task<(Response response, int superheroId)> CreateAsync(SuperheroCreateDTO superhero);
        Task<SuperheroDetailsDTO> ReadAsync(int superheroId);
        Task<IEnumerable<SuperheroListDTO>> ReadAsync();
        Task<Response> UpdateAsync(SuperheroUpdateDTO superhero);
        Task<Response> DeleteAsync(int superheroId);
    }
}
