
namespace LearningSystem.Web.Areas.Admin.Models.Courses
{
    using LearningSystem.Data;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddCourseFormViewModel: IValidatableObject
    {
        [Required]
        [MaxLength(DataConstants.CourseNameMaxLenght)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DataConstants.CourseDescriptionMaxLenght)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        public string TrainerId { get; set; }

        public IEnumerable<SelectListItem> Trainers { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.StartDate < DateTime.Now)
            {
                yield return new ValidationResult("Start date should be in the future");
            }

            if (this.EndDate < this.StartDate)
            {
                yield return new ValidationResult("End date should be after start date");
            }
            
        }
    }
}
