using SoftNews.Data.Model;

namespace SoftNews.Data.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public User Author { get; set; }
        public int AuthorId { get; set; }
        public bool IsDeleted { get; set; }      
        public byte[] Image { get; set; }
        public string ImageType { get; set; }
    }
}
