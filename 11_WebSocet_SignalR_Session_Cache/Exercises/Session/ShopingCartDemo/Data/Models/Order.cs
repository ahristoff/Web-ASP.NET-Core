
namespace ShopingCartDemo.Data.Models
{
    using ShopingCartDemo.Models;
    using System.Collections.Generic;

    public class Order
    {
        public int Id { get; set; }
        
        public decimal TotalPrice { get; set; }


        public string UserId { get; set; }

        public ApplicationUser User { get; set; }


        public List<OrderProductItem> Items { get; set; } = new List<OrderProductItem>();
    }
}
