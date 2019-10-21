using System;
using System.Collections.Generic;
using System.Text;

namespace BDSA2019.Lecture07.Models.FactoryMethod
{
    public class Spear : IWeapon
    {
        public string Name => nameof(Spear);

        public int Damage => 12;

        public int Range => 5;
    }
}
