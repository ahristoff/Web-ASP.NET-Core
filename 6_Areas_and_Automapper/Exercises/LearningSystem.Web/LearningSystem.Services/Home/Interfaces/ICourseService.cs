using LearningSystem.Services.Home.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningSystem.Services.Home.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<CoursesListingServiceModel>> ActiveCoursesAsync();

        Task<TModel> CourseByIdAsync<TModel>(int courseId) where TModel : class;

        Task<bool> UserIsSignedInCourse(int courseId, string userId);

        Task<bool> SignInUser(int courseId, string userId);

        Task<bool> SignOutUser(int courseId, string userId);

        //--------------------------Search----------------------------------------------

        Task<IEnumerable<CoursesListingServiceModel>> FindCoursesAsync(string searchText);

        Task<bool> SaveExamSubmission(int courseId, string userId, byte[] fileExamSolutionContents);
    }
}
