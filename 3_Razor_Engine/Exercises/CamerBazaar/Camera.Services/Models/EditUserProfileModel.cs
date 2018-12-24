
namespace Camera.Services.Models
{
    using System.ComponentModel.DataAnnotations;

    public class EditUserProfileModel
    {
        public string Id { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string CurrentPassword { get; set; }
    }
}
