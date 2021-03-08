using SoftNews.Data.Models;
using SoftNews.Repository.Interfaces;
using SoftNews.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftNews.Service.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryService(ICategoryRepository 
            categoryRepository) => this.categoryRepository = categoryRepository;

        public Task<List<Category>> GetCategories() 
            => categoryRepository.GetCategories();
    }
}
