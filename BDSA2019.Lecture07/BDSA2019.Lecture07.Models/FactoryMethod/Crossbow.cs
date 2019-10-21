namespace BDSA2019.Lecture07.Models.FactoryMethod
{
    public class Crossbow : IWeapon
    {
        public string Name => nameof(Crossbow);

        public int Damage => 12;

        public int Range => 12;
    }
}
