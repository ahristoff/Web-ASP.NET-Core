
namespace LearningSystem.Services.Admin.ServicesInterfaces
{
    using System;
    using System.Threading.Tasks;

    public interface IAdminCourseService
    {
        Task Create(string nmae, string description, DateTime startDate, DateTime endDate, string trainerId);
    }
}
