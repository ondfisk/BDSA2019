using System.Collections.Generic;

namespace BDSA2019.Lecture04
{
    public class Power
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<SuperheroPower> Superheroes { get; set; }
    }
}
