
namespace RazorPages.Services.Persons
{
    using RazorPages.Models.Persons;
    using System.Collections.Generic;

    public interface IPersonService
    {
        IEnumerable<PersonDetailModel> GetAllUsers();

        PersonDetailModel Details(string id);

        void CreatePerson(string firstName, string lastName, string email, string username, int age);

        PersonDetailModel Edit(string id);

        void SaveEdit(string id, string firstName, string lastName, string email, string username, int age);

        void Delete(string id);
    }
}
