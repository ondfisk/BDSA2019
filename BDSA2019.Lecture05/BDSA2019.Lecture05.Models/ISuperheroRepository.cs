using System.Linq;

namespace BDSA2019.Lecture05.Models
{
    public interface ISuperheroRepository
    {
        (Response response, int superheroId) Create(SuperheroCreateDTO superhero);
        SuperheroDetailsDTO Read(int superheroId);
        IQueryable<SuperheroListDTO> Read();
        Response Update(SuperheroUpdateDTO superhero);
        Response Delete(int superheroId);
    }
}