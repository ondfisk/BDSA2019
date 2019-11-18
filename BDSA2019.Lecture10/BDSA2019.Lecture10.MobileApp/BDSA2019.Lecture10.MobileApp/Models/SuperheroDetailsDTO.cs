using System.Collections.Generic;

namespace BDSA2019.Lecture10.MobileApp.Models
{
    public class SuperheroDetailsDTO : SuperheroListDTO
    {
        public string Occupation { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public Gender Gender { get; set; }
        public int? FirstAppearance { get; set; }
        public string BackgroundUrl { get; set; }
        public ISet<string> Powers { get; set; } = new HashSet<string>();
    }
}
