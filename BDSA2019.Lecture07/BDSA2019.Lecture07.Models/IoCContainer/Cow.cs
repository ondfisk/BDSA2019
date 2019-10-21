using System;

namespace BDSA2019.Lecture07.Models.IoCContainer
{
    public class Cow : IAnimal
    {
        public void Hello()
        {
            Console.WriteLine("Mooh");
        }
    }
}
