
namespace Camera.Services.Models
{
    using Camera.Data.Models.Enums;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class EditCameraModel
    {
        public int Id { get; set; }

        
        [StringLength(2000), MinLength(10)]
        public string ImageUrl { get; set; }

        public CameraMakeType Make { get; set; }

        
        [MaxLength(100)]
        public string CameraModel { get; set; }

        public decimal Price { get; set; }

        public InStock InStock { get; set; }

        public bool IsFullFrame { get; set; }

        [Range(1, 30)]
        public int MinShutterSpeed { get; set; }

        [Range(2000, 8000)]
        public int MaxShutterSpeed { get; set; }

        public MinISO MinISO { get; set; }

        [Range(200, 409600)]
        public int MaxISO { get; set; }

        
        [StringLength(15)]
        public string VideoResolution { get; set; }

        public LightMetering LightMeterings { get; set; }
        
        [StringLength(6000)]
        public string Description { get; set; }

        public string UserId { get; set; }
    }
}
