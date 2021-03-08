using Microsoft.AspNetCore.Mvc;
using SoftNews.Data.Models;
using SoftNews.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftNews.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<List<Category>> GetAll()
        {
            return await _categoryService.GetCategories();
        }
    }
}