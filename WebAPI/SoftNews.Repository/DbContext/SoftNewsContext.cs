using Microsoft.EntityFrameworkCore;
using SoftNews.Data.Model;
using SoftNews.Data.Models;

namespace SoftNews.Repository.DbContext
{
    public class SoftNewsContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public SoftNewsContext(DbContextOptions<SoftNewsContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<News> News { get; set; }
    }
}
