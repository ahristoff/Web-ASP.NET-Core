
namespace Camera.Services.Models
{
    using System.ComponentModel.DataAnnotations;

    public class AllUserModel
    {
        public string Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }
    }
}
