
namespace Camera.Services.Models
{
    using Camera.Data.Models;
    using System.Collections.Generic;

    public class CameraUserDetails
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Username { get; set; }

        public IEnumerable<CameraAllListingModel> Cameras { get; set; }

        public int CountInStockCameras { get; set; }

        public int CountOutOfStockCameras { get; set; }
    }
}
