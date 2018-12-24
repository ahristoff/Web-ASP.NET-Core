
namespace CarDealer.Web.Models.Sales
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreateSaleModel
    {
        [Required]
        public int CustomerId { get; set; }
        
        public IEnumerable<SelectListItem> Customers { get; set; }

        [Required]
        public int CarId { get; set; }

        public IEnumerable<SelectListItem> Cars { get; set; }

        [Required]
        public double Discount { get; set; }
    }
}
