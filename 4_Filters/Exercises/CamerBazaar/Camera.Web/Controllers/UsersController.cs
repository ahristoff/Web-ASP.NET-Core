
namespace Camera.Web.Controllers
{
    using Camera.Data.Models;
    using Camera.Services;
    using Camera.Services.Models;
    using Camera.Web.Infrastructure.Filters;
    using Camera.Web.Models.AccountViewModels;
    using Camera.Web.Models.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public class UsersController : Controller
    {
        private readonly IUsersService users;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UsersController(IUsersService users, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.users = users;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [Authorize]
        [Route("/users/userdetails/{id}")]
        public IActionResult UserDetails(string id)
        {
            var user = this.users.UserDetails(id);

            var userId = user.Id;

            var currUserId = this.userManager.GetUserId(User);

            if (userId != currUserId)
            {
                this.TempData["ErrorMessage"] = "unauthorized access";
                return RedirectToAction(nameof(CamerasController.All), "Cameras");
            }

            var lastLoginTime = "";
      
            using (StreamReader sr = new StreamReader(@"Infrastructure\Filters\Logs\lastLogin.txt"))
            {
                lastLoginTime = sr.ReadLine();
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
        public ActionResult Logouts()
        {
            return RedirectToAction(nameof(AccountController.Logout), "Account");
        }
    }
}
