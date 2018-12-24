
namespace LearningSystem.Services.Blog.ServicesInterfaces
{
    using LearningSystem.Services.Blog.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBlogArticleService
    {
        Task Create(string title, string content, string authorId);

        Task<IEnumerable<BlogArticleListingServiceModel>> All(int page = 1);

        Task<int> Total();

        Task<BlogArticleDetailsServiceModel> DetailsById(int id);
    }
}
