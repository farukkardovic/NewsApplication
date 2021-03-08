using Microsoft.EntityFrameworkCore;
using SoftNews.Data.Models;
using SoftNews.Repository.DbContext;
using SoftNews.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftNews.Repository.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SoftNewsContext dbContext;
        public CategoryRepository(SoftNewsContext dbContext) 
            => this.dbContext = dbContext;
        public async Task<List<Category>> GetCategories() 
            => await dbContext.Categories.ToListAsync();
    }
}
