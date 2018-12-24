
namespace BookShop.Services.Servises
{
    using AutoMapper.QueryableExtensions;
    using BookShop.Data;
    using BookShop.Data.Models;
    using BookShop.Services.Interfaces;
    using BookShop.Services.Models.Books;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class BookService : IBookService
    {
        private readonly BookShopDbContext db;

        public BookService(BookShopDbContext db)
        {
            this.db = db;
        }

        //4
        public async Task<BookDetailsServiceModel> Details(int bookId)                               
        {
            return await this.db.Books
                .Where(b => b.Id == bookId)
                .ProjectTo<BookDetailsServiceModel>()
                .FirstOrDefaultAsync();
        }

        //5
        public async Task<IEnumerable<BookListingServiceModel>> AllBookSearchedByWord(string saerchWord) 
        {
            return await this.db.Books
                .Where(b => b.Title.ToLower().Contains(saerchWord.ToLower()))
                .OrderBy(b => b.Title)
                .Take(10)
                .ProjectTo<BookListingServiceModel>()
                .ToListAsync();
        }

        //6
        public async Task<Book> FindBook(int bookId)
        {
                var book = await this.db.Books
                .Where(b => b.Id == bookId)
                .FirstOrDefaultAsync();

            return book;
        }

        //6
        public async Task SaveChangedBook(int bookId, string title,
            string description,
            decimal price,
            int copies,
            int? edition,
            int? ageRestriction,
            DateTime releaseDate,
            int authorId)
        {
            var book = await FindBook(bookId);

            book.Title = title;
            book.Description = description;
            book.Price = price;
            book.Copies = copies;
            book.Edition = edition;
            book.AgeRestriction = ageRestriction;
            book.ReleaseDate = releaseDate;
            book.AuthorId = authorId;
            
            await this.db.SaveChangesAsync();
            
        }

        //7
        public async Task DeleteBook(int bookId)
        {
            var book = await FindBook(bookId);

            this.db.Books.Remove(book);
            await this.db.SaveChangesAsync();
        }

        //8
        public async Task<int> Create(string title, string description, decimal price, int copies, int? edition, int? ageRestriction, DateTime releaseDate, int authorId, string categories)
        {
            var categoryNames = categories.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var categoryIds = await this.db.Categories
                .Where(c => categoryNames.Contains(c.Name))
                .Select(c => c.Id)
                .ToListAsync();

            var book = new Book
            {
                Title = title,
                Description = description,
                Price = price,
                Copies = copies,
                Edition = edition,
                AgeRestriction = ageRestriction,
                ReleaseDate = releaseDate,
                AuthorId = authorId
            };

            foreach (var categoryId in categoryIds)
            {
                book.Categories.Add(new BookCategory { CategoryId = categoryId, BookId = book.Id });
            }

            this.db.Books.Add(book);
            await this.db.SaveChangesAsync();

            return book.Id;
        }
    }
}
