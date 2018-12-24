
namespace LearningSystem.Test.Services
{
    using Data;
    using FluentAssertions;
    using LearningSystem.Data.Models;
    using LearningSystem.Services.Home.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class CourseServiceTest
    {
        private LearningSystemDbContext db;
        private CourseService courseService;

        public CourseServiceTest()
        {
            Tests.Initialize();
            db = Tests.PrepareDatabase();
            courseService = new CourseService(db);
        }
        
        //-----------------tests in CourseService -> 2 tests---------------------

        [Fact]
        public async Task FindCoursesAsyncReturnListOfCourses()
        {
            //Arrange

            //initialize AutoMapper -> now in Tests
            //Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());

            //make fake Db
            //var dbOptions = new DbContextOptionsBuilder<LearningSystemDbContext>()
            //    .UseInMemoryDatabase(Guid.NewGuid().ToString())
            //    .Options;
            //var db = new LearningSystemDbContext(dbOptions);

            //var courseService = new CourseService(db);

            var firstCourse = new Course { Id = 1, Name = "First", StartDate = new DateTime(2018, 10, 01) };
            var secindCourse = new Course { Id = 2, Name = "Second", StartDate = new DateTime(2018, 11, 01) };
            var thirdCourse = new Course { Id = 3, Name = "Third", StartDate = new DateTime(2018, 12, 01) };

            await db.AddRangeAsync(firstCourse, secindCourse, thirdCourse);
            await db.SaveChangesAsync();

            //Act

            var result = await courseService.FindCoursesAsync("t");

            //Assert

            //whether orderByDescending is correct
            result.Should()
                .Match(r => r.ElementAt(0).Name == "Third" && r.ElementAt(1).Name == "First")
                .And
                .HaveCount(2);
        }

        [Fact]
        public async Task SignInUserShouldSaveCorrectData()
        {
            //Arrange
            var course = new Course
            {
                Id = 1,
                StartDate = DateTime.MaxValue,
                Students = new List<StudentCourse>()
            };

            db.Add(course);
            await db.SaveChangesAsync();
            

            //Act
            var result = await courseService.SignInUser(1, "testStudentId");
            var savedEntry = db.Find<StudentCourse>(1, "testStudentId");
           

            //Assert
            result.Should()
                .Be(true);

            savedEntry.Should()
                .NotBeNull();
        }
    }
}
