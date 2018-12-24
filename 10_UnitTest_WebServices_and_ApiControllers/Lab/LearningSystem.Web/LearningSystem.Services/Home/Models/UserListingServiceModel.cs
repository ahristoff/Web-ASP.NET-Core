
namespace LearningSystem.Services.Home.Models
{
    using AutoMapper;
    using LearningSystem.Common.Mapping;
    using LearningSystem.Data.Models;

    public class UserListingServiceModel: IMapFrom<User>, IHaveCustomMapping
    {
        public string Username { get; set; }

        public string Name { get; set; }

        public int Courses { get; set; }



        public void ConfigureMapping(Profile profile)
        {
            profile
                .CreateMap<User, UserListingServiceModel>()
                .ForMember(u => u.Courses, cfg => cfg.MapFrom(c => c.Courses.Count));
        }
    }
}
