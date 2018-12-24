
namespace LearningSystem.Services.Admin.Services
{
    using AutoMapper.QueryableExtensions;
    using LearningSystem.Data;
    using LearningSystem.Data.Models;
    using LearningSystem.Services.Admin.Models;
    using LearningSystem.Services.Admin.ServicesInterfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AdminCourseService : IAdminCourseService
    {
        public LearningSystemDbContext db;

        public AdminCourseService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminCourseListingServiceModel>> AllCourses()
        {
            return await this.db.Courses
                .ProjectTo<AdminCourseListingServiceModel>()
                .ToListAsync();
        }

        public async Task Create(string name, string description, DateTime startDate, DateTime endDate, string trainerId)
        {
            var course = new Course
            {
                Name = name,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                TrainerId = trainerId
            };

            this.db.Courses.Add(course);
            await this.db.SaveChangesAsync();
        }

        public async Task Delete(int courseId)
        {
            var course = await db.Courses.FindAsync(courseId);

            db.Courses.Remove(course);
            db.SaveChanges();
        }
    }
}
