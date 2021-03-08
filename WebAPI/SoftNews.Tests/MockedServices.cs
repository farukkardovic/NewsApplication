using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using SoftNews.Data.Models;
using SoftNews.Repository.DbContext;
using SoftNews.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftNews.Tests
{
    public static class MockedServices
    {
        private static readonly List<Category> listCategories = new List<Category>
        {
            new Category{Id = 2,Name = "Sport"},
            new Category{Id = 3,Name = "Business"}
        };

        private const int ValidNewsId = 10;

        public static IServiceCollection AddMockedServices(this IServiceCollection services)
        {
            var categoryService = Substitute.For<ICategoryService>();
            categoryService.GetCategories().Returns(listCategories);

            var newsService = Substitute.For<INewsService>();
            newsService.AddAsync(Arg.Any<News>()).Returns(Task.FromResult(1));
            newsService.DeleteAsync(Arg.Any<int>()).Returns(Task.FromResult(1));

            var news = new News
            {
                Image = System.IO.File.ReadAllBytes($"{AppDomain.CurrentDomain.BaseDirectory}/Image/sampleImg.jpg")
            };

            newsService.DeleteAsync(Arg.Is(ValidNewsId)).Returns(Task.FromResult(1));
            newsService.GetById(Arg.Is(ValidNewsId)).Returns(Task.FromResult(news));


            var options = new DbContextOptionsBuilder<SoftNewsContext>()
                            .UseSqlite("Filename=TestDatabase.db")
                            .Options;
            var dbContext = new SoftNewsContext(options);
            services.AddSingleton(dbContext);

            services.AddSingleton(categoryService);
            return services.AddSingleton(newsService);
        }
    }
}
