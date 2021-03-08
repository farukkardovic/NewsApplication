using SoftNews.Data.Models;
using SoftNews.Shared;
using System.Threading.Tasks;

namespace SoftNews.Repository.Interfaces
{
    public interface INewsRepository
    {
        Task<PagedList<News>> GetByName(NewsParams newsParams);
        Task<int> AddAsync(News news);
        Task UpdateNews(News news);
        Task<News> GetById(int id);
        Task Delete(int id);
    }
}
