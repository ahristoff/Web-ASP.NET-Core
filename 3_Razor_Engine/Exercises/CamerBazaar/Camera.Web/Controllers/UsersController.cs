
namespace Camera.Web.Controllers
{
    using Camera.Data.Models;
    using Camera.Services;
    using Camera.Services.Models;
    using Camera.Web.Infrastructure.Filters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    public class UsersController : Controller
    {
        private readonly IUsersService users;
        private readonly UserManager<User> userManager;

        public UsersController(IUsersService users, UserManager<User> userManager)
        {
            this.users = users;
            this.userManager = userManager;
        }

        [Authorize]
        [Route("users/userdetails/{id}")]
        public IActionResult UserDetails(string id)
        {
            var user = this.users.UserDetails(id);

            return View(user);
        }

        [Authorize]
       // [MyFilter("Pesho")]
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
    }
}
