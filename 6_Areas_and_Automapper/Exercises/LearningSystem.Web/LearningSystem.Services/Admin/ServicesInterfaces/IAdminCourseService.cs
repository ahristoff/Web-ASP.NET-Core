
namespace LearningSystem.Services.Admin.ServicesInterfaces
{
    using LearningSystem.Services.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IAdminCourseService
    {
        Task Create(string nmae, string description, DateTime startDate, DateTime endDate, string trainerId);

        Task<IEnumerable<AdminCourseListingServiceModel>> AllCourses();

        Task Delete(int courseId);
    }
}
