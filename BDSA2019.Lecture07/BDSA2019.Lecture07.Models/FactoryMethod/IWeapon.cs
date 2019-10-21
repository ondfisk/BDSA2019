namespace BDSA2019.Lecture07.Models.FactoryMethod
{
    public interface IWeapon
    {
        string Name { get; }

        int Damage { get; }

        int Range { get; }
    }
}
