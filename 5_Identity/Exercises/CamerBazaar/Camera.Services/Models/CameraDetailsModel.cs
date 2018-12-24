
namespace Camera.Services.Models
{
    using Camera.Data.Models.Enums;
    using System.ComponentModel.DataAnnotations;

    public class CameraDetailsModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(2000), MinLength(10)]
        public string ImageUrl { get; set; }

        public CameraMakeType Make { get; set; }

        [Required]
        [MaxLength(100)]
        public string ModelCamera { get; set; }

        public decimal Price { get; set; }

        public InStock InStock { get; set; }

        public string Username { get; set; }

        public string IsFullFrame { get; set; }

        [Range(1, 30)]
        public int MinShutterSpeed { get; set; }

        [Range(2000, 8000)]
        public int MaxShutterSpeed { get; set; }

        public MinISO MinISO { get; set; }

        [Range(200, 409600)]
        public int MaxISO { get; set; }

        [Required]
        [StringLength(15)]
        public string VideoResolution { get; set; }

        public LightMetering LightMeterings { get; set; }

        [Required]
        [StringLength(6000)]
        public string Description { get; set; }

        public string UserId { get; set; }
    }
}
