using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BDSA2019.Lecture11.MobileApp.Models
{
    public interface IRestClient
    {
        Task<(HttpStatusCode, IEnumerable<T>)> GetAllAsync<T>(string resource);
        Task<(HttpStatusCode, T)> GetAsync<T>(string resource);
        Task<(HttpStatusCode, Uri)> PostAsync<T>(string resource, T item);
        Task<(HttpStatusCode, bool)> PutAsync<T>(string resource, T item);
        Task<(HttpStatusCode, bool)> DeleteAsync(string resource);
    }
}
