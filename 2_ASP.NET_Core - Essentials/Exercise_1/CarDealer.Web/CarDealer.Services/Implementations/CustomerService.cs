
namespace CarDealer.Services.Implementations
{
    using Data;
    using System.Collections.Generic;
    using Models;
    using System.Linq;
    using System;
    using CarDealer.Services.Models.Customers;
    using CarDealer.Services.Models.Cars;

    public class CustomerService : ICustomerService
    {
        private readonly CarDealerDbContext db;

        public CustomerService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CustomerModel> OrderedCustomers(OrderType order)
        {
            var customersQuery = this.db.Customers.ToList();

            switch (order)
            {
                case OrderType.Ascending:
                    customersQuery = customersQuery.OrderBy(c => c.BirthDay).ThenBy(c => c.Name).ToList();
                    break;
                case OrderType.Descending:
                    customersQuery = customersQuery.OrderByDescending(c => c.BirthDay).ThenBy(c => c.Name).ToList();
                    break;
                default:
                    throw new InvalidOperationException("Invalid Order");           
            }

            return customersQuery
                .Select(c => new CustomerModel
                {
                    Name = c.Name,
                    BirthDay = c.BirthDay,
                    IsYoungDriver = c.IsYoungDriver
                })
                .ToList();
        }

        public CustomerTotalSalesModel TotalSalesById(int id)
        {
            
            return this.db
                .Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerTotalSalesModel
                {
                    Name = c.Name,
                    IsYoungDriver = c.IsYoungDriver,
                    TotalBougthCars = c.Sales.Count,
                    TotalMoneySpent = c.Sales.Sum(p => p.Car.Parts
                    .Sum(pr => pr.Part.Price))
                })
                .FirstOrDefault();
        }
    }
}
