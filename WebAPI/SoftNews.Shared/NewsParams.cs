namespace SoftNews.Shared
{
    public class NewsParams
    {
        private const int MaxPageSize = 50;
        public int pageSize = 3;
        public int PageNumber { get; set; } = 1;

        public string searchQuery { get; set; }
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
        public int? AuthorId { get; set; }
    }
}
