
namespace LearningSystem.Web.Models.Trainer
{
    using LearningSystem.Services.Home.Models;
    using System.Collections.Generic;

    public class StudentsInCourseViewModel
    {
        public IEnumerable<StudentInCourseServiceModel> Students { get; set; }

        public CoursesListingServiceModel Course { get; set; }
    }
}
