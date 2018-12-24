
namespace CarDealer.Services.Models.Suppliers
{
    using CarDealer.Services.Models.Parts;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    //pagination
    public class AllSuppliersModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string IsImporter { get; set; }

        public IEnumerable<PartModel> Parts { get; set; }
    }
}
