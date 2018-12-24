
namespace LearningSystem.Services.Blog.Services
{
    using AutoMapper.QueryableExtensions;
    using LearningSystem.Data;
    using LearningSystem.Data.Models;
    using LearningSystem.Services.Blog.Models;
    using LearningSystem.Services.Blog.ServicesInterfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class BlogArticleService : IBlogArticleService
    {
        private readonly LearningSystemDbContext db;

        public BlogArticleService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<BlogArticleListingServiceModel>> All(int page = 1)
        {
            return  await this.db.Articles
                .OrderByDescending(a => a.PublishDate)
                .Skip((page - 1) * ServiceConstants.BlogArticlesPageSize)
                .Take(ServiceConstants.BlogArticlesPageSize)
                .ProjectTo<BlogArticleListingServiceModel>()
                .ToListAsync();
        }

        public async Task<int> Total()
        {
            return await this.db.Articles.CountAsync();
        }

        public async Task Create(string title, string content, string authorId)
        {
            var article = new Article()
            {
                Title = title,
                Content = content,
                PublishDate = DateTime.Now,
                AuthorId = authorId
            };

            this.db.Articles.Add(article);
            await db.SaveChangesAsync();
        }

        public async Task<BlogArticleDetailsServiceModel> DetailsById(int id)
        {
            return await this.db.Articles
                     .Where(a => a.Id == id)
                     .ProjectTo<BlogArticleDetailsServiceModel>()
                     .FirstOrDefaultAsync();
        }
    }
}
