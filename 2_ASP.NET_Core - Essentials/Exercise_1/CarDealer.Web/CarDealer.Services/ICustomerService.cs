
namespace CarDealer.Services
{
    using CarDealer.Services.Models.Customers;
    using Models;
    using System.Collections.Generic;

    public interface ICustomerService
    {
        IEnumerable<CustomerModel> OrderedCustomers(OrderType order);

        CustomerTotalSalesModel TotalSalesById(int Id);
    }
}
