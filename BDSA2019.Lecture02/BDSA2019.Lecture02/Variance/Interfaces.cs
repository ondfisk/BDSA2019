using System;
using System.Collections.Generic;

namespace BDSA2019.Lecture02.Variance
{
    /// <summary>
    /// Contravariant interface - can only take T's as input parameters
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDestroyer<in T> where T : IDisposable
    {
        void Destroy(T obj);
    }

    /// <summary>
    /// Covariant interface - can only return T's
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICreator<out T> where T : new()
    {
        T Create();
    }

    public interface IBreeder<out T> : ICreator<T> where T : new()
    {
        IEnumerable<T> Breed();
    }

    /// <summary>
    /// Invariant interface - best of both worlds? 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICreatorDestroyer<T> : ICreator<T>, IDestroyer<T> where T : IDisposable, new()
    {
    }
}
