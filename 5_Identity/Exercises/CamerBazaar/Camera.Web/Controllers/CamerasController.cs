
namespace Camera.Web.Controllers
{
    using Camera.Data.Models;
    using Camera.Data.Models.Enums;
    using Camera.Services;
    using Camera.Services.Models;
    using Camera.Web.Infrastructure.Filters;
    using Camera.Web.Models.Cameras;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class CamerasController: Controller
    {
        private readonly ICameraService cameras;
        private readonly UserManager<User> userManager;

        public CamerasController(ICameraService cameras, UserManager<User> userManager)
        {
            this.cameras = cameras;
            this.userManager = userManager;
        }

        [MessureTime]
        [AllowAnonymous]
        [Route("cameras/all")]
        public IActionResult All()
        {
            var cameras = this.cameras.AllCameras();

            return View(cameras);
        }
      
        [Authorize]
        [Route("cameras/create")]
        public async Task<IActionResult> Add()
        {
            var user = await this.userManager.GetUserAsync(User);

            if (await userManager.IsInRoleAsync(user, "CanNotAddCameras"))
            {
                TempData["ErrorMessage"] = "You do not have access to add new offers";
                return RedirectToAction(nameof(All));
            }
            
            return this.View();
        }

        [Authorize]
        [HttpPost]
        [Route("cameras/create")]
        public IActionResult Add(AddCameraViewModel cameraViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(cameraViewModel);
            }

            this.cameras.Create(
                cameraViewModel.Make,
                cameraViewModel.Model,
                cameraViewModel.Price,
                cameraViewModel.Quantity,
                cameraViewModel.MinShutterSpeed,
                cameraViewModel.MaxShutterSpeed,
                cameraViewModel.MinISO,
                cameraViewModel.MaxISO,
                cameraViewModel.IsFullFrame,
                cameraViewModel.VideoResolution,
                cameraViewModel.LightMeterings,
                cameraViewModel.Description,
                cameraViewModel.ImageUrl,
                userManager.GetUserId(User)
                );

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        [MessureTime]
        [Route("cameras/{id}")]
        public IActionResult Details(int id)
        {
            var camera = this.cameras.Details(id);
            return View(camera);
        }

        [Authorize]
        [Route("cameras/edit/{id}")]
        public IActionResult Edit(int id)
        {
            var user = this.cameras.Edit(id);

            var userId = user.UserId;
            var currUserId = this.userManager.GetUserId(User);

            if (userId != currUserId)
            {
                this.TempData["ErrorMessage"] = "unauthorized access";
                return RedirectToAction(nameof(All));

                //return Unauthorized();
            }

            var camera = this.cameras.Edit(id);

            return View(new EditCameraModel
            {
               Id = camera.Id,
               Make = camera.Make,
               CameraModel = camera.CameraModel,
               Description = camera.Description,
               ImageUrl = camera.ImageUrl,
               InStock = camera.InStock,
               IsFullFrame = camera.IsFullFrame,
               LightMeterings = camera.LightMeterings,
                MaxISO = camera.MaxISO,
               MaxShutterSpeed = camera.MaxShutterSpeed,
               MinISO = camera.MinISO,
               MinShutterSpeed = camera.MinShutterSpeed,
               Price = camera.Price,
               VideoResolution = camera.VideoResolution
            });
        }

        [Authorize]
        [HttpPost]
        [Route("cameras/edit/{id}")]
        public IActionResult Edit(string id, EditCameraModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            cameras.SaveEdit(
                model.Id,
                model.Make,
                model.CameraModel,
                model.Price,
                model.MinShutterSpeed,
                model.MaxShutterSpeed,
                model.MinISO,
                model.MaxISO,
                model.IsFullFrame,
                model.VideoResolution,
                model.LightMeterings,
                model.Description,
                model.ImageUrl
               );

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        [Route("cameras/delete/{id}")]
        public IActionResult Delete(int id)
        {
            var user = this.cameras.Edit(id);
            var userId = user.UserId;

            var currUserId = this.userManager.GetUserId(User);

            if (userId != currUserId)
            {
                this.TempData["ErrorMessage"] = "unauthorized access";
                return RedirectToAction(nameof(All));

                //return Unauthorized();
            }

            return View(id);
        }

        [Authorize]
        [Route("cameras/destroy/{id}")]
        public IActionResult Destroy(int id)
        {
            this.cameras.Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}
