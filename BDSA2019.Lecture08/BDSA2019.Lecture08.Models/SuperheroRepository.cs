using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDSA2019.Lecture08.Entities;
using Microsoft.EntityFrameworkCore;
using static BDSA2019.Lecture08.Models.Response;

namespace BDSA2019.Lecture08.Models
{

    public class SuperheroRepository : ISuperheroRepository
    {
        private readonly ISuperheroContext _context;

        public SuperheroRepository(ISuperheroContext context)
        {
            _context = context;
        }

        public async Task<(Response response, int superheroId)> CreateAsync(SuperheroCreateDTO superhero)
        {
            var entity = new Superhero
            {
                Name = superhero.Name,
                AlterEgo = superhero.AlterEgo,
                City = ReadOrCreateCity(superhero.CityName),
                FirstAppearance = superhero.FirstAppearance,
                Gender = superhero.Gender,
                Occupation = superhero.Occupation,
                // Powers = ReadOrCreatePowers(0, superhero.Powers).ToList()
            };

            _context.Superheroes.Add(entity);
            await _context.SaveChangesAsync();

            return (Created, entity.Id);
        }

        public IQueryable<SuperheroListDTO> Read()
        {
            return from s in _context.Superheroes
                   orderby s.AlterEgo
                   select new SuperheroListDTO
                   {
                       Id = s.Id,
                       Name = s.Name,
                       AlterEgo = s.AlterEgo
                   };
        }

        public async Task<SuperheroDetailsDTO> ReadAsync(int superheroId)
        {
            var entities = from s in _context.Superheroes
                           where s.Id == superheroId
                           select new SuperheroDetailsDTO
                           {
                               Id = s.Id,
                               Name = s.Name,
                               AlterEgo = s.AlterEgo,
                               Occupation = s.Occupation,
                               CityId = s.CityId,
                               CityName = s.City.Name,
                               Gender = s.Gender,
                               FirstAppearance = s.FirstAppearance,
                               InnerPowers = s.Powers.Select(p => p.Power.Name)
                           };

            return await entities.FirstOrDefaultAsync();
        }

        public async Task<Response> UpdateAsync(SuperheroUpdateDTO superhero)
        {
            var entity = await _context.Superheroes.Include(c => c.Powers).FirstOrDefaultAsync(c => c.Id == superhero.Id);

            if (entity == null)
            {
                return NotFound;
            }

            entity.Name = superhero.Name;
            entity.AlterEgo = superhero.AlterEgo;
            entity.City = ReadOrCreateCity(superhero.CityName);
            entity.Gender = superhero.Gender;
            entity.FirstAppearance = superhero.FirstAppearance;
            entity.Occupation = superhero.Occupation;
            // entity.Powers = (await ReadOrCreatePowersAsync(0, superhero.Powers)).ToList();

            await _context.SaveChangesAsync();

            return Updated;
        }

        public async Task<Response> DeleteAsync(int superheroId)
        {
            var entity = await _context.Superheroes.FindAsync(superheroId);

            if (entity == null)
            {
                return NotFound;
            }

            _context.Superheroes.Remove(entity);
            await _context.SaveChangesAsync();

            return Deleted;
        }

        private City ReadOrCreateCity(string cityName)
        {
            return string.IsNullOrWhiteSpace(cityName) ? null :
                _context.Cities.FirstOrDefault(c => c.Name == cityName) ??
                new City { Name = cityName };
        }

        private async IAsyncEnumerable<SuperheroPower> ReadOrCreatePowersAsync(int superheroId, IEnumerable<string> powers)
        {
            foreach (var power in powers)
            {
                var p = await _context.Powers.FirstOrDefaultAsync(c => c.Name == power) ??
                    new Power { Name = power };

                yield return new SuperheroPower { SuperheroId = superheroId, Power = p };
            }
        }
    }
}
