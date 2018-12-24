
namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using CarDealer.Services.Models;
    using CarDealer.Web.Models.Sales;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Newtonsoft.Json;
    using System.Linq;

    public class SalesController: Controller
    {
        private readonly ISaleService sales;
        private readonly ICarService cars;
        private readonly ICustomerService customers;

        public SalesController(ISaleService sales, ICarService cars, ICustomerService customers)
        {
            this.sales = sales;
            this.cars = cars;
            this.customers = customers;
        }

        [Route("sales")]
        public IActionResult All()
        {
            var sale = this.sales.All().OrderBy(s => s.CustomerName).ThenBy(s => s.Discount);

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

        [Route("/sales/create")]
        public IActionResult Create()
        {          
            return View(new CreateSaleModel
            {
                Customers = this.customers.AllCustomer().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                Cars = this.cars.AllCars().Select(c => new SelectListItem
                {
                    Text = c.Make + " " + c.Model,
                    Value = c.Id.ToString()
                })
            });
        }

        [HttpPost]
        [Route("/sales/create")]
        public IActionResult ReviewSale(CreateSaleModel createSaleModel, ReviewSaleModel reviewSaleModel)
        {
            if (!ModelState.IsValid)
            {
                return View(new CreateSaleModel
                {
                    Customers = this.customers.AllCustomer()
                    .Select(c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    }),
                    Cars = this.cars.AllCars()
                    .Select(c => new SelectListItem
                    {
                        Text = c.Make + " " + c.Model,
                        Value = c.Id.ToString()
                    })
                });
            }

            TempData["data"] = JsonConvert.SerializeObject(createSaleModel);

            var customerId = createSaleModel.CustomerId;
            var customerName = customers.AllCustomer()
                .Where(c => c.Id == customerId)
                .Select(c => c.Name)
                .FirstOrDefault();

            var carId = createSaleModel.CarId;
            var carWithMakeAndModel = cars.AllCars()
                .Where(c => c.Id == carId)
                .Select(c => c.Make + " " + c.Model)
                .FirstOrDefault();

            var isYongDriver = customers.AllCustomer()
                 .Where(c => c.Id == customerId)
                 .Select(c => c.IsYoungDriver)
                 .FirstOrDefault();

            var discount = createSaleModel.Discount + (isYongDriver ? 5 : 0);

            var carPrice = this.cars.AllCars()
                .Where(c => c.Id == carId)
                .Select(c => c.Price)
                .FirstOrDefault();
           
            var endPrice = (carPrice *(100 -  (decimal)discount))/100;

            return View(new ReviewSaleModel
            {
                CustomerName = customerName,
                CarMakeAndModel = carWithMakeAndModel,
                Discounts = (double)discount,
                CarPrice = carPrice,
                CarPriceWithDiscount = endPrice
            });
        }

        [Route("/sales/creates")]
        public IActionResult Creates()
        {
            var saleModel = JsonConvert.DeserializeObject<CreateSaleModel>(TempData["data"].ToString());

            if (!ModelState.IsValid)
            {
                return View(new CreateSaleModel
                {
                    Customers = this.customers.AllCustomer()
                    .Select(c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    }),
                    Cars = this.cars.AllCars()
                    .Select(c => new SelectListItem
                    {
                        Text = c.Make + " " + c.Model,
                        Value = c.Id.ToString()
                    })
                });
            }

            sales.Create(saleModel.CustomerId, saleModel.CarId, saleModel.Discount);

            return RedirectToAction(nameof(All));
        }
    }
}
