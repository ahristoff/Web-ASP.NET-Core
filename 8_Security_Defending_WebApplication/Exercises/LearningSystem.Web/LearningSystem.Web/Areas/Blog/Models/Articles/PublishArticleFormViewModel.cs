
namespace LearningSystem.Web.Areas.Blog.Models.Articles
{
    using LearningSystem.Data;
    using System.ComponentModel.DataAnnotations;

    public class PublishArticleFormViewModel
    {
        [Required]
        [MaxLength(DataConstants.ArticleTitleMaxLenght)]
        [MinLength(DataConstants.ArticleTitleMinLenght)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
