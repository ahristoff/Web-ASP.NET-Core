
namespace ShopingCartDemo.Models
{
    using Microsoft.AspNetCore.Identity;
    using ShopingCartDemo.Data.Models;
    using System.Collections.Generic;

    public class ApplicationUser : IdentityUser
    {
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
