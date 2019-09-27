using System.Collections.Generic;
using System.Linq;
using BDSA2019.Lecture05.Entities;
using static BDSA2019.Lecture05.Models.Response;

namespace BDSA2019.Lecture05.Models
{
    public class SuperheroRepository
    {
        public SuperheroRepository()
        {
        }

        public (Response response, int superheroId) Create(SuperheroCreateDTO superhero)
        {
            throw new System.NotImplementedException();
        }

        public Response Delete(int superheroId)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<SuperheroListDTO> Read()
        {
            throw new System.NotImplementedException();
        }

        public SuperheroDetailsDTO Read(int superheroId)
        {
            throw new System.NotImplementedException();
        }

        public Response Update(SuperheroUpdateDTO superhero)
        {
            throw new System.NotImplementedException();
        }
    }
}