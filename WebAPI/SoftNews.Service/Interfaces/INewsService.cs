using SoftNews.Data.Models;
using SoftNews.Shared;
using System.Threading.Tasks;

namespace SoftNews.Service.Interfaces
{
    public interface INewsService
    {
        Task<PagedList<News>> GetByName(NewsParams newsParams);
        Task<int> AddAsync(News news);
        Task UpdateAsync(News news);
        Task<News> GetById(int id);
        Task DeleteAsync(int id);
    }
}
