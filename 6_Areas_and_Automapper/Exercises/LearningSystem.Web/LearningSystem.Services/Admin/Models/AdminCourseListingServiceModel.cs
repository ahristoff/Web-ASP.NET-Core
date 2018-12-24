
namespace LearningSystem.Services.Admin.Models
{
    using LearningSystem.Common.Mapping;
    using LearningSystem.Data.Models;

    public class AdminCourseListingServiceModel: IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
