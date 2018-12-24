
namespace LearningSystem.Services.Home.Services
{
    using AutoMapper.QueryableExtensions;
    using LearningSystem.Data;
    using LearningSystem.Data.Models;
    using LearningSystem.Services.Home.Interfaces;
    using LearningSystem.Services.Home.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CourseService : ICourseService
    {
        private readonly LearningSystemDbContext db;

        public CourseService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<CoursesListingServiceModel>> ActiveCoursesAsync()
        {
            return await this.db.Courses
                .OrderBy(c => c.StartDate)
                .Where(c => c.StartDate >= DateTime.Now)
                .ProjectTo<CoursesListingServiceModel>()
                .ToListAsync();
        }

        public async Task<TModel> CourseByIdAsync<TModel>(int courseId) where TModel: class
        {
            return await this.db.Courses
                .Where(c => c.Id == courseId)
                .ProjectTo<TModel>()
                .FirstOrDefaultAsync();
        }
        
        public async Task<bool> UserIsSignedInCourse(int courseId, string userId)
        {
            return await this.db.Courses
                .AnyAsync(c => c.Id == courseId && c.Students.Any(s => s.StudentId == userId));
        }

        public async Task<bool> SignInUser(int courseId, string userId)
        {
            var courseInfo = await this.db.Courses
                .Where(c => c.Id == courseId)
                .Select(c => new
                {
                    c.StartDate,
                    UserIdSignedIn = c.Students.Any(s => s.StudentId == userId)
                })
                .FirstOrDefaultAsync();

            if (courseInfo == null || courseInfo.StartDate < DateTime.Now || courseInfo.UserIdSignedIn == true)
            {
                return false;
            }

            var studentInCourse = new StudentCourse()
            {
                CourseId = courseId,
                StudentId = userId
            };

            this.db.StudentCourses.Add(studentInCourse);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SignOutUser(int courseId, string userId)
        {
            var courseInfo = await this.db.Courses
                .Where(c => c.Id == courseId)
                .Select(c => new
                {
                    c.StartDate,
                    UserIdSignedIn = c.Students.Any(s => s.StudentId == userId)
                })
                .FirstOrDefaultAsync();

            if (courseInfo == null || courseInfo.StartDate < DateTime.Now || courseInfo.UserIdSignedIn == false)
            {
                return false;
            }

            var studentInCourse = await this.db.StudentCourses
                .Where(sc => sc.CourseId == courseId && sc.StudentId == userId)
                .FirstOrDefaultAsync();

            this.db.StudentCourses.Remove(studentInCourse);
            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<CoursesListingServiceModel>> FindCoursesAsync(string searchText)
        {
            searchText = searchText ?? string.Empty;

            return await this.db.Courses
                .OrderByDescending(c => c.StartDate)
                .Where(c => c.Name.ToLower().Contains(searchText.ToLower()))
                .ProjectTo<CoursesListingServiceModel>()
                .ToListAsync();
        }
        //------------------------------------------------------------------------------
        public async Task<bool> SaveExamSubmission(int courseId, string userId, byte[] fileExamSolutionContents)
        {
            var studentInCourse = await this.db.FindAsync<StudentCourse>(courseId, userId);

            if (studentInCourse == null)
            {
                return false;
            }

            studentInCourse.ExamSubmission = fileExamSolutionContents;

            await this.db.SaveChangesAsync();

            return true;
        }
    }
}
