using BDSA2019.Lecture11.MobileApp.Models;
using Moq;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BDSA2019.Lecture11.MobileApp.Tests.Models
{
    public class RestClientTests
    {
        [Fact]
        public async Task GetAllAsync_given_resource_returns_converted()
        { 
            var json = "[{\"id\":1,\"name\":\"foo\"},{\"id\":2,\"name\":\"bar\"}]";

            var handler = new HttpMessageHandlerStub { StatusCode = HttpStatusCode.OK, Content = json };
            var httpClient = new HttpClient(handler);

            var settings = new Mock<ISettings>();
            settings.SetupGet(s => s.BackendUrl).Returns(new Uri("https://foo.bar"));

            var service = new Mock<IAuthenticationService>();
            var client = new RestClient(httpClient, settings.Object, service.Object);

            var (_, response) = await client.GetAllAsync<TestType>("items");

            Assert.Collection(response,
                d => { Assert.Equal(1, d.Id); Assert.Equal("foo", d.Name); },
                d => { Assert.Equal(2, d.Id); Assert.Equal("bar", d.Name); });
        }

        [Fact]
        public async Task GetAsync_given_resource_returns_converted()
        {
            var json = "{\"id\":1,\"name\":\"foo\"}";

            var handler = new HttpMessageHandlerStub { StatusCode = HttpStatusCode.OK, Content = json };
            var httpClient = new HttpClient(handler);

            var settings = new Mock<ISettings>();
            settings.SetupGet(s => s.BackendUrl).Returns(new Uri("https://foo.bar"));

            var service = new Mock<IAuthenticationService>();
            var client = new RestClient(httpClient, settings.Object, service.Object);

            var (_, response) = await client.GetAsync<TestType>("items");

            Assert.Equal(1, response.Id);
            Assert.Equal("foo", response.Name);
        }

        [Fact]
        public async Task PostAsync_given_resource_returns_location()
        {
            var type = new TestType();

            var handler = new HttpMessageHandlerStub { StatusCode = HttpStatusCode.Created, Location = new Uri("https://foo.bar/data/2") };
            var httpClient = new HttpClient(handler);

            var settings = new Mock<ISettings>();
            settings.SetupGet(s => s.BackendUrl).Returns(new Uri("https://foo.bar"));

            var service = new Mock<IAuthenticationService>();
            var client = new RestClient(httpClient, settings.Object, service.Object);

            var (_, response) = await client.PostAsync("items", type);

            Assert.Equal(new Uri("https://foo.bar/data/2"), response);
        }

        [Fact]
        public async Task PutAsync_given_NoContent_returns_true()
        {
            var type = new TestType();

            var handler = new HttpMessageHandlerStub { StatusCode = HttpStatusCode.NoContent };
            var httpClient = new HttpClient(handler);

            var settings = new Mock<ISettings>();
            settings.SetupGet(s => s.BackendUrl).Returns(new Uri("https://foo.bar"));

            var service = new Mock<IAuthenticationService>();
            var client = new RestClient(httpClient, settings.Object, service.Object);

            var (_, response) = await client.PutAsync("items", type);

            Assert.True(response);
        }

        [Fact]
        public async Task PutAsync_given_Conflict_returns_false()
        {
            var type = new TestType();

            var handler = new HttpMessageHandlerStub { StatusCode = HttpStatusCode.Conflict };
            var httpClient = new HttpClient(handler);

            var settings = new Mock<ISettings>();
            settings.SetupGet(s => s.BackendUrl).Returns(new Uri("https://foo.bar"));

            var service = new Mock<IAuthenticationService>();
            var client = new RestClient(httpClient, settings.Object, service.Object);

            var (_, response) = await client.PutAsync("items", type);

            Assert.False(response);
        }

        [Fact]
        public async Task DeleteAsync_given_NoContent_returns_true()
        {
            var handler = new HttpMessageHandlerStub { StatusCode = HttpStatusCode.NoContent };
            var httpClient = new HttpClient(handler);

            var settings = new Mock<ISettings>();
            settings.SetupGet(s => s.BackendUrl).Returns(new Uri("https://foo.bar"));

            var service = new Mock<IAuthenticationService>();
            var client = new RestClient(httpClient, settings.Object, service.Object);

            var (_, response) = await client.DeleteAsync("items");

            Assert.True(response);
        }

        [Fact]
        public async Task DeleteAsync_given_Conflict_returns_false()
        {
            var handler = new HttpMessageHandlerStub { StatusCode = HttpStatusCode.Conflict };
            var httpClient = new HttpClient(handler);
            
            var settings = new Mock<ISettings>();
            settings.SetupGet(s => s.BackendUrl).Returns(new Uri("https://foo.bar"));

            var service = new Mock<IAuthenticationService>();
            var client = new RestClient(httpClient, settings.Object, service.Object);

            var (_, response) = await client.DeleteAsync("items");

            Assert.False(response);
        }

        class TestType
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        class HttpMessageHandlerStub : HttpMessageHandler
        {
            public HttpStatusCode StatusCode { get; set; }
            public string Content { get; set; }
            public Uri Location { get; set; }

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var response = new HttpResponseMessage(StatusCode);

                if (!string.IsNullOrWhiteSpace(Content))
                {
                    response.Content = new StringContent(Content, Encoding.UTF8, "service/json");
                }

                response.Headers.Location = Location;

                return await Task.FromResult(response);
            }
        }
    }
}
