
namespace LearningSystem.Services.Home.Models
{
    using AutoMapper;
    using LearningSystem.Common.Mapping;
    using LearningSystem.Data.Models;
    using System.Linq;

    public class StudentInCourseServiceModel:IMapFrom<User>, IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public Grade? Grade { get; set; }



        public void ConfigureMapping(Profile profile)
        {
            int currcourseId = 0;

            profile
                .CreateMap<User, StudentInCourseServiceModel>()
                .ForMember(s => s.Grade, cfg => cfg.MapFrom(u => u.Courses
                .Where(c => c.CourseId == currcourseId)
                .Select(s => s.Grade)
                .FirstOrDefault()));
        }
    }
}
