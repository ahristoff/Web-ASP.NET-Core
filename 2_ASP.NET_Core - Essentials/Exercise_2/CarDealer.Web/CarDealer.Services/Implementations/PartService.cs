
namespace CarDealer.Services.Implementations
{
    using CarDealer.Data;
    using CarDealer.Data.Models;
    using CarDealer.Services.Models.Parts;
    using System.Collections.Generic;
    using System.Linq;

    public class PartService : IPartService
    {
        private readonly CarDealerDbContext db;

        public PartService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<PartListingModel> AllListings(int page = 1, int pageSize = 10)
        {
            return this.db.Parts
                .OrderByDescending(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new PartListingModel
                {
                    Id =p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    Suppliername = p.Supplier.Name
                })
                .ToList();
        }

        public int Total()
        {
            return this.db.Parts.Count();
        }

        public void Create(string name, decimal price, int supplierId, int quantity = 1)
        {
            var part = new Part
            {
                Name = name,
                Price = price,
                SupplierId = supplierId,
                Quantity = quantity
            };

            this.db.Parts.Add(part);
            this.db.SaveChanges();
        }

        public void Delete(int id)
        {
            var part = this.db.Parts.Find(id);

            if (part == null)
            {
                return;
            }

            this.db.Parts.Remove(part);
            this.db.SaveChanges();
        }

        public EditPartModel Edit(int id)
        {
            return this.db.Parts
                .Where(p => p.Id == id)
                .Select(p => new EditPartModel
                {
                    Id = p.Id,
                    Price = p.Price,
                    Quantity = p.Quantity
                })
                .FirstOrDefault();
        }

        public void SaveEdit(int id, decimal price, int quantity)
        {
            var part = this.db.Parts.Find(id);

            part.Id = id;
            part.Price = price;
            part.Quantity = quantity;

            db.SaveChanges();
        }

        public bool Exists(int id)
        {
            return db.Parts.Any(c => c.Id == id);
        }

        public IEnumerable<PartBasicModel> All() //AddCar whit Parts
        {
            return this.db.Parts
                .OrderBy(p => p.Name)
                .Select(p => new PartBasicModel
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToList();
        }
    }
}
