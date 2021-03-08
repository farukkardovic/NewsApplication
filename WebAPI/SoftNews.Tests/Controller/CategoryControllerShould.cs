using System.Net;
using System.Threading.Tasks;
using WebAPI;
using Xunit;

namespace SoftNews.Tests.ControllerShould
{
    public class CategoryControllerShould : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        public CategoryControllerShould(CustomWebApplicationFactory<Startup> factory) 
            => _factory = factory;

        [Fact]
        public async Task ShouldReturnListOfCategoriesAsync()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/Category");

            var jsonString = await response.Content.ReadAsStringAsync();

            Assert.NotEmpty(jsonString);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
