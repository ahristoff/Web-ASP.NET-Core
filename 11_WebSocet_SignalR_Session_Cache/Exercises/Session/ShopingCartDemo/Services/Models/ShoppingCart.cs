
namespace ShopingCartDemo.Services.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class ShoppingCart
    {
        private readonly IList<CartItem> cartItems;

        public ShoppingCart()
        {
            this.cartItems = new List<CartItem>();
        }
        
        public IEnumerable<CartItem> Items => new List<CartItem>(this.cartItems);

        public void AddToCart(int productId)
        {
            var cartItem = this.Items.FirstOrDefault(p => p.ProductId == productId);

            if (cartItem == null)
            {
                cartItem = new CartItem { ProductId = productId, Quantity = 1 };

                this.cartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }
        }

        public void RemoveFromCart(int productId)
        {
            var cartItem = this.cartItems.FirstOrDefault(p => p.ProductId == productId);
            if (cartItem != null)
            {
                this.cartItems.Remove(cartItem);
            }
        }

        public void RemoveQuantity(int productId)
        {
            var cartItem = this.Items.FirstOrDefault(p => p.ProductId == productId);

            if (cartItem != null)
            {
                if (cartItem.Quantity == 1)
                {
                    this.cartItems.Remove(cartItem);
                }
                cartItem.Quantity--;
            }
        }

        public void ClearShoppingCart()  // Delete all after record in db
        {
            this.cartItems.Clear();
        }
    }
}
