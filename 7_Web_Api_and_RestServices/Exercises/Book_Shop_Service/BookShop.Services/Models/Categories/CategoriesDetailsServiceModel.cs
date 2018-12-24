
namespace BookShop.Services.Models.Categories
{
    using BookShop.Common.Mapping;
    using BookShop.Data.Models;

    public class CategoriesDetailsServiceModel: IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
