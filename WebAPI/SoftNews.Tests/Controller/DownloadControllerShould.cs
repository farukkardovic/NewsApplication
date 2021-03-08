using System.Net;
using System.Threading.Tasks;
using WebAPI;
using Xunit;

namespace SoftNews.Tests.Controller
{
    public class DownloadControllerShould : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        private const int ValidNewsId = 10;
        private const int InvalidalidNewsId = 13;

        public DownloadControllerShould(CustomWebApplicationFactory<Startup> factory)
            => _factory = factory;

        [Fact]
        public async Task ReturnImageFileGivenValidNewsId()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync($"/api/Download/{ValidNewsId}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ReturnNotFoundGivenInvalidNewsId()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync($"/api/Download/{InvalidalidNewsId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
