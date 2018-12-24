
namespace LearningSystem.Web.Models.Home
{
    using LearningSystem.Services.Home.Models;
    using System.Collections.Generic;

    public class HomeIndexViewModel: SearchFormViewModel
    {
        public IEnumerable<CoursesListingServiceModel> Courses { get; set; }
    }
}
