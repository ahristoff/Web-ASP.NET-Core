
namespace ShopingCartDemo.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Http;
    using System;

    public static class SessionExtensions
    {
        public static string GetShoppingCartId(this ISession session)
        {
            var shoppingCartId = session.GetString("Shopping_Cart_Id");

            if (shoppingCartId == null)   //The user is still not using this shopping cart
            {
                shoppingCartId = Guid.NewGuid().ToString();  //generate new shoppingCartId

                session.SetString("Shopping_Cart_Id", shoppingCartId);
            }

            return shoppingCartId;
        }
    }
}
