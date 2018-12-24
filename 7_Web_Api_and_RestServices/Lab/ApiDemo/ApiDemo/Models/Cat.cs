
namespace ApiDemo.Models
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using System.ComponentModel.DataAnnotations;

    public class Cat
    {
        [BindNever]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0, 20)]
        public int Age { get; set; }

        [Required]
        public string Color { get; set; }
    }
}
