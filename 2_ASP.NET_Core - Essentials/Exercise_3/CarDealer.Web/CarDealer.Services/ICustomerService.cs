
namespace CarDealer.Services
{
    using CarDealer.Services.Models.Customers;
    using Models;
    using System;
    using System.Collections.Generic;

    public interface ICustomerService
    {
        IEnumerable<CustomerModel> OrderedCustomers(OrderType order);

        CustomerTotalSalesModel TotalSalesById(int Id);

        IEnumerable<CustomerModel> AllCustomer();

        void Create(string name, DateTime birthday, bool isYoungDriver);

        CustomerModel Edit(int id);

        void SaveEdit(int id, string name, DateTime birthDay, bool isYoungDriver);

        bool Exists(int id);
    }
}
