
using System.ComponentModel.DataAnnotations;

namespace Camera.Services.Models
{
    public class EditUserProfileModel
    {
        public string Id { get; set; }

        public string NewPassword { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }

        public string CurrentPassword { get; set; }
    }
}
