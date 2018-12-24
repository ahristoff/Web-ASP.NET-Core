
namespace CarDealer.Web.Models.Suppliers
{
    using CarDealer.Services.Models;
    using System.Collections.Generic;

    public class SuppliersModel
    {
        public string Type { get; set; } //local or not

        public IEnumerable<SupplierModel> Suppliers { get; set; }
    }
}
