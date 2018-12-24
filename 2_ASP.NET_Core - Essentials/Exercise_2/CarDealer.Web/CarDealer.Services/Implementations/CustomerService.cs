
namespace CarDealer.Services.Implementations
{
    using Data;
    using System.Collections.Generic;
    using Models;
    using System.Linq;
    using System;
    using CarDealer.Services.Models.Customers;
    using CarDealer.Services.Models.Cars;
    using CarDealer.Data.Models;

    public class CustomerService : ICustomerService
    {
        private readonly CarDealerDbContext db;

        public CustomerService(CarDealerDbContext db)
        {
            this.db = db;
        }
       
        public IEnumerable<CustomerModel> OrderedCustomers(OrderType order)
        {
            var customersQuery = this.db.Customers.AsQueryable();

            switch (order)
            {
                case OrderType.Ascending:
                    customersQuery = customersQuery.OrderBy(c => c.BirthDay).ThenBy(c => c.Name);
                    break;
                case OrderType.Descending:
                    customersQuery = customersQuery.OrderByDescending(c => c.BirthDay).ThenBy(c => c.Name);
                    break;
                default:
                    throw new InvalidOperationException("Invalid Order");           
            }

            return customersQuery
                .Select(c => new CustomerModel
                {
                    Id = c.Id,
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

        public void Create(string name, DateTime birthday, bool isYoungDriver)
        {
            var customer = new Customer
            {
                Name = name,
                BirthDay = birthday,
                IsYoungDriver = isYoungDriver
            };

            db.Customers.Add(customer);
            db.SaveChanges();
        }

        public CustomerModel Edit(int id)
        {
            return this.db
               .Customers
               .Where(c => c.Id == id)
               .Select(c => new CustomerModel
               {
                   Id = id,
                   Name = c.Name,
                   IsYoungDriver = c.IsYoungDriver,
                   BirthDay = c.BirthDay
               })
               .FirstOrDefault();
        }

        public void SaveEdit(int id, string name, DateTime birthDay, bool isYoungDriver)
        {
            var existingCustomer = db.Customers.Find(id);

            if (existingCustomer == null)
            {
                return;
            }

            existingCustomer.Name = name;
            existingCustomer.BirthDay = birthDay;
            //existingCustomer.IsYoungDriver = isYoungDriver;

            db.SaveChanges();
        }

        public bool Exists(int id)
        {
            return db.Customers.Any(c => c.Id == id);
        }
    }
}
