using System;

namespace BDSA2019.Lecture07.Models.IoCContainer
{
    public class Wolf : IAnimal
    {
        public void Hello()
        {
            Console.WriteLine("Woof");
        }
    }
}
