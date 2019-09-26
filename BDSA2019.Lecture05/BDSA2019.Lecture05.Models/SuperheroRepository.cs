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

        public (Response response, int superheroId) Create(SuperheroCreateDTO superhero)
        {
            var city = superhero.CityName == null ? null :
                       _context.Cities.FirstOrDefault(c => c.Name == superhero.CityName) ?? new City { Name = superhero.CityName };

            IEnumerable<SuperheroPower> getPowers(IEnumerable<string> powers)
            {
                foreach (var powerName in superhero.Powers)
                {
                    var power = _context.Powers.FirstOrDefault(p => p.Name == powerName);

                    yield return new SuperheroPower { Power = power ?? new Power { Name = powerName } };
                }
            }

            var entity = new Superhero
            {
                Name = superhero.Name,
                AlterEgo = superhero.AlterEgo,
                Occupation = superhero.Occupation,
                Gender = superhero.Gender,
                City = city,
                FirstAppearance = superhero.FirstAppearance,
                Powers = getPowers(superhero.Powers).ToList()
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

            return Deleted;
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