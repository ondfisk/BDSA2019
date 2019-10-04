using System.Collections.Generic;
using BDSA2019.Lecture06.Entities;

namespace BDSA2019.Lecture06.Models
{
    public class SuperheroDetailsDTO
    {
        public int Id { get; set; }
        public string Occupation { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public Gender Gender { get; set; }
        public int? FirstAppearance { get; set; }
        public ICollection<string> Powers { get; set; }
    }
}
