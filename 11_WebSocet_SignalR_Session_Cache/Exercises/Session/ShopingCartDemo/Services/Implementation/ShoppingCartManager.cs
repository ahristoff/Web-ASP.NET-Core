
namespace ShopingCartDemo.Services.Implementation
{
    using ShopingCartDemo.Services.Interfaces;
    using ShopingCartDemo.Services.Models;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    public class ShoppingCartManager : IShoppingCartManager
    {
        private readonly ConcurrentDictionary<string, ShoppingCart> carts;

        public ShoppingCartManager()
        {
            this.carts = new ConcurrentDictionary<string, ShoppingCart>();
        }

        public void AddToCart(string shoppingCartId, int productId)
        {
            var shoppingCart = this.carts.GetOrAdd(shoppingCartId, new ShoppingCart());
            
            shoppingCart.AddToCart(productId);
        }

        public IEnumerable<CartItem> GetCartItems(string shoppingCartId)
        {
            var shoppingCart = this.carts.GetOrAdd(shoppingCartId, new ShoppingCart());

            return new List<CartItem>(shoppingCart.Items);
            //return shoppingCart.Items;
        }

        public void RemoveFromCart(string shoppingCartId, int productId)
        {
            var shoppingCart = this.carts.GetOrAdd(shoppingCartId, new ShoppingCart());

            shoppingCart.RemoveFromCart(productId);
        }

        public void RemoveQuantity(string shoppingCartId, int productId)
        {
            var shoppingCart = this.carts.GetOrAdd(shoppingCartId, new ShoppingCart());

            shoppingCart.RemoveQuantity(productId);
        }
        
        public void ClearShoppingCart(string shoppingCartId)
        {
            var shoppingCart = this.carts.GetOrAdd(shoppingCartId, new ShoppingCart());

            shoppingCart.ClearShoppingCart();
        }
    }
}
