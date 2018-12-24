
namespace CarDealer.Web.Models.Suppliers
{
    using CarDealer.Services.Models;
    using CarDealer.Services.Models.Suppliers;
    using System.Collections.Generic;

    public class SuppliersModel
    {
        public string Type { get; set; } //local or not

        public IEnumerable<SupplierListingModel> Suppliers { get; set; }
    }
}
