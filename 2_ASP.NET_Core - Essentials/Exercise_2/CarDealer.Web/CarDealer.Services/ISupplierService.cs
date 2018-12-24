
namespace CarDealer.Services
{
    using CarDealer.Services.Models;
    using CarDealer.Services.Models.Suppliers;
    using System.Collections.Generic;

    public interface ISupplierService
    {
        IEnumerable<SupplierListingModel> AllListing(bool IsImporter);

        IEnumerable<SupplierModel> All();

    }
}
