
namespace RazorPages.Services.Persons
{
    using RazorPages.Data;
    using RazorPages.Data.Models;
    using RazorPages.Models.Persons;
    using System.Collections.Generic;
    using System.Linq;

    public class PersonService : IPersonService
    {
        private readonly ApplicationDbContext db;

        public PersonService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<PersonDetailModel> GetAllUsers()
        {
            var users = db.Users
                .OrderBy(u => u.Firstname)
                .Select(u => new PersonDetailModel
                {
                    Id = u.Id,
                    Firstname = u.Firstname,
                    Lastname = u.Lastname,
                    Email = u.Email,
                    Username = u.UserName,
                    Age = u.Age
                })
                .ToList();

            return users;
        }

        public PersonDetailModel Details(string id)
        {
            return this.db.Users
                .Where(u => u.Id == id)
                .Select(u => new PersonDetailModel
                {
                    Id = id,
                    Firstname = u.Firstname,
                    Lastname = u.Lastname,
                    Email = u.Email,
                    Username = u.UserName,
                    Age = u.Age
                })
                .FirstOrDefault();
        }

        public void CreatePerson(string firstName, string lastName, string email, string username, int age)
        {
            var person = new ApplicationUser
            {
                Firstname = firstName,
                Lastname = lastName,
                Email = email,
                UserName = username,
                Age = age
            };

            db.Add(person);
            db.SaveChanges();
        }

        public PersonDetailModel Edit(string id)
        {
            return this.db.Users
                .Where(u => u.Id == id)
                .Select(u => new PersonDetailModel
                {
                    Id = u.Id,
                    Firstname = u.Firstname,
                    Lastname = u.Lastname,
                    Email = u.Email,
                    Username = u.UserName,
                    Age = u.Age
                })
                .FirstOrDefault();
        }

        public void SaveEdit(string id, string firstName, string lastName, string email, string username, int age)
        {
            var person = this.db.Users.Find(id);

            person.Id = id;
            person.Firstname = firstName;
            person.Lastname = lastName;
            person.Email = email;
            person.UserName = username;
            person.Age = age;

            this.db.SaveChanges();
        }

        public void Delete(string id)
        {
            var person = this.db.Users.Find(id);

            this.db.Users.Remove(person);
            this.db.SaveChanges();
        }
    }
}
