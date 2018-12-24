
namespace LearningSystem.Web.Areas.Admin.Models.Users
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddUserToRoleFormViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public IEnumerable<string> Roles { get; set; }
    }
}
