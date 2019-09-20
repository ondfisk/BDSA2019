using System.Collections.Generic;

namespace BDSA2019.Lecture04
{
    public class Superhero
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AlterEgo { get; set; }
        public string Occupation { get; set; }
        public int? CityId { get; set; }
        public City City { get; set; }
        public Gender Gender { get; set; }
        public int FirstAppearance { get; set; }
        public ICollection<SuperheroPower> Powers { get; set; }
    }
}
