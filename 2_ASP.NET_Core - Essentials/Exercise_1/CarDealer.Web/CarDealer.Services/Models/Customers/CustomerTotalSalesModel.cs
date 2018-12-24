
namespace CarDealer.Services.Models.Customers
{
    using CarDealer.Services.Models.Cars;
    using System.Collections.Generic;
    using System.Linq;

    public class CustomerTotalSalesModel
    {
        public string Name { get; set; }

        public bool IsYoungDriver { get; set; }       

        //public IEnumerable<CarPriceModel> BougthCars { get; set; }

        public int TotalBougthCars { get; set; }

        public decimal TotalMoneySpent { get; set; }

        public double Discount { get; set; }

    }
}
