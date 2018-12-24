
namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using Microsoft.AspNetCore.Mvc;

    public class SalesController: Controller
    {
        private readonly ISaleService sales;

        public SalesController(ISaleService sales)
        {
            this.sales = sales;
        }

        [Route("sales")]
        public IActionResult All()
        {
            var sale = this.sales.All();

            return View(sale);
        }


        [Route("sales/{id}")]
        public IActionResult Details(int id)
        {
            return View(this.sales.Details(id));
        }

        [Route("sales/discounted")]
        public IActionResult DiscountedSales()
        {
            return View(this.sales.DiscountedSales());
        }

        [Route("sales/discounted/{persent}")]
        public IActionResult DiscountedSalesByPersent(double persent)
        {
            return View(this.sales.DiscountedSalesByPersent(persent));
        }
    }
}
