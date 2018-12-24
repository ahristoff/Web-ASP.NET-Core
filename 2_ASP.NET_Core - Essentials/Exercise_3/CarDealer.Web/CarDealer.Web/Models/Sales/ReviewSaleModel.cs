
namespace CarDealer.Web.Models.Sales
{
    public class ReviewSaleModel
    {
        public string CustomerName { get; set; }

        public string CarMakeAndModel { get; set; }

        public double Discounts { get; set; }

        public decimal CarPrice { get; set; }

        public decimal CarPriceWithDiscount { get; set; }
    }
}
