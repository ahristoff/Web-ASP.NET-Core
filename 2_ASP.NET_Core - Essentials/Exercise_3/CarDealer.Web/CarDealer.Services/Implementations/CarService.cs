
namespace CarDealer.Services.Implementations
{
    using CarDealer.Data;
    using CarDealer.Services.Models.Parts;
    using CarDealer.Services.Models.Cars;
    using System.Collections.Generic;
    using System.Linq;
    using CarDealer.Data.Models;

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
                     Id = c.Id,
                     Make = c.Make,
                     Model = c.Model,
                     TraveledDistance = c.TravelledDistance
                 })
                 .ToList();
        }

        public IEnumerable<CarWithPartsModel> WithParts()
        {
            return this.db.Cars
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

        public IEnumerable<CarModelWithPrice> AllCars()
        {
            return db.Cars
                .Select(c => new CarModelWithPrice
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    TraveledDistance = c.TravelledDistance,
                    Price = c.Parts.Sum(p => p.Part.Price),
                })
                .ToList();
        }

        public void Create(string make, string model, long traveledDistance, IEnumerable<int> partIds)
        {
            var car = new Car
            {
                Make = make,
                Model = model,
                TravelledDistance = traveledDistance,                
            };

            foreach (var partId in partIds)
            {
                car.Parts.Add(new PartCar { PartId = partId});
            }

            this.db.Cars.Add(car);
            this.db.SaveChanges();
        }
    }
}
