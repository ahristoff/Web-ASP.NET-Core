
namespace CarDealer.Services.Models.Parts
{
    using System.ComponentModel.DataAnnotations;

    public class EditPartModel
    {
        public int Id { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
