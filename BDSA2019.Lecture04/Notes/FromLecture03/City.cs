namespace BDSA2019.Lecture04
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public City(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public City() {}

        public override string ToString()
        {
            return Name;
        }
    }
}
