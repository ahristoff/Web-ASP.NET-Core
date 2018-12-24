
namespace CarDealer.Web.Models.Suppliers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreateSupplierModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public bool IsImporter { get; set; }

        public IEnumerable<int> PartIds { get; set; }

        public IEnumerable<SelectListItem> AllParts { get; set; }
    }
}
