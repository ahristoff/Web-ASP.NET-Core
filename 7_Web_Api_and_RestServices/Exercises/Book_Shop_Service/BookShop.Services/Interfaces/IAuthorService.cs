
namespace BookShop.Services.Interfaces
{
    using BookShop.Services.Models.Authors;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAuthorService
    {
        Task<AuthorDetailsServiceModel> Details(int id);                  //1

        Task<int> Create(string firstName, string lastName);              //2

        Task<IEnumerable<BooksByAuthorServiceModel>> Books(int authorId); //3



        Task<bool> Exists(int authorId);                                  //8
    }
}
