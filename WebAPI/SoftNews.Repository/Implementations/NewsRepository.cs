using Microsoft.EntityFrameworkCore;
using SoftNews.Data.Models;
using SoftNews.Repository.DbContext;
using SoftNews.Repository.Interfaces;
using SoftNews.Shared;
using System.Linq;
using System.Threading.Tasks;

namespace SoftNews.Repository.Implementations
{
    public class NewsRepository : INewsRepository
    {
        private readonly SoftNewsContext context;
        public NewsRepository(SoftNewsContext context)
        {
            this.context = context;
        }

        public async Task<int> AddAsync(News news)
        {
            await context.News.AddAsync(news);
            await context.SaveChangesAsync();
            return news.Id;
        }

        public async Task<PagedList<News>> GetByName(NewsParams newsParams)
        {
            var items = context.News
                .Where(x => x.Name.Contains(newsParams.searchQuery) || string.IsNullOrEmpty(newsParams.searchQuery));

            items = items.Where(x => !x.IsDeleted);

            if (false && newsParams.AuthorId.HasValue)
                items = items.Where(x => x.AuthorId == newsParams.AuthorId);

            return await PagedList<News>.CreateAsync(items, newsParams.PageNumber, newsParams.pageSize);
        }

        public async Task<News> GetById(int id)
        {
            return await context.News.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateNews(News news)
        {
            context.News.Update(news);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var news = await context.News.SingleAsync(x => x.Id == id);
            news.IsDeleted = true;
            context.SaveChanges();
        }

        public async Task<PagedList<News>> GetByNameForPublic(NewsParams newsParams)
        {
            {
                var items = context.News.Where(x => x.Name.Contains(newsParams.searchQuery) || string.IsNullOrEmpty(newsParams.searchQuery));
                items = items.Where(x => x.AuthorId == newsParams.AuthorId);

                return await PagedList<News>.CreateAsync(items, newsParams.PageNumber, newsParams.pageSize);
            }
        }
    }
}
