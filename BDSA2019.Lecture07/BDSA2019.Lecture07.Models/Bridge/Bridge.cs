using System;
using System.Threading.Tasks;

namespace BDSA2019.Lecture07.Models.Bridge
{
    public class Bridge
    {
        private readonly ICharacterRepository _repository;

        public Bridge(ICharacterRepository repository)
        {
            _repository = repository;
        }

        public async Task PrintAll()
        {
            foreach (var character in await _repository.ReadAsync())
            {
                Console.WriteLine(character);
            }
        }
    }
}
