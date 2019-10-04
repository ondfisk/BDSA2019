using BDSA2019.Lecture07.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDSA2019.Lecture07.Models
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly IFuturamaContext _context;

        public CharacterRepository(IFuturamaContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(CharacterCreateUpdateDTO character)
        {
            var entity = new Character
            {
                ActorId = character.ActorId,
                Name = character.Name,
                Species = character.Species,
                Planet = character.Planet,
            };

            _context.Characters.Add(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<CharacterDTO> FindAsync(int characterId)
        {
            var entities = from c in _context.Characters
                           where c.Id == characterId
                           select new CharacterDTO
                           {
                               Id = c.Id,
                               Name = c.Name,
                               Species = c.Species,
                               Planet = c.Planet,
                               ActorId = c.ActorId,
                               ActorName = c.Actor.Name,
                               NumberOfEpisodes = c.EpisodeCharacters.Count()
                           };

            return await entities.FirstOrDefaultAsync();
        }

        public IQueryable<CharacterDTO> Read()
        {
            var entities = from c in _context.Characters
                           select new CharacterDTO
                           {
                               Id = c.Id,
                               Name = c.Name,
                               Species = c.Species,
                               Planet = c.Planet,
                               ActorId = c.ActorId,
                               ActorName = c.Actor.Name,
                               NumberOfEpisodes = c.EpisodeCharacters.Count()
                           };

            return entities;
        }

        public async Task<bool> UpdateAsync(CharacterCreateUpdateDTO character)
        {
            var entity = await _context.Characters.FindAsync(character.Id);

            if (entity == null)
            {
                return false;
            }

            entity.ActorId = character.ActorId;
            entity.Name = character.Name;
            entity.Species = character.Species;
            entity.Planet = character.Planet;
           
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int characterId)
        {
            var entity = await _context.Characters.FindAsync(characterId);

            if (entity == null)
            {
                return false;
            }

            _context.Characters.Remove(entity);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
