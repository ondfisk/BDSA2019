using System.Linq;

namespace BDSA2019.Lecture05.Models
{
    public interface ISuperheroRepository
    {
        (Response response, int superheroId) Create(SuperheroCreateDTO superhero); 
        IQueryable<SuperheroListDTO> Read(); 
        SuperheroDetailsDTO Read(int superheroId); 
        Response Update(SuperheroUpdateDTO superhero);
        Response Delete(int superheroId);
    }
}
