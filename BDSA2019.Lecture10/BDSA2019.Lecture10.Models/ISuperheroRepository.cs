using System.Collections.Generic;
using System.Threading.Tasks;

namespace BDSA2019.Lecture10.Models
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
