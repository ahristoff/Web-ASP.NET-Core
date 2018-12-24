
namespace LearningSystem.Web.Models.Trainer
{
    using LearningSystem.Services.Home.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class StudentsInCourseViewModel
    {
        public IEnumerable<StudentInCourseServiceModel> Students { get; set; }

        public CoursesListingServiceModel Course { get; set; }
    }
}
