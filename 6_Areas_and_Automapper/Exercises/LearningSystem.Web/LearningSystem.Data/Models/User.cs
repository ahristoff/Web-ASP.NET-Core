
namespace LearningSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {
        [Required]
        [MinLength(DataConstants.UserNameMinLenght)]
        [MaxLength(DataConstants.UserNameMaxLenght)]
        public string Name { get; set; }

        public DateTime BirthDay { get; set; }


        //Trainer user lead many courses
        public List<Course> Trainings { get; set; } = new List<Course>();

        public List<Article> Articles { get; set; } = new List<Article>();

        public List<StudentCourse> Courses { get; set; } = new List<StudentCourse>();
    }
}
