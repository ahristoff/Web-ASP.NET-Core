
namespace CarDealer.Services.Models.Suppliers
{
    using CarDealer.Services.Models.Parts;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class EditSupplierModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public bool IsImporter { get; set; }

        public IEnumerable<PartModel> Parts { get; set; }
    }
}
