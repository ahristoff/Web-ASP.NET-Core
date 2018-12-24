
namespace BookShop.Services.Interfaces
{
    using BookShop.Data.Models;
    using BookShop.Services.Models.Books;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBookService
    {
        Task<BookDetailsServiceModel> Details(int bookId);                                        //4

        Task<IEnumerable<BookListingServiceModel>> AllBookSearchedByWord(string saerchWord);      //5

        Task<int> Create(
            string title,
            string description,
            decimal price,
            int copies,
            int? edition,
            int? ageRestriction,
            DateTime releaseDate,
            int authorId,
            string categories);

        Task<Book> FindBook(int bookId);                                           //6

        Task SaveChangedBook(int bookId, string title,
            string description,
            decimal price,
            int copies,
            int? edition,
            int? ageRestriction,
            DateTime releaseDate,
            int authorId);                                                          //6

        Task DeleteBook(int bookId);                                                //7
    } 
}
