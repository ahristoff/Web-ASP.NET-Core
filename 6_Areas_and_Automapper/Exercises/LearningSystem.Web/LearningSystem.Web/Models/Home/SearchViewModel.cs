
namespace LearningSystem.Web.Models.Home
{
    using LearningSystem.Services.Home.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SearchViewModel
    {
        public IEnumerable<CoursesListingServiceModel> Courses { get; set; } = new List<CoursesListingServiceModel>();

        public IEnumerable<UserListingServiceModel> Users { get; set; } = new
            List<UserListingServiceModel>();

        public string SearchText { get; set; }
    }
}
