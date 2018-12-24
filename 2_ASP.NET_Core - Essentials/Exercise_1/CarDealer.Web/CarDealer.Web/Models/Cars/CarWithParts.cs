
namespace CarDealer.Web.Models.Cars
{
    using CarDealer.Services.Models.Cars;
    using System.Collections.Generic;

    public class CarWithParts
    {
        public IEnumerable<CarWithPartsModel> CarParts { get; set; }
    }
}
