
namespace CarDealer.Services
{
    using CarDealer.Services.Models;
    using CarDealer.Services.Models.Suppliers;
    using System.Collections.Generic;

    public interface ISupplierService
    {
        IEnumerable<SupplierListingModel> AllListing(bool IsImporter);

        IEnumerable<SupplierModel> All();

        IEnumerable<AllSuppliersModel> AllSuppliersWithParts(int page = 1, int pageSize = 10);

        int Total();

        void CreateSupplier(string supplierName, bool isImported, IEnumerable<int> partIds);

        void SaveEdit(int id, string supplierName, bool isImporter, IEnumerable<int> partIds);

        EditSupplierModel Edit(int id);

        void Delete(int id);
    }
}
