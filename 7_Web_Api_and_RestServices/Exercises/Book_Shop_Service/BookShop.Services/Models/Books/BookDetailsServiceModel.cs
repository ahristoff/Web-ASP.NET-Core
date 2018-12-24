
namespace BookShop.Services.Models.Books
{
    using AutoMapper;
    using BookShop.Common.Mapping;
    using BookShop.Data.Models;
    using BookShop.Services.Models.Authors;
    using System.Linq;

    public class BookDetailsServiceModel: BooksByAuthorServiceModel,IMapFrom<Book>, IHaveCustomMapping  //4
    {
        public string Author { get; set; }

        public override void ConfigureMapping(Profile profile)
        {
            profile
                .CreateMap<Book, BookDetailsServiceModel>()
                .ForMember(b => b.Categories, cfg => cfg.MapFrom
                        (b => b.Categories.Select(c => c.Category.Name)))
                .ForMember(a => a.Author, cfg => cfg.MapFrom
                        (a => a.Author.FirstName + " " + a.Author.LastName));
        }
    }
}
