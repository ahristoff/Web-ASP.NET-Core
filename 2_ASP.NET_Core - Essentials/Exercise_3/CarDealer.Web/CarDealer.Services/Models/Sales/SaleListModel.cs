using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.Services.Models.Sales
{
    public class SaleListModel: SaleModel
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public bool IsYoungDriver { get; set; }

        public decimal DiscounterPrice => 
            this.Price * (1 - ((decimal)this.Discount * 0.01m + (this.IsYoungDriver ? 0.05m : 0)));     
    }
}
