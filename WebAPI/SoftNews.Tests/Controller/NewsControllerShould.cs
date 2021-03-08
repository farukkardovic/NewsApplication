using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SoftNews.WebAPI.ViewModels;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebAPI;
using Xunit;

namespace SoftNews.Tests.ControllerShould
{
    public class NewsControllerShould : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        private const int ValidNewsId = 10;

        public NewsControllerShould(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ReturnUnauthorizedWhenNotAuthenticated()
        {
            var client = _factory.CreateClient();

            var newNews = new NewsAddViewModel
            {
                AuthorId = 2,
                Name = "Test 1",
                CategoryId = 2,               
                Description = "First test",
                File = null
            };

            string json = JsonConvert.SerializeObject(newNews, Formatting.Indented);
            var httpContent = new StringContent(json);

            var response = await client.PostAsync("api/News", httpContent);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

        }

        [Fact]
        public async Task ReturnOkWhenInsertingNewItem()
        {
            var client = BuildHttpClient();

            var newNews = new NewsAddViewModel
            {
                AuthorId = 2,
                Name = "Test 2",
                CategoryId = 2,              
                Description = "Second test"
            };

            using (var stream = File.OpenRead($"{AppDomain.CurrentDomain.BaseDirectory}/Image/sampleImg.jpg"))
            {
                newNews.File = new FormFile(stream, 0, stream.Length, "testfile", Path.GetFileName(stream.Name))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/jpg"
                };
            }

            var httpContent = GetMultipartFormData(newNews);

            var response = await client.PostAsync("api/News", httpContent);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteNewsArticleById()
        {
            var client = BuildHttpClient();

            var response = await client.DeleteAsync($"/api/News/{ValidNewsId}");
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        private HttpClient BuildHttpClient()
        {
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddAuthentication("Test")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>(
                            "Test", options => { });
                });
            }).CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test", $"name=faruk&nameIdentifier=2");
            return client;
        }

        private MultipartFormDataContent GetMultipartFormData<T>(T model)
        {
            var props = model.GetType().GetProperties();
            var content = new MultipartFormDataContent();

            foreach (var prop in props)
            {
                var value = prop.GetValue(model);
                if (value != null)
                {
                    string json = JsonConvert.SerializeObject(value);
                    var propValue = new StringContent(json);
                    if (value is IFormFile)
                    {
                        content.Add(propValue, prop.Name, $"testFileName{DateTime.UtcNow.Ticks}.jpg");
                    }
                    else
                    {
                        content.Add(propValue, prop.Name);
                    }
                }
            }

            return content;
        }
    }
}
