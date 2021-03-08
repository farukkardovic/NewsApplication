using SoftNews.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftNews.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
    }
}
