using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SoftNews.WebAPI.ViewModels
{
    public class NewsAddViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public int? AuthorId { get; set; }            
        public IFormFile File { get; set; }
    }
}
