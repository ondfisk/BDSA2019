using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDSA2019.Lecture11.Entities;
using BDSA2019.Lecture11.Shared;
using Microsoft.EntityFrameworkCore;
using static BDSA2019.Lecture11.Models.Response;

namespace BDSA2019.Lecture11.Models
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
                City = await MapCityAsync(superhero.CityName),
                FirstAppearance = superhero.FirstAppearance,
                Gender = superhero.Gender.Convert(),
                Occupation = superhero.Occupation,
                PortraitUrl = superhero.PortraitUrl,
                BackgroundUrl = superhero.BackgroundUrl,
                Powers = await MapPowersAsync(0, superhero.Powers)
            };

            _context.Superheroes.Add(entity);
            await _context.SaveChangesAsync();

            return (Created, entity.Id);
        }

        public async Task<IEnumerable<SuperheroListDTO>> ReadAsync()
        {
            var query = from s in _context.Superheroes
                        orderby s.AlterEgo
                        select new SuperheroListDTO
                        {
                            Id = s.Id,
                            Name = s.Name,
                            AlterEgo = s.AlterEgo,
                            PortraitUrl = s.PortraitUrl
                        };

            return await query.ToListAsync();
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
                               Gender = s.Gender.Convert(),
                               FirstAppearance = s.FirstAppearance,
                               PortraitUrl = s.PortraitUrl,
                               BackgroundUrl = s.BackgroundUrl,
                               Powers = s.Powers.Select(p => p.Power.Name).ToList()
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
            entity.City = await MapCityAsync(superhero.CityName);
            entity.Gender = superhero.Gender.Convert();
            entity.FirstAppearance = superhero.FirstAppearance;
            entity.Occupation = superhero.Occupation;
            entity.PortraitUrl = superhero.PortraitUrl;
            entity.BackgroundUrl = superhero.BackgroundUrl;
            entity.Powers = await MapPowersAsync(superhero.Id, superhero.Powers);

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

        private async Task<City> MapCityAsync(string cityName)
        {
            return string.IsNullOrWhiteSpace(cityName) ? null :
                await _context.Cities.FirstOrDefaultAsync(c => c.Name == cityName) ??
                new City { Name = cityName };
        }

        private async Task<ICollection<SuperheroPower>> MapPowersAsync(int superheroId, IEnumerable<string> powers)
        {
            var entities = await _context.Powers.ToDictionaryAsync(p => p.Name);

            var superheroPowers = new List<SuperheroPower>();

            foreach (var power in powers)
            {
                if (!entities.TryGetValue(power, out var p))
                {
                    p = new Power { Name = power };
                }
                superheroPowers.Add(new SuperheroPower { SuperheroId = superheroId, Power = p });
            }

            return superheroPowers;
        }
    }
}
