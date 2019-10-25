namespace BDSA2019.Lecture07.Models.FactoryMethod
{
    public class Rifle : IWeapon
    {
        public string Name => "Barrett 50 cal. sniper bad ass big gun";

        public int Damage => 420;

        public int Range => 1000;
    }
}