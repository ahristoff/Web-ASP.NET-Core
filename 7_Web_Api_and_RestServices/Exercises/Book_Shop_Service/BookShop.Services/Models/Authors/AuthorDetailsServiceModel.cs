
namespace BookShop.Services.Models.Authors
{
    using AutoMapper;
    using BookShop.Common.Mapping;
    using BookShop.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class AuthorDetailsServiceModel: IMapFrom<Author>, IHaveCustomMapping          //1
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public IEnumerable<string> BookTitles { get; set; }

        
        public void ConfigureMapping(Profile profile)
        {
            profile
                 .CreateMap<Author, AuthorDetailsServiceModel>()
                 .ForMember(a => a.BookTitles, cfg => cfg.MapFrom(a => a.Books.Select(b => b.Title)));
        }
    }
}
