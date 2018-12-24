
namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using CarDealer.Web.Models.Suppliers;
    using Microsoft.AspNetCore.Mvc;

    public class SuppliersController: Controller
    {
        private readonly ISupplierService suppliers;

        public SuppliersController(ISupplierService suppliers)
        {
            this.suppliers = suppliers;
        }

        public IActionResult Local()
        {
            return this.View("Suppliers", this.GetSuppliers(false));
        }

        public IActionResult Importers()
        {
            return View("Suppliers", this.GetSuppliers(true));
        }

        private SuppliersModel GetSuppliers(bool importers)
        {
            var type = importers ? "IMporters" : "Local";

            var suppliers = this.suppliers.AllListing(importers);

            return new SuppliersModel
            {
                Type = type,
                Suppliers = suppliers
            };
        }
    }
}
