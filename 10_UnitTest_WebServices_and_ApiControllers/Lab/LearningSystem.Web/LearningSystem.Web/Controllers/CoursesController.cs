
namespace LearningSystem.Web.Controllers
{
    using LearningSystem.Data;
    using LearningSystem.Data.Models;
    using LearningSystem.Services.Home.Interfaces;
    using LearningSystem.Services.Home.Models;
    using LearningSystem.Web.Infrastructures.Extensions;
    using LearningSystem.Web.Models.Courses;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class CoursesController : Controller
    {
        private readonly ICourseService courses;
        private readonly UserManager<User> userManager;

        public CoursesController(ICourseService courses, UserManager<User> userManager)
        {
            this.courses = courses;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Details(int courseId)
        {
            var model = new CourseDetailsViewModel
            {
                Course = await this.courses.CourseByIdAsync<CourseDetailsServiceModel>(courseId),
            };

            if (model.Course == null)
            {
                return NotFound();
            }

            if (User.Identity.IsAuthenticated)
            {
                var userId = this.userManager.GetUserId(User);

                model.UserIsSignedInCourse = await this.courses.UserIsSignedInCourse(courseId, userId);
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SignIn(int id)
        {
            var userId = this.userManager.GetUserId(User);

            var result = await this.courses.SignInUser(id, userId);

            if (!result)
            {
                return NotFound();
            }
            TempData.AddSuccessMessage("You are signed In to this course!");

            return RedirectToAction(nameof(Details), new { id });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SignOut(int id)
        {
            var userId = this.userManager.GetUserId(User);

            var result = await this.courses.SignOutUser(id, userId);

            if (!result)
            {
                return NotFound();
            }
            TempData.AddSuccessMessage("Sorry to your sign out!");

            return RedirectToAction(nameof(Details), new { id });
        }

        //--------------------------Exam------------------------------------------

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SubmitExam(int courseId, IFormFile exam)
        {
            if (!exam.FileName.EndsWith(".zip") || exam.Length > DataConstants.CoursesExamSubmissionFileLenght || exam == null)
            {
                TempData.AddErrorMessage("Your file should be a '.zip' file with mx 2 MB in size!");
                //ModelState.AddModelError(string.Empty, "Your file should be a '.zip' file with mx 2 MB in size!");

                return RedirectToAction(nameof(Details), new { courseId });
            }

            //using (var memoryStream = new MemoryStream())
            //{
            //    await exam.CopyToAsync(memoryStream);
            //}

            var fileExamSolutionContents = await exam.ToByteArrayAsync();

            var userId = this.userManager.GetUserId(User);

            var success = await this.courses.SaveExamSubmission(courseId, userId, fileExamSolutionContents);

            if (!success)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage("Exam submission saved successfully!");

            return RedirectToAction(nameof(Details), new { courseId });
        }
    }
}
