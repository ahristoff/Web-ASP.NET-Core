
namespace BookShop.Services.Servises
{
    using AutoMapper.QueryableExtensions;
    using BookShop.Data;
    using BookShop.Data.Models;
    using BookShop.Services.Interfaces;
    using BookShop.Services.Models.Authors;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AuthorService : IAuthorService
    {
        private readonly BookShopDbContext db;

        public AuthorService(BookShopDbContext db)
        {
            this.db = db;
        }
        
        public async Task<AuthorDetailsServiceModel> Details(int id)                //1
        {
            return await this.db.Authors
                .Where(a => a.Id == id)
                .ProjectTo<AuthorDetailsServiceModel>()
                .FirstOrDefaultAsync();
        }

        public async Task<int> Create(string firstName, string lastName)            //2    
        {
            var author = new Author
            {
                FirstName = firstName,
                LastName = lastName
            };

            await this.db.Authors.AddAsync(author);
            await this.db.SaveChangesAsync();

            return author.Id;
        }

        public async Task<IEnumerable<BooksByAuthorServiceModel>> Books(int authorId)     //3
        {
            return await this.db.Books
                .Where(b => b.AuthorId == authorId)
                .ProjectTo<BooksByAuthorServiceModel>()
                .ToListAsync();
        }
        
        public async Task<bool> Exists(int authorId)                                       //8
        {
            return await this.db.Authors.AnyAsync(a => a.Id == authorId);
        }
    }
}
