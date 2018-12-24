
namespace CarDealer.Web.Controllers
{
    using Services;
    using Services.Models;
    using Microsoft.AspNetCore.Mvc;
    using CarDealer.Web.Models.Customers;
    using CarDealer.Services.Models.Customers;

    public class CustomersController : Controller
    {
        private readonly ICustomerService customers;

        public CustomersController(ICustomerService customers)
        {
            this.customers = customers;
        }

        [Route("customers/all/{order}")]
        public IActionResult All(string order)
        {
            var orderType = order.ToLower() == "ascending"
                ? OrderType.Ascending
                : OrderType.Descending;

            var customers = this.customers.OrderedCustomers(orderType);

            return View(new AllCustomersModel
            {
                Customers = customers,
                OrderType = orderType
            });
        }

        [Route("customers/{id}")]
        public IActionResult DetailsCustomer(int id)
        {
            var customer = this.customers.TotalSalesById(id);
            var price = customer.IsYoungDriver ? customer.TotalMoneySpent * 0.95m : customer.TotalMoneySpent;

            return View(new CustomerTotalSalesModel
            {
                Name = customer.Name,
                TotalBougthCars = customer.TotalBougthCars,
                TotalMoneySpent = price
            });
        }

        [Route("customers/create")]   //[Route(nameof(Create))]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("customers/create")]   //[Route(nameof(Create))]
        public IActionResult Create(CreateCustomerModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            customers.Create(model.Name, model.Birthday, model.IsYoungDriver);

            return RedirectToAction(nameof(All), new { order = OrderType.Ascending });
        }

        [Route("customers/edit/{id}")]    //[Route(nameof(Edit) + "/{id}")]
        public IActionResult Edit(int id)
        {
            var customer = customers.Edit(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(new EditCustomerModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Birthday = customer.BirthDay,
                //IsYoungDriver = customer.IsYoungDriver
            });
        }

        [Route("customers/edit/{id}")]    //[Route(nameof(Edit) + "/{id}")]
        [HttpPost]
        public IActionResult Edit(int id, EditCustomerModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool customerExists = this.customers.Exists(id);

            if (!customerExists)
            {
                return NotFound();
            }

            customers.SaveEdit(model.Id, model.Name, model.Birthday, model.IsYoungDriver);

            return RedirectToAction(nameof(All), new { order = OrderType.Ascending });
        }
    }
}
