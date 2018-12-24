
namespace Camera.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Data;
    using Data.Models;
    using Camera.Data.Models.Enums;
    using System.Linq;
    using Camera.Services.Models;

    public class CameraService : ICameraService
    {
        private readonly CameraDbContext db;

        public CameraService(CameraDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CameraAllListingModel> AllCameras()
        {
            return this.db.Cameras
                .OrderBy(c => c.Make)
                .Select(c => new CameraAllListingModel
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    Price = c.Price,
                    Details = "Details",
                    ImageUrl = c.ImageUrl,
                    InStock = c.Quantity == 0 ? InStock.OutOfStock : InStock.InStock
                });
        }

        public void Create(
            CameraMakeType make, 
            string model, 
            decimal price, 
            int quantity, 
            int minShutterSpeed, 
            int maxShutterSpeed, 
            MinISO minISO, 
            int maxISO, 
            bool isFullFrame,
            string videoResolutuon,
            IEnumerable<LightMetering> lightMeterings,
            string description, 
            string imageUrl, 
            string userId)
        {

            if (lightMeterings == null)
            {
                lightMeterings = new List<LightMetering>();
            }

            var camera = new Camera
            {
                Make = make,
                Model = model,
                Price = price,
                Quantity = quantity,
                MinShutterSpeed = minShutterSpeed,
                MaxShutterSpeed = maxShutterSpeed,
                MinISO = minISO,
                MaxISO = maxISO,  
                IsFullFrame = isFullFrame,
                VideoResolution = videoResolutuon,
                LightMetering = (LightMetering)lightMeterings.Cast<int>().Sum(),
                Description = description,
                ImageUrl = imageUrl,
                UserId = userId
            };

            this.db.Cameras.Add(camera);
            this.db.SaveChanges();
        }

        public CameraDetailsModel Details(int id)
        {
            return this.db.Cameras
                .Where(s => s.Id == id)
                .Select(c => new CameraDetailsModel
                {
                    Id = c.Id,
                    Make = c.Make,
                    ModelCamera = c.Model,
                    Price = c.Price,
                    InStock = c.Quantity == 0 ? InStock.OutOfStock : InStock.InStock,
                    Username = c.User.UserName,
                    ImageUrl = c.ImageUrl,
                    IsFullFrame = c.IsFullFrame == true ? "Yes" : "No",
                    MinShutterSpeed = c.MinShutterSpeed,
                    MaxShutterSpeed = c.MaxShutterSpeed,
                    MinISO = c.MinISO,
                    MaxISO = c.MaxISO,
                    VideoResolution = c.VideoResolution,
                    LightMeterings = c.LightMetering,
                    Description = c.Description,
                    UserId = c.User.Id
                })
                .FirstOrDefault();
        }

        public EditCameraModel Edit(int id)
        {
            return this.db.Cameras
                .Where(c => c.Id == id)
                .Select(c => new EditCameraModel
                {
                    Id = c.Id,
                    Make = c.Make,
                    CameraModel = c.Model,
                    Price = c.Price,
                    InStock = c.Quantity == 0 ? InStock.OutOfStock : InStock.InStock,
                    ImageUrl = c.ImageUrl,
                    IsFullFrame = c.IsFullFrame,
                    MinShutterSpeed = c.MinShutterSpeed,
                    MaxShutterSpeed = c.MaxShutterSpeed,
                    MinISO = c.MinISO,
                    MaxISO = c.MaxISO,
                    VideoResolution = c.VideoResolution,
                    LightMeterings = c.LightMetering,
                    Description = c.Description,
                    UserId = c.UserId
                })
                .FirstOrDefault();
        }

        public void SaveEdit(
            int id,
            CameraMakeType make,
            string cameraModel,
            decimal price,
            int minShutterSpeed,
            int maxShutterSpeed,
            MinISO minISO,
            int maxISO,
            bool isFullFrame,
            string videoResolutuon,
            LightMetering lightMeterings,
            string description,
            string imageUrl
            )
        {
            var camera = this.db.Cameras.Find(id);

            camera.Id = id;
            camera.ImageUrl = imageUrl;
            camera.Make = make;
            camera.Model = cameraModel;
            camera.Price = price;
            camera.MinShutterSpeed = minShutterSpeed;
            camera.MaxShutterSpeed = maxShutterSpeed;
            camera.MinISO = minISO;
            camera.MaxISO = maxISO;
            camera.IsFullFrame = isFullFrame;
            camera.VideoResolution = videoResolutuon;
            camera.LightMetering = (LightMetering)lightMeterings;
            camera.Description = description;

            this.db.SaveChanges();
        }

        public void Delete(int id)
        {
            var camera = this.db.Cameras.Find(id);

            if (camera == null)
            {
                return;
            }

            this.db.Cameras.Remove(camera);
            this.db.SaveChanges();
        }
    }
}
