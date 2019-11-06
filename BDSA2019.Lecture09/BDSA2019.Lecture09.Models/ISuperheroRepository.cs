using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDSA2019.Lecture09.Models
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
