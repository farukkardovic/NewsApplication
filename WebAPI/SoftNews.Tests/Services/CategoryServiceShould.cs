using NSubstitute;
using SoftNews.Data.Models;
using SoftNews.Repository.Interfaces;
using SoftNews.Service.Implementations;
using SoftNews.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SoftNews.Tests.Services
{
    public class CategoryServiceShould
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryService _categoryService;

        private readonly List<Category> listCategories = new List<Category>
        {
            new Category{Id = 2,Name = "Sport"},
            new Category{Id = 3,Name = "Business"}
        };

        public CategoryServiceShould()
        {
            _categoryRepository = Substitute.For<ICategoryRepository>();
            _categoryService = new CategoryService(_categoryRepository);
            _categoryRepository.GetCategories().Returns(listCategories);
        }

        [Fact]
        public async Task ReturnListOfCategoriesAsync()
        {
            var list = await _categoryService.GetCategories();
            Assert.NotEmpty(list);
        }
    }
}
