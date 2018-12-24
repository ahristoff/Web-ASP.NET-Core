
namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using LearningSystem.Data.Models;
    using LearningSystem.Services.Admin.ServicesInterfaces;
    using LearningSystem.Web.Areas.Admin.Models.Courses;
    using LearningSystem.Web.Controllers;
    using LearningSystem.Web.Infrastructures.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class CoursesController: BaseAdminController
    {
        private readonly UserManager<User> userManager;
        private readonly IAdminCourseService courses;

        public CoursesController(UserManager<User> userManager, IAdminCourseService courses)
        {
            this.userManager = userManager;
            this.courses = courses;
        }

        public async Task<IActionResult> AllCourses()
        {
            return View(await courses.AllCourses());
        }


        public async Task<IActionResult> Create()
        {
            var trainers = await userManager.GetUsersInRoleAsync(WebConstants.TrainerRole);

            var trainerListItems = trainers
                .Select(t => new SelectListItem
                {
                    Text = t.UserName,
                    Value = t.Id
                })
                .ToList();

            return View(new AddCourseFormViewModel
            {
                Trainers = trainerListItems
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddCourseFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var trainers = await userManager.GetUsersInRoleAsync(WebConstants.TrainerRole);

                var trainerListItems = trainers
                    .Select(t => new SelectListItem
                    {
                        Text = t.UserName,
                        Value = t.Id
                    })
                    .ToList();

                return View(new AddCourseFormViewModel
                {
                    Trainers = trainerListItems
                });
            }


            var endDate = model.EndDate;
            await this.courses.Create(
                model.Name, 
                model.Description, 
                model.StartDate, 
                new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59), 
                model.TrainerId);

            TempData.AddSuccessMessage($"Course {model.Name} created successfully!");

            //return Redirect("/Home/Index");
            return RedirectToAction(nameof(HomeController.Index), "Home", new { area = string.Empty});
        }
        
        public async Task<IActionResult> DeleteCourse(int courseId)
        {
            await courses.Delete(courseId);

            return RedirectToAction(nameof(AllCourses));
        }
    }
}
