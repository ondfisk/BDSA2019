namespace BDSA2019.Lecture06.Models
{
    public class SuperheroUpdateDTO : SuperheroCreateDTO
    {
        public int Id { get; set; }

        public string WhyTheF { get; set; }
    }
}
