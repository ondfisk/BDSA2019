using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BDSA2019.Lecture11.MobileApp.Models
{
    public class RestClient : IRestClient
    {
        private readonly HttpClient _client;
        private readonly ISettings _settings;
        private readonly IAuthenticationService _service;

        public RestClient(HttpClient client, ISettings settings, IAuthenticationService service)
        {
            client.BaseAddress = settings.BackendUrl;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");

            _client = client;
            _settings = settings;
            _service = service;
        }

        public async Task<(HttpStatusCode, IEnumerable<T>)> GetAllAsync<T>(string resource)
        {
            await AuthorizeClientAsync();

            var response = await _client.GetAsync(resource);

            var obj = default(IEnumerable<T>);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                obj = JsonConvert.DeserializeObject<IEnumerable<T>>(json);
            }

            return (response.StatusCode, obj);
        }

        public async Task<(HttpStatusCode, T)> GetAsync<T>(string resource)
        {
            await AuthorizeClientAsync();

            var response = await _client.GetAsync(resource);

            var obj = default(T);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                obj = JsonConvert.DeserializeObject<T>(json);
            }

            return (response.StatusCode, obj);
        }

        public async Task<(HttpStatusCode, Uri)> PostAsync<T>(string resource, T item)
        {
            await AuthorizeClientAsync();

            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(resource, content);

            return (response.StatusCode, response.Headers.Location);
        }

        public async Task<(HttpStatusCode, bool)> PutAsync<T>(string resource, T item)
        {
            await AuthorizeClientAsync();

            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync(resource, content);

            return (response.StatusCode, response.IsSuccessStatusCode);
        }

        public async Task<(HttpStatusCode, bool)> DeleteAsync(string resource)
        {
            await AuthorizeClientAsync();

            var response = await _client.DeleteAsync(resource);

            return (response.StatusCode, response.IsSuccessStatusCode);
        }

        private async Task AuthorizeClientAsync()
        {
            var (token, _) = await _service.AcquireTokenAsync();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
