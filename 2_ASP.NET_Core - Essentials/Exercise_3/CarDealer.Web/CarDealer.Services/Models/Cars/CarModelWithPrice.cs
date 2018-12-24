using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.Services.Models.Cars
{
    public class CarModelWithPrice
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public decimal Price { get; set; }

        public long TraveledDistance { get; set; }
    }
}
