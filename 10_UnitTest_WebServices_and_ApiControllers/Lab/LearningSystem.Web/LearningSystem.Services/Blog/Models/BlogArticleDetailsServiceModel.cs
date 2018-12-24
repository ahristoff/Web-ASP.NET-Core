﻿
namespace LearningSystem.Services.Blog.Models
{
    using AutoMapper;
    using LearningSystem.Common.Mapping;
    using LearningSystem.Data.Models;
    using System;

    public class BlogArticleDetailsServiceModel: IMapFrom<Article>, IHaveCustomMapping
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public string Author { get; set; }


        public void ConfigureMapping(Profile profile)
            => profile
                .CreateMap<Article, BlogArticleDetailsServiceModel>()
                .ForMember(a => a.Author, cfg => cfg.MapFrom(a => a.Author.UserName));

    }
}