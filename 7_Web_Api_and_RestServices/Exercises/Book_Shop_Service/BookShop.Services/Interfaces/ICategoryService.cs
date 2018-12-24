
namespace BookShop.Services.Interfaces
{
    using BookShop.Data.Models;
    using BookShop.Services.Models.Categories;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        Task<IEnumerable<CategoriesDetailsServiceModel>> AllCategories();                 //9

        Task<CategoriesDetailsServiceModel> CategoryById(int categoryId);                 //10

        Task SaveChangedCategory(int categoryId, string name);                            //11

        Task<Category> CategoryByIdFromDb(int categoryId);                                //11

        Task DeleteCategory(int categoryId);                                              //12

        Task<int> CreateCategory(string name);                                            //13
    }
}
