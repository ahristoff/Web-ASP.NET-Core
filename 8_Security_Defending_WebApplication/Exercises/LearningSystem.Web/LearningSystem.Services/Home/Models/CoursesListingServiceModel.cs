
namespace LearningSystem.Services.Home.Models
{
    using LearningSystem.Common.Mapping;
    using LearningSystem.Data.Models;
    using System;

    public class CoursesListingServiceModel: IMapFrom<Course>
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
