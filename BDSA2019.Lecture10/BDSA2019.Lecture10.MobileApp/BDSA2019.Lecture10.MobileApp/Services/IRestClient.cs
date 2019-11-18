using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BDSA2019.Lecture10.MobileApp.Services
{
    public interface IRestClient
    {
        Task<IEnumerable<T>> GetAllAsync<T>(string resource);
        Task<T> GetAsync<T>(string resource);
        Task<Uri> PostAsync<T>(string resource, T item);
        Task<bool> PutAsync<T>(string resource, T item);
        Task<bool> DeleteAsync(string resource);
    }
}
