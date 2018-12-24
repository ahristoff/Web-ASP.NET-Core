
namespace LearningSystem.Web.Areas.Blog.Controllers
{
    using LearningSystem.Data.Models;
    using LearningSystem.Services.Blog.ServicesInterfaces;
    using LearningSystem.Services.Html;
    using LearningSystem.Web.Areas.Blog.Models.Articles;
    using LearningSystem.Web.Infrastructures.Extensions;
    using LearningSystem.Web.Infrastructures.Filters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Area(WebConstants.BlogArea)]
    [Authorize(Roles = WebConstants.BlogAuthorRole)]
    public class ArticlesController: Controller
    {
        public readonly IHtmlService html;
        public readonly IBlogArticleService articles;
        public readonly UserManager<User> userManeger;

        public ArticlesController(
            IHtmlService html, IBlogArticleService articles, UserManager<User> userManeger)
        {
            this.html = html;
            this.articles = articles;
            this.userManeger = userManeger;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1)
        {
            return View(new ArticleListingViewModel
            {
                Articles = await this.articles.All(page),
                TotalArticles = await this.articles.Total(),
                CurrentPage = page,
            });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Create(PublishArticleFormViewModel model)
        {
            model.Content = this.html.Sanitize(model.Content);

            var userId = this.userManeger.GetUserId(User);

            await this.articles.Create(model.Title, model.Content, userId);

            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            return this.ViewOrNotFound(await this.articles.DetailsById(id));
        }
    }
}
