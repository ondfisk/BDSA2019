using System;

namespace BDSA2019.Lecture11.Models
{
    public static class Extensions
    {
        public static Shared.Gender Convert(this Entities.Gender gender) => (Shared.Gender)Enum.Parse(typeof(Shared.Gender), gender.ToString());
        public static Entities.Gender Convert(this Shared.Gender gender) => (Entities.Gender)Enum.Parse(typeof(Entities.Gender), gender.ToString());
    }
}
