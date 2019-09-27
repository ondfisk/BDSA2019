using System.Collections.Generic;
using System.Linq;
using BDSA2019.Lecture05.Entities;
using static BDSA2019.Lecture05.Models.Response;

namespace BDSA2019.Lecture05.Models
{

    public class SuperheroRepository : ISuperheroRepository
    {
        private readonly ISuperheroContext _context;

        public SuperheroRepository(ISuperheroContext context)
        {
            _context = context;
        }

        private City ReadOrCreateCity(string cityName)
        {
            return _context.Cities.FirstOrDefault(c => c.Name == cityName) ??
               new City { Name = cityName };
        }

        private IEnumerable<SuperheroPower> ReadOrCreatePowers(int superheroId, IEnumerable<string> powers)
        {
            foreach (var power in powers)
            {
                var p = _context.Powers.FirstOrDefault(c => c.Name == power) ??
                    new Power { Name = power };

                yield return new SuperheroPower { SuperheroId = superheroId, Power = p };
            }
        }

        public (Response response, int superheroId) Create(SuperheroCreateDTO superhero)
        {

            var entity = new Superhero
            {
                Name = superhero.Name,
                AlterEgo = superhero.AlterEgo,
                City = ReadOrCreateCity(superhero.CityName),
                FirstAppearance = superhero.FirstAppearance,
                Gender = superhero.Gender,
                Occupation = superhero.Occupation
            };

            _context.Superheroes.Add(entity);
            _context.SaveChanges();

            return (Created, entity.Id);
        }

        public Response Delete(int superheroId)
        {
            var entity = _context.Superheroes.Find(superheroId);

            if (entity == null)
            {
                return NotFound;
            }

            _context.Superheroes.Remove(entity);
            _context.SaveChanges();

            return Response.Deleted;
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
            var entity = _context.Superheroes.Find(superhero.Id);

            if (entity == null)
            {
                return NotFound;
            }

            var city = _context.Cities.FirstOrDefault(c => c.Name == superhero.CityName) ??
                new City { Name = superhero.CityName };

            entity.Name = superhero.Name;
            entity.AlterEgo = superhero.AlterEgo;
            entity.City = city;

            _context.SaveChanges();

            return Updated;
        }
    }
}