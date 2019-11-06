using System.Collections.Generic;
using System.Linq;
using BDSA2019.Lecture09.Entities;

namespace BDSA2019.Lecture09.Models
{
    public class SuperheroDetailsDTO : SuperheroListDTO
    {
        public string Occupation { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public Gender Gender { get; set; }
        public int? FirstAppearance { get; set; }
        public ISet<string> Powers { get; set; } = new HashSet<string>();

        /// <summary>
        /// Required as .ToHashSet() is not allowed in an Entity Framework Core Linq query
        /// </summary>
        internal IEnumerable<string> InnerPowers
        {
            set => Powers = value.ToHashSet();
        }
    }
}
