
namespace Camera.Services
{
    using Camera.Data.Models.Enums;
    using Camera.Services.Models;
    using System.Collections.Generic;

    public interface ICameraService
    {
        void Create(
            CameraMakeType make, 
            string model,
            decimal price,
            int quantity,
            int minShutterSpeed,
            int maxShutterSpeed,
            MinISO minISO,
            int maxIso,
            bool isFullFrame,
            string videoResolutuon,
            IEnumerable<LightMetering> lightMeterings,
            string description,
            string imageUrl,
            string userId);

        IEnumerable<CameraAllListingModel> AllCameras();

        CameraDetailsModel Details(int id);

        EditCameraModel Edit(int id);

        void SaveEdit(
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
             );

        void Delete(int id);
    }
}
