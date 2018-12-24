
namespace ShopingCartDemo.Services.Interfaces
{
    using ShopingCartDemo.Services.Models;
    using System.Collections.Generic;

    public interface IShoppingCartManager
    {
        void AddToCart(string shoppingCartId, int productId);

        void RemoveFromCart(string shoppingCartId, int productId);

        IEnumerable<CartItem> GetCartItems(string shoppingCartId);

        void RemoveQuantity(string shoppingCartId, int productId);

        void ClearShoppingCart(string shoppingCartId);
    }
}
