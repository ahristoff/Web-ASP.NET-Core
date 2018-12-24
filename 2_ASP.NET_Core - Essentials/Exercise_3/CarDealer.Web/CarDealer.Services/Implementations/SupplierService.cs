
namespace CarDealer.Services.Implementations
{
    using CarDealer.Data;
    using CarDealer.Data.Models;
    using CarDealer.Services.Models.Parts;
    using CarDealer.Services.Models.Suppliers;
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class SupplierService : ISupplierService
    {
        private readonly CarDealerDbContext db;

        public SupplierService(CarDealerDbContext db)
        {
            this.db = db;
        }
       
        public IEnumerable<SupplierListingModel> AllListing(bool isImporter)
        {
            return this.db
                        .Suppliers
                        .OrderByDescending(s => s.Id)
                        .Where(s => s.IsImporter == isImporter)
                        .Select(s => new SupplierListingModel
                        {
                            Id = s.Id,
                            Name = s.Name,
                            TotalParts = s.Parts.Count
                        })
                        .ToList();
        }

        public IEnumerable<SupplierModel> All()  // for addPart
        {
            return this.db.Suppliers
                .OrderBy(s => s.Name)
                .Select(s => new SupplierModel
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .ToList();
        }

        //pagination
        public IEnumerable<AllSuppliersModel> AllSuppliersWithParts(int page = 1, int pageSize = 5)
        {
            return this.db.Suppliers
                .OrderBy(s => s.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new AllSuppliersModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    IsImporter = s.IsImporter == true ? "Yes" : "No",
                    Parts = s.Parts.Select(p => new PartModel
                    {
                        Name = p.Name,
                        Price = p.Price
                    })
                })
                .ToList();
        }

        //pagination
        public int Total()
        {
            return this.db.Suppliers.Count();
        }

        public void CreateSupplier(string supplierName, bool isImported, IEnumerable<int> partIds)
        {
            var supplier = new Supplier
            {
                Name = supplierName,
                IsImporter = isImported,
            };

            foreach (var partId in partIds)
            {
                supplier.Parts.Add(new Part
                {
                    Name = db.Parts.Where(p => p.Id == partId).Select(p => p.Name).FirstOrDefault(),
                    Price = db.Parts.Where(p => p.Id == partId).Select(p => p.Price).FirstOrDefault()
                });
            }

            this.db.Suppliers.Add(supplier);
            this.db.SaveChanges();
        }

        public EditSupplierModel Edit(int id)
        {
            return this.db.Suppliers
                .Where(s => s.Id == id)
                .Select(s => new EditSupplierModel
                {
                    Name = s.Name,
                    IsImporter = s.IsImporter,
                    Parts = s.Parts.Where(sp => sp.SupplierId == id).Select(p => new PartModel
                    {
                        Name = p.Name,
                        Price = p.Price
                    })
                    .ToList()
                })
                .FirstOrDefault();
        }

        public void SaveEdit(int id, string supplierName, bool isImporter, IEnumerable<int> partIds)
        {
            var supplier = this.db.Suppliers.Find(id);
            var supplierParts = this.db.Parts.Where(p => p.SupplierId == id).ToList();

            supplier.Id = id;
            supplier.Name = supplierName;
            supplier.IsImporter = isImporter;

            if (partIds != null)
            {
                supplier.Parts.Clear();
                foreach (var partId in partIds)
                {
                    supplier.Parts.Add(new Part
                    {
                        Name = db.Parts.Where(p => p.Id == partId).Select(p => p.Name).FirstOrDefault(),
                        Price = db.Parts.Where(p => p.Id == partId).Select(p => p.Price).FirstOrDefault()
                    });
                }
            }

            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var supplier = this.db.Suppliers.Find(id);

            if (supplier == null)
            {
                return;
            }

            this.db.Suppliers.Remove(supplier);
            this.db.SaveChanges();
        }
    }
}
