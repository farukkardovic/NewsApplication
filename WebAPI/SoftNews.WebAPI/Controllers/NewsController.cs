using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftNews.Data.Models;
using SoftNews.Service.Interfaces;
using SoftNews.Shared;
using SoftNews.WebAPI.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SoftNews.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly IMapper _mapper;

        public NewsController(INewsService newsService, IMapper mapper)
        {
            _newsService = newsService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetByName")]
        public async Task<IActionResult> GetByName([FromQuery]NewsParams newsParams)
        {
            var authorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

            if (!string.IsNullOrEmpty(authorId))
            {
                newsParams.AuthorId = int.Parse(authorId);
            }

            var news = await _newsService.GetByName(newsParams);
            var newsToReturn = _mapper.Map<IEnumerable<News>>(news);

            Response.AddPagination(news.CurrentPage, news.PageSize, news.TotalCount, news.TotalPages);
            return Ok(newsToReturn);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetById")]
        public async Task<News> GetById([FromQuery]int id)
        {
            return await _newsService.GetById(id);
        }

        [HttpPost]
        public async Task<IActionResult> AddNews([FromForm] NewsAddViewModel news)
        {
            using (var ms = new MemoryStream())
            {
                news.File.CopyTo(ms);
                var fileBytes = ms.ToArray();

                var newsForCreate = new News
                {
                    Name = news.Name,
                    Description = news.Description,
                    CategoryId = news.CategoryId,
                    AuthorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                    Image = fileBytes,
                    ImageType = news.File.ContentType
                };

                await _newsService.AddAsync(newsForCreate);
            }

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateNews([FromForm] NewsAddViewModel news)
        {
            using (var ms = new MemoryStream())
            {
                news.File.CopyTo(ms);
                var fileBytes = ms.ToArray();

                var newsForEdit = new News
                {
                    Name = news.Name,
                    Description = news.Description,
                    CategoryId = news.CategoryId,
                    AuthorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                    Image = fileBytes,
                    ImageType = news.File.ContentType,
                    Id = news.Id
                };

                await _newsService.UpdateAsync(newsForEdit);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNews(int id)
        {
            await _newsService.DeleteAsync(id);
            return NoContent();
        }
    }
}