using System;

namespace BDSA2019.Lecture07.Models.Adapter
{
    public class FooAdapter : IFooService
    {
        public bool Update(Foo foo)
        {
            try
            {
                FoolishService.Modify(foo);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
    }
}
