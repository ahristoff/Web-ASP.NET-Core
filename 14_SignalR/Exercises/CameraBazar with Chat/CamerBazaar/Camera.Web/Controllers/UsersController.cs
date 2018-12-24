
namespace Camera.Web.Controllers
{
    using Camera.Data.Models;
    using Camera.Services;
    using Camera.Services.Models;
    using Camera.Web.Infrastructure.Filters;
    using Camera.Web.Models.AccountViewModels;
    using Camera.Web.Models.ManageViewModels;
    using Camera.Web.Models.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class UsersController : Controller
    {
        private readonly IUsersService users;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UsersController(IUsersService users, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.users = users;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        [Authorize]
        [Route("/users/userdetails/{id}")]
        public IActionResult UserDetails(string id)
        {
            var user = this.users.UserDetails(id);
            
            var userId = user.Id;
            var currUserId = this.userManager.GetUserId(User);

            var lastLoginTime = "";
            if (userId == currUserId)
            {
                using (StreamReader sr = new StreamReader(@"Infrastructure\Filters\Logs\lastLogin.txt"))
                {
                    lastLoginTime = sr.ReadLine();
                }

                ViewData["message"] = $"Hello {user.Username}      LastLogin Time: {lastLoginTime}";
            }
            
        
            return View(new UserDetailsViewModel
            {
                Cameras = user.Cameras,
                CountInStockCameras = user.CountInStockCameras,
                CountOutOfStockCameras = user.CountOutOfStockCameras,
                Email = user.Email,
                Id = user.Id,
                Phone = user.Phone,
                Username = user.Username,
                LastLoginTime = lastLoginTime
            });
        }

        [Authorize]
        [AutoValidateAntiforgeryToken]
        [Route("users/edit/{id}")]
        public IActionResult Edit(string id)
        {
            var user = this.users.Edit(id);
            var userId = user.Id;

            var currUserId = this.userManager.GetUserId(User);

            if (userId != currUserId)
            {
                this.TempData["ErrorMessage"] = "unauthorized access";
                return RedirectToAction(nameof(UserDetails));              
            }
            return View(user);
        }

        [Authorize]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        [Route("users/edit/{id}")]
        public async Task<IActionResult> Edit(string id, EditUserProfileModel profileModel)
        {
            if (!ModelState.IsValid)
            {
                return View(profileModel);
            }

            users.SaveEdit(profileModel.Id, profileModel.Email, profileModel.Phone);

            var user = await this.userManager.FindByIdAsync(id);
            var result = await this.userManager.ChangePasswordAsync(user, profileModel.CurrentPassword, profileModel.NewPassword);

            if (result.Succeeded)
            {
                this.TempData["SuccessMessage"] = $"Data changed for user {user.Email}";

                return RedirectToAction(nameof(UserDetails));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    this.TempData["ErrorMessage"] = error.Description; 
                }

                return View(profileModel);
            }
        }
        //------------------------------------------------------
        [Route("users/myprofile")]
        public IActionResult MyProfile()
        {
            var userId = userManager.GetUserId(User);

            return RedirectToAction(nameof(UserDetails), new { id = userId});
        }

        [Route("users/editmyprofile")]
        public IActionResult EditMyProfile()
        {
            var userId = userManager.GetUserId(User);

            return RedirectToAction(nameof(Edit), new { id = userId });
        }
    
        [Route("users/logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        //------------------------------------------------------------------

        [Authorize(Roles = "Administrator, Moderator")]
        [Route("users/all")]
        public IActionResult AllUsers()
        {
            var users = this.users.AllUsers();

            return View(users);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [Route("users/changePassword/{id}")]
        public async Task<IActionResult> ChangePassword(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return this.View(new ChangeUserPassword
            {
                Email = user.Email
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Moderator")]
        [Route("users/changePassword/{id}")]
        public async Task<IActionResult> ChangePassword(string id, ChangeUserPassword model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await this.userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            //change password from Admin
            var token = await this.userManager.GeneratePasswordResetTokenAsync(user);

            var result = await this.userManager.ResetPasswordAsync(user, token, model.Password);

            if (result.Succeeded)
            {
                this.TempData["SuccessMessage"] = $"Password changed for user {user.Email}";

                return RedirectToAction(nameof(AllUsers));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description); //string.Empty is key
                }
                return View(model);
            }
        }

        [Authorize(Roles = "Administrator")]
        [Route("users/delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(new DeleteUser
            {
                Id = id,
                Email = user.Email
            });
        }

        [Authorize(Roles = "Administrator")]
        [Route("users/destroy/{id}")]
        public async Task<IActionResult> Destroy(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            await this.userManager.DeleteAsync(user);

            TempData["SuccessMessage"] = $"User deleted {user.Email}";

            return RedirectToAction(nameof(AllUsers));
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [Route("users/roles/{id}")]
        public async Task<IActionResult> Roles(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var roles = await this.userManager.GetRolesAsync(user);

            return View(new UserWithRoles
            {
                Id = user.Id,
                Email = user.Email,
                Roles = roles
            });
        }

        [Authorize(Roles = "Administrator")]
        [Route("users/addToRoles/{id}")]
        public IActionResult AddToRole(string id)
        {
            var rolesDropDown = this.roleManager.Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToList();

            return View(rolesDropDown);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("users/addToRoles/{id}")]
        public async Task<IActionResult> AddToRole(string id, string role)//role -> View AddToRole->select-name=role
        {
            var user = await this.userManager.FindByIdAsync(id);
            var roleExists = await this.roleManager.RoleExistsAsync(role);
            if (user == null || !roleExists)
            {
                return NotFound();
            }

            await this.userManager.AddToRoleAsync(user, role);

            TempData["Success"] = $"User {user.Email} added to {role} role";

            return RedirectToAction(nameof(AllUsers));
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult RemoveToRole(string id)
        {
            var rolesDropDown = this.roleManager.Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToList();

            return View(rolesDropDown);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveToRole(string id, string role)
        {
            var user = await this.userManager.FindByIdAsync(id);
            var roleExists = await this.roleManager.RoleExistsAsync(role);
            if (user == null || !roleExists)
            {
                return NotFound();
            }
            if (!await userManager.IsInRoleAsync(user, role))
            {
                TempData["Error"] = $"User {user.Email} has not {role} role";

                return RedirectToAction(nameof(AllUsers));
            }
            await this.userManager.RemoveFromRoleAsync(user, role);

            TempData["Success"] = $"User {user.Email} was removed from {role} role";

            return RedirectToAction(nameof(AllUsers));
        }
    }
}
