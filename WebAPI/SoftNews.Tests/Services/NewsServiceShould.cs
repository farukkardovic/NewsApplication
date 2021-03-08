using NSubstitute;
using SoftNews.Repository.Interfaces;
using SoftNews.Service.Implementations;
using SoftNews.Service.Interfaces;
using SoftNews.Shared;
using System.Threading.Tasks;
using Xunit;

namespace SoftNews.Tests.Services
{
    public class NewsServiceShould
    {
        private readonly INewsRepository _newsRepository;
        private readonly INewsService _newsService;

        private const int ValidId = 3;
        private const string ValidNewsName = "NewsName";

        public NewsServiceShould()
        {
            _newsRepository = Substitute.For<INewsRepository>();
            _newsService = new NewsService(_newsRepository);
        }

        [Fact]
        public async Task QueryWithCorrectParameters()
        {
            await _newsService.GetById(ValidId);
            await _newsRepository.Received(1).GetById(Arg.Is(ValidId));
        }

        [Fact]
        public async Task QueryWithCorrectParametersForName()
        {
            var actual = new NewsParams { searchQuery = ValidNewsName, AuthorId = 1, PageNumber = 12, PageSize = 10 };

            await _newsService.GetByName(actual);
            await _newsRepository.Received(1).GetByName(Arg.Is<NewsParams>(p => p.PageNumber == actual.PageNumber
                                            && p.PageNumber == actual.PageNumber && p.searchQuery == actual.searchQuery));
        }

        [Fact]
        public async Task PassArgumentToDeRepository()
        {
            await _newsService.DeleteAsync(ValidId);
            await _newsRepository.Received(1).Delete(Arg.Is(ValidId));
        }
    }
}
