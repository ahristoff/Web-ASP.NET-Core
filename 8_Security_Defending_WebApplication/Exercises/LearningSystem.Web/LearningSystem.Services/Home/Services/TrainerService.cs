
namespace LearningSystem.Services.Home.Services
{
    using AutoMapper.QueryableExtensions;
    using LearningSystem.Data;
    using LearningSystem.Data.Models;
    using LearningSystem.Services.Home.Interfaces;
    using LearningSystem.Services.Home.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TrainerService : ITrainerService
    {
        private readonly LearningSystemDbContext db;

        public TrainerService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<CoursesListingServiceModel>> Courses(string trainerId)
        {
            return await this.db
                .Courses
                .Where(c => c.TrainerId == trainerId)
                .ProjectTo<CoursesListingServiceModel>()
                .ToListAsync();
        }
        
        public async Task<IEnumerable<StudentInCourseServiceModel>> StudentsInCourse(int courseId)
        {
            return await this.db.Courses
                 .Where(c => c.Id == courseId)
                 .SelectMany(c => c.Students.Select(s => s.Student))
                 .ProjectTo<StudentInCourseServiceModel>(new { currcourseId = courseId})
                 .ToListAsync();
        }

        public async Task<bool> IsTrainer(int courseId, string trainerId)
        {
            return await this.db.Courses
                 .AnyAsync(c => c.Id == courseId && c.TrainerId == trainerId);
                
        }

        public async Task<bool> AddGrade(int courseId, string studentId, Grade grade)
        {
            var studentInCourse = await this.db.FindAsync<StudentCourse>(courseId, studentId);

            if (studentInCourse == null)
            {
                return false;
            }

            studentInCourse.Grade = grade;

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<byte[]> GetExamSolutionSubmission(int courseId, string studentId)
        {
            var studentInCourse = await this.db.FindAsync<StudentCourse>(courseId, studentId);

            if (studentInCourse == null)
            {
                return null;
            }

            return studentInCourse.ExamSubmission;
        }

        public async Task<StudentInCourseNamesServiceModel> StudentInCourseNames(int courseId, string studentId)
        {
            var username = await this.db.Users
                .Where(u => u.Id == studentId)
                .Select(u => u.UserName)
                .FirstOrDefaultAsync();

            if (username == null)
            {
                return null;
            }

            var courseName = await this.db.Courses
                .Where(c => c.Id == courseId)
                .Select(c => c.Name)
                .FirstOrDefaultAsync();

            if (courseName == null)
            {
                return null;
            }

            return new StudentInCourseNamesServiceModel
            {
                CourseTitle = courseName,
                UserName = username
            };
        }
    }
}
