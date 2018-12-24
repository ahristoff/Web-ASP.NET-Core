
namespace CarDealer.Services.Implementations
{
    using CarDealer.Data;
    using CarDealer.Services.Models;
    using CarDealer.Services.Models.Cars;
    using System.Collections.Generic;
    using System.Linq;

    public class CarService : ICarService
    {
        private readonly CarDealerDbContext db;

        public CarService(CarDealerDbContext db)
        {
            this.db = db;
        }


        public IEnumerable<CarModel> ByMake(string make)
        {
            return this.db
                 .Cars
                 .Where(c => c.Make.ToLower() == make.ToLower())
                 .OrderBy(c => c.Model)
                 .ThenBy(c => c.TravelledDistance)
                 .Select(c => new CarModel
                 {
                     Make = c.Make,
                     Model = c.Model,
                     TraveledDistance = c.TravelledDistance
                 })
                 .ToList();
        }

        public IEnumerable<CarWithPartsModel> WithParts()
        {
            return this.db
                .Cars
                .Select(c => new CarWithPartsModel
                {
                    Make = c.Make,
                    Model = c.Model,
                    TraveledDistance = c.TravelledDistance,
                    Parts = c.Parts.Select(p => new PartModel
                    {
                        Price = p.Part.Price,
                        Name = p.Part.Name
                    })
                })
                .OrderBy(c => c.Make)
                .ToList();
        }
    }
}
