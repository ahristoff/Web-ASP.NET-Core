
namespace LearningSystem.Services.Home.Interfaces
{
    using LearningSystem.Data.Models;
    using LearningSystem.Services.Home.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITrainerService
    {
        Task<IEnumerable<CoursesListingServiceModel>> Courses(string trainerId);

        Task<IEnumerable<StudentInCourseServiceModel>> StudentsInCourse(int courseId);

        Task<bool> IsTrainer(int courseId, string trainerId);

        Task<bool> AddGrade(int courseId, string studentId, Grade grade);

        Task<byte[]> GetExamSolutionSubmission(int courseId, string studentId);

        Task<StudentInCourseNamesServiceModel> StudentInCourseNames(int courseId, string studentId);
    }
}
