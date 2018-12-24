
namespace ShopingCartDemo.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ShopingCartDemo.Data;
    using ShopingCartDemo.Models;
    using System.Diagnostics;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly ShopingCartDbContext db;

        public HomeController(ShopingCartDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            //Seed Products -only the first way

            //var db = (ShopingCartDbContext)this.HttpContext.RequestServices.GetService(typeof(ShopingCartDbContext));

            //for (int i = 0; i < 20; i++)
            //{
            //    db.Products.Add(new Product
            //    {
            //        Title = "Product" + i.ToString(),
            //        Price = i * 100
            //    });
            //}

            //db.SaveChanges();


            var products = this.db.Products.OrderBy(p => p.Price).ToList();
            
            return View(products);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
