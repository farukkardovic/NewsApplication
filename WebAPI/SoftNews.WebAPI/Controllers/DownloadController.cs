using Microsoft.AspNetCore.Mvc;
using SoftNews.Service.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace SoftNews.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class DownloadController : ControllerBase
    {
        private readonly INewsService _newsService;

        public DownloadController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Download(int id)
        {
            var news = await _newsService.GetById(id);

            if (news is null || news.Image is null)
            {
                return NotFound();
            }

            var stream = new MemoryStream(news.Image);
            return File(stream, "application/octet-stream");
        }
    }
}
