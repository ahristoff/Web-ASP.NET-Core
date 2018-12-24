
namespace CarDealer.Web.Models.Suppliers
{
    using CarDealer.Services.Models.Parts;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class EditSuppliersModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public bool IsImporter { get; set; }

        public IEnumerable<int> PartIds { get; set; }

        public IEnumerable<SelectListItem> AllParts { get; set; }
    }
}
