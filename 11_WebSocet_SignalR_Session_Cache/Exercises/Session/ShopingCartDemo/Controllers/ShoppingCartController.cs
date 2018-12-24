
namespace ShopingCartDemo.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ShopingCartDemo.Data;
    using ShopingCartDemo.Data.Models;
    using ShopingCartDemo.Infrastructure.Extensions;
    using ShopingCartDemo.Models;
    using ShopingCartDemo.Services.Interfaces;
    using System.Linq;

    public class ShoppingCartController: Controller
    {
        private readonly IShoppingCartManager shoppingCartManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ShopingCartDbContext db;

        public ShoppingCartController(IShoppingCartManager shoppingCartManager, UserManager<ApplicationUser> userManager, ShopingCartDbContext db)
        {
            this.db = db;
            this.shoppingCartManager = shoppingCartManager;
            this.userManager = userManager;
        }

        public IActionResult AddToCart(int productId)
        {
            //var shoppingCartId = this.HttpContext.Session.GetString("Shopping_Cart_Id");

            //if (shoppingCartId == null)   //The user is still not using this shopping cart
            //{
            //    shoppingCartId = Guid.NewGuid().ToString();  //generate new shoppingCartId

            //    this.HttpContext.Session.SetString("Shopping_Cart_Id", shoppingCartId);
            //}

            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();  //SessionExtensions


            this.shoppingCartManager.AddToCart(shoppingCartId, productId);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult RemoveProduct(int productId)
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();  //SessionExtensions

            this.shoppingCartManager.RemoveFromCart(shoppingCartId, productId);

            return RedirectToAction(nameof(ItemsInCart));
        }

        public IActionResult RemoveQuantity(int productId)
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();  //SessionExtensions

            this.shoppingCartManager.RemoveQuantity(shoppingCartId, productId);

            return RedirectToAction(nameof(ItemsInCart));
        }

        public IActionResult ItemsInCart()
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();  //SessionExtensions

            var shoppingCartItems = this.shoppingCartManager.GetCartItems(shoppingCartId);

            var shoppingCartItemsIds = shoppingCartItems.Select(si => si.ProductId);

            var itemQuantity = shoppingCartItems.ToDictionary(i => i.ProductId, i => i.Quantity);

            var itemsWithDetails = this.db.Products
                .Where(p => shoppingCartItemsIds.Contains(p.Id))
                .Select(pr => new CartItemViewModel
                {
                    ProductId = pr.Id,
                    Name = pr.Title,
                    Price = pr.Price,
                    Quantity = itemQuantity[pr.Id]
                    //Quantity = shoppingCartItems.Where(c => c.ProductId == pr.Id).FirstOrDefault().Quantity
                })
                .ToList();
            
            return View(itemsWithDetails);
        }

        [Authorize]
        public IActionResult FinishOrder()
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();  //SessionExtensions

            var shoppingCartItems = this.shoppingCartManager.GetCartItems(shoppingCartId);

            var shoppingCartItemsIds = shoppingCartItems.Select(si => si.ProductId);

            var itemQuantity = shoppingCartItems.ToDictionary(i => i.ProductId, i => i.Quantity);

            var itemsWithDetails = this.db.Products
                .Where(p => shoppingCartItemsIds.Contains(p.Id))
                .Select(pr => new CartItemViewModel
                {
                    ProductId = pr.Id,
                    Name = pr.Title,
                    Price = pr.Price,
                    Quantity = itemQuantity[pr.Id]
                })
                .ToList();

            var order = new Order
            {
                UserId = userManager.GetUserId(User),
                TotalPrice = itemsWithDetails.Sum(i => i.Price * i.Quantity)
            };

            foreach (var item in itemsWithDetails)
            {
                order.Items.Add(new OrderProductItem
                {
                    ProductId = item.ProductId,
                    ProductPrice = item.Price,
                    Quantity = item.Quantity
                });
            }

            if (order.TotalPrice == 0)
            {
                this.TempData["ErrorMessage"] = "unsuccessful request, you have not selected a product";
                return RedirectToAction(nameof(ItemsInCart));
            }

            this.db.Orders.Add(order);
            this.db.SaveChanges();

            shoppingCartManager.ClearShoppingCart(shoppingCartId);

            this.TempData["SuccessMessage"] = "successfully completed your request";

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
