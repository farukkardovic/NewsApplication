using SoftNews.Service.Interfaces;
using System.Threading.Tasks;
using SoftNews.Repository.Interfaces;
using SoftNews.Data.Models;
using SoftNews.Shared;

namespace SoftNews.Service.Implementations
{
    public class NewsService: INewsService
    {
        private INewsRepository newsRepository;

        public NewsService(INewsRepository newsRepository)
        {
            this.newsRepository = newsRepository;
        }

        public async Task<int> AddAsync(News news) 
            => await newsRepository.AddAsync(news);

        public async Task DeleteAsync(int id) 
            => await newsRepository.Delete(id);

        public async Task<News> GetById(int id) 
            => await newsRepository.GetById(id);

        public Task<PagedList<News>> GetByName(NewsParams newsParams) 
            => newsRepository.GetByName(newsParams);

        public async Task UpdateAsync(News news) 
            => await newsRepository.UpdateNews(news);

    }
}
