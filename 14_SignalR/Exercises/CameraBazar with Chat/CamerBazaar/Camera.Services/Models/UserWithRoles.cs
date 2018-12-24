
namespace Camera.Services.Models
{
    using System;
    using System.Collections.Generic;

    public class UserWithRoles
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public IEnumerable<String> Roles { get; set; }
    }
}
