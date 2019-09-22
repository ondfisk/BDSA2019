using System.Collections.Generic;
using BDSA2019.Lecture05.Entities;

namespace BDSA2019.Lecture05.Models
{
    public class SuperheroDetailsDTO
    {
        public string Occupation { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public Gender Gender { get; set; }
        public int? FirstAppearance { get; set; }
        public ICollection<string> Powers { get; set; }
    }
}