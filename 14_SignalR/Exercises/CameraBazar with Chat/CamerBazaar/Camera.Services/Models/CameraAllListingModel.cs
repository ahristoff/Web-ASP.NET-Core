
namespace Camera.Services.Models
{
    using Camera.Data.Models.Enums;
    using System.ComponentModel.DataAnnotations;

    public class CameraAllListingModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(2000), MinLength(10)]
        public string ImageUrl { get; set; }

        public CameraMakeType Make { get; set; }

        [Required]
        [MaxLength(100)]
        public string Model { get; set; }

        public decimal Price { get; set; }

        public string Details { get; set; }

        public InStock InStock { get; set; }
    }
}
