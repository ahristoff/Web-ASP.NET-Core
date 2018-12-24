
namespace LearningSystem.Web.Controllers
{
    using LearningSystem.Services.Home.Interfaces;
    using LearningSystem.Web.Models;
    using LearningSystem.Web.Models.Home;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly ICourseService courses;
        private readonly IUserService users;

        public HomeController(ICourseService courses, IUserService users)
        {
            this.courses = courses;
            this.users = users;
        }

        public async Task<IActionResult> Index()
        {
            return View(new HomeIndexViewModel
            {
                Courses = await this.courses.ActiveCoursesAsync()
            });
        }

        public async Task<IActionResult> Search(SearchFormViewModel model)
        {
            var viewModel = new AllSearchViewModel
            {
                SearchText = model.SearchText
            };

            if (model.SearchInCourses)
            {
                viewModel.SearchInCourses = true;
                viewModel.Courses = await this.courses.FindCoursesAsync(model.SearchText);
            }

            if (model.SearchInUsers)
            {
                viewModel.SearchInUsers = true;
                viewModel.Users = await this.users.FindUsersAsync(model.SearchText);
            }

            return View(viewModel);
        }
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
