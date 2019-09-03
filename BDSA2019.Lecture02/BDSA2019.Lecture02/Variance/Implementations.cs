using System;
using System.Collections.Generic;

namespace BDSA2019.Lecture02.Variance
{
    public class Destroyer<T> : IDestroyer<T> where T : IDisposable
    {
        public void Destroy(T obj)
        {
            obj.Dispose();
        }
    }

    public class Creator<T> : ICreator<T> where T : new()
    {
        public T Create()
        {
            return new T();
        }
    }

    public class Breeder<T> : Creator<T>, IBreeder<T> where T : new()
    {
        public IEnumerable<T> Breed()
        {
            while (true)
            {
                yield return new T();
            }
        }
    }

    public class CreatorDestroyer<T> : ICreator<T>, IDestroyer<T> where T : IDisposable, new()
    {
        public T Create()
        {
            return new T();
        }

        public void Destroy(T obj)
        {
            obj.Dispose();
        }
    }
}
