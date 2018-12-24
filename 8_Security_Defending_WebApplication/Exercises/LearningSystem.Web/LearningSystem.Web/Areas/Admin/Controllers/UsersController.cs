
namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using LearningSystem.Data.Models;
    using LearningSystem.Services.ServicesInterfaces;
    using LearningSystem.Web.Areas.Admin.Models.Users;
    using LearningSystem.Web.Infrastructures.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class UsersController: BaseAdminController
    {
        private readonly IAdminUserService users;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public UsersController(IAdminUserService users, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.users = users;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await this.users.Allasync();
            var roles = await this.roleManager
                .Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToListAsync();

            return View(new AdminUserListingsViewModel
            {
                Users = users,
                Roles = roles
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(AddUserToRoleFormViewModel model)
        {
            foreach (var role in model.Roles)
            {
                var roleExists = await this.roleManager.RoleExistsAsync(role);
                if (!roleExists)
                {
                    ModelState.AddModelError(string.Empty, $"Invalid {role} Role ");
                }
            }

            var userExists = await this.userManager.FindByIdAsync(model.UserId) != null;

            if (!userExists)
            {
                ModelState.AddModelError(string.Empty, "InvalidIdentity Details");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            var user = await this.userManager.FindByIdAsync(model.UserId);

            await this.userManager.AddToRolesAsync(user, model.Roles);

            TempData.AddSuccessMessage($"User {user.UserName} successfully added to the {String.Join(", ",model.Roles)} role");

            return RedirectToAction(nameof(Index));
        }
    }
}
