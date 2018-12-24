
namespace LearningSystem.Services.Blog.Models
{
    using AutoMapper;
    using LearningSystem.Common.Mapping;
    using LearningSystem.Data;
    using LearningSystem.Data.Models;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class BlogArticleListingServiceModel: IMapFrom<Article>, IHaveCustomMapping
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.ArticleTitleMinLenght)]
        [MinLength(DataConstants.ArticleTitleMaxLenght)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public string Author { get; set; }

        public void ConfigureMapping(Profile profile)
            => profile
            .CreateMap<Article, BlogArticleListingServiceModel>()
            .ForMember(a => a.Author, cfg => cfg.MapFrom(a => a.Author.UserName));
            
    }
}
