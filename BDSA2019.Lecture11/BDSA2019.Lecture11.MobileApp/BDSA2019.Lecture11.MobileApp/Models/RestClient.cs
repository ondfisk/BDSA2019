using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            _client = client;
            _settings = settings;
            _service = service;
        }

        public async Task<(HttpStatusCode, IEnumerable<T>)> GetAllAsync<T>(string resource)
        {
            var message = await CreateHttpAuthorizedMethodAsync(HttpMethod.Get, resource);

            var response = await _client.SendAsync(message);

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
            var message = await CreateHttpAuthorizedMethodAsync(HttpMethod.Get, resource);

            var response = await _client.SendAsync(message);

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
            var message = await CreateHttpAuthorizedMethodAsync(HttpMethod.Post, resource, item);

            var response = await _client.SendAsync(message);

            return (response.StatusCode, response.Headers.Location);
        }

        public async Task<(HttpStatusCode, bool)> PutAsync<T>(string resource, T item)
        {
            var message = await CreateHttpAuthorizedMethodAsync(HttpMethod.Put, resource, item);

            var response = await _client.SendAsync(message);

            return (response.StatusCode, response.IsSuccessStatusCode);
        }

        public async Task<(HttpStatusCode, bool)> DeleteAsync(string resource)
        {
            var message = await CreateHttpAuthorizedMethodAsync(HttpMethod.Delete, resource);

            var response = await _client.SendAsync(message);

            return (response.StatusCode, response.IsSuccessStatusCode);
        }

        private async Task<HttpRequestMessage> CreateHttpAuthorizedMethodAsync(HttpMethod method, string resource)
        {
            var (token, errorMessage) = await _service.AcquireTokenAsync();

            var requestUri = new Uri(_settings.BackendUrl, resource);

            var message = new HttpRequestMessage(method, requestUri);
            message.Headers.Accept.ParseAdd("application/json");
            message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return message;
        }

        private async Task<HttpRequestMessage> CreateHttpAuthorizedMethodAsync<T>(HttpMethod method, string resource, T item)
        {
            var message = await CreateHttpAuthorizedMethodAsync(method, resource);

            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            message.Content = content;

            return message;
        }
    }
}
