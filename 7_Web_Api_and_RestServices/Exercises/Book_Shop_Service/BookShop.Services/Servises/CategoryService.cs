
namespace BookShop.Services.Servises
{
    using AutoMapper.QueryableExtensions;
    using BookShop.Data;
    using BookShop.Data.Models;
    using BookShop.Services.Interfaces;
    using BookShop.Services.Models.Categories;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoryService: ICategoryService
    {
        private readonly BookShopDbContext db;

        public CategoryService(BookShopDbContext db)
        {
            this.db = db;
        }
        
        public async Task<IEnumerable<CategoriesDetailsServiceModel>> AllCategories()             //9
        {
            var categories = await this.db.Categories
                .ProjectTo<CategoriesDetailsServiceModel>()
                .ToListAsync();

            return categories;
        }

        public async Task<CategoriesDetailsServiceModel> CategoryById(int categoryId)             //10
        {
            var currentCategory = await this.db.Categories
                .Where(c => c.Id == categoryId)
                .ProjectTo<CategoriesDetailsServiceModel>()
                .FirstOrDefaultAsync();

            return currentCategory;
        }

        public async Task<Category> CategoryByIdFromDb(int categoryId)                            //11
        {
            var category = await this.db.Categories
                .Where(c => c.Id == categoryId)
                .FirstOrDefaultAsync();

            return category;
        }

        public async Task SaveChangedCategory(int categoryId, string name)                         //11
        {
            var category = await CategoryByIdFromDb(categoryId);

            category.Name = name;

            await this.db.SaveChangesAsync();
        }

        public async Task DeleteCategory(int categoryId)                                           //12
        {
            var category = await CategoryByIdFromDb(categoryId);

            this.db.Categories.Remove(category);
            await this.db.SaveChangesAsync();
        }

        public async Task<int> CreateCategory(string name)                                         //13
        {
            var category = new Category
            {
                Name = name
            };

            if (this.db.Categories.Any(c => c.Name == name))
            {
                return 0;
            }

            this.db.Categories.Add(category);
            await this.db.SaveChangesAsync();

            return category.Id;
        }
    }
}
