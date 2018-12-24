
namespace IdentityDemo.Models.Identity
{
    using System;
    using System.Collections.Generic;

    public class UserWithRolesViewModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public IEnumerable<String> Roles { get; set; }
    }
}
