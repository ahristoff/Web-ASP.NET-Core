
namespace BookShop.Services.Models.Authors
{
    using AutoMapper;
    using BookShop.Common.Mapping;
    using BookShop.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BooksByAuthorServiceModel: IMapFrom<Book>, IHaveCustomMapping            //3
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public decimal Price { get; set; }
        
        public int Copies { get; set; }

        public int? Edition { get; set; }

        public int? AgeRestriction { get; set; }

        public DateTime ReleaseDate { get; set; }
        

        public IEnumerable<string> Categories { get; set; }



        public virtual void ConfigureMapping(Profile profile)
        {
            profile
                .CreateMap<Book, BooksByAuthorServiceModel>()
                .ForMember(b => b.Categories, cfg => cfg.MapFrom
                (b => b.Categories.Select(c => c.Category.Name)));
        }
    }
}
