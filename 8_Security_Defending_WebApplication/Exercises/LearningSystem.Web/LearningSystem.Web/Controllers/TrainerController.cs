
namespace LearningSystem.Web.Controllers
{
    using LearningSystem.Data.Models;
    using LearningSystem.Services.Home.Interfaces;
    using LearningSystem.Services.Home.Models;
    using LearningSystem.Web.Infrastructures.Extensions;
    using LearningSystem.Web.Models.Trainer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    [Authorize(Roles = WebConstants.TrainerRole)]
    public class TrainerController: Controller
    {
        private readonly ITrainerService trainer;
        private readonly ICourseService courses;
        private readonly UserManager<User> userManager;

        public TrainerController(ITrainerService trainer, UserManager<User> userManager, ICourseService courses)
        {
            this.trainer = trainer;
            this.userManager = userManager;
            this.courses = courses;
        }

        public  async Task<IActionResult> Courses()
        {
            var userId = this.userManager.GetUserId(User);

            var courses = await this.trainer.Courses(userId);

            return View(courses);
        }

        public async Task<IActionResult> Students(int courseId)
        {
            var userId = this.userManager.GetUserId(User);

            if (!await this.trainer.IsTrainer(courseId, userId))
            {
                return NotFound();
            }
         
            return View(new StudentsInCourseViewModel
            {
                Students = await this.trainer.StudentsInCourse(courseId),
                Course =  await this.courses.CourseByIdAsync<CoursesListingServiceModel>(courseId)
            });
        }

        [HttpPost]
        public async Task<IActionResult> GradeStudent(int courseId, string studentId, Grade grade)
        {
            if (studentId == null)
            {
                return BadRequest();
            }

            var userId = this.userManager.GetUserId(User);

            if (!await this.trainer.IsTrainer(courseId, userId))
            {
                return BadRequest();
            }

            var success = await this.trainer.AddGrade(courseId, studentId, grade);

            if (!success)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Students), new { courseId});
        }

        public async Task<IActionResult> DownloadExam(int courseId, string studentId)
        {
            if (studentId == null)
            {
                return BadRequest();
            }
            var studentName = this.userManager.FindByIdAsync(studentId);

            var userId = this.userManager.GetUserId(User);

            if (!await this.trainer.IsTrainer(courseId, userId))
            {
                return BadRequest();
            }

            var studentInCourseNames = await this.trainer.StudentInCourseNames(courseId, studentId);

            if (studentInCourseNames == null)
            {
                return BadRequest();
            }

            var examSolutionSubmission = await this.trainer.GetExamSolutionSubmission(courseId, studentId);

            if (examSolutionSubmission == null)
            {
                TempData.AddErrorMessage($"No available exam solution from {studentInCourseNames.UserName}");
                return RedirectToAction(nameof(Students), new { courseId });
            }
            
            return File(examSolutionSubmission, "application/zip", $"{studentInCourseNames.CourseTitle}-{studentInCourseNames.UserName}-{DateTime.Now}.zip");
        }
    }
}
