
namespace CarDealer.Web.Controllers
{
    using Services;
    using Services.Models;
    using Microsoft.AspNetCore.Mvc;
    using CarDealer.Web.Models.Customers;
    using CarDealer.Services.Models.Customers;

    public class CustomersController: Controller
    {
        private readonly ICustomerService customers;

        public CustomersController(ICustomerService customers)
        {
            this.customers = customers;
        }

        [Route("customers/all/{order}")]
        public IActionResult All(string order)
        {
            var orderType = order == "ascending"
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
            var price = customer.IsYoungDriver ? customer.TotalMoneySpent * 0.95m :customer.TotalMoneySpent;

            return View(new CustomerTotalSalesModel
            {
                Name = customer.Name,
                TotalBougthCars = customer.TotalBougthCars,     
                TotalMoneySpent = price
            });
        }
    }
}
