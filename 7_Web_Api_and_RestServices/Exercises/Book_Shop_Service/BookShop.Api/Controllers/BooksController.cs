
namespace BookShop.Api.Controllers
{
    using BookShop.Api.Infrastructure.Filters;
    using BookShop.Api.Models.Books;
    using BookShop.Services.Interfaces;
    using BookShop.Services.Models.Books;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    public class BooksController: Controller
    {
        private readonly IBookService books;
        private readonly IAuthorService authors;

        public BooksController(IBookService books, IAuthorService authors)
        {
            this.books = books;
            this.authors = authors;
        }

        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetBook(int bookId)                                    //4
        {
            var book = await this.books.Details(bookId);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpGet]
        public async Task<IActionResult> GetBooksByWord([FromQuery]string search = "")           //5
        {
            var books = await this.books.AllBookSearchedByWord(search);

            return Ok(books);
        }

        [HttpPut("{bookId}")]
        [ValidateModelState]
        public async Task<IActionResult> Put(int bookId, [FromBody]EditBookRequestModel model)    //6
        {
            var book = await this.books.FindBook(bookId);

            if (book == null)
            {
                return NotFound();
            }
            
            await books.SaveChangedBook(bookId, model.Title, model.Description, model.Price, model.Copies, model.Edition, model.AgeRestriction, model.ReleaseDate, model.AuthorId);

            return Ok();
        }

        [HttpDelete("{bookId}")]
        [ValidateModelState]
        public async Task<IActionResult> Delete(int bookId)
        {
            var book = await this.books.FindBook(bookId);

            if (book == null)
            {
                return NotFound();
            }

            await books.DeleteBook(bookId);

            return Ok();
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody]CreateBookRequestModel model)            //8
        {
            if (!await this.authors.Exists(model.AuthorId))
            {
                return BadRequest("Author does not exist!");
            }

            var bookId = await this.books.Create(
                model.Title,
                model.Description,
                model.Price,
                model.Copies,
                model.Edition,
                model.AgeRestriction,
                model.ReleaseDate,
                model.AuthorId,
                model.Categories);

            return Ok(bookId);
        }
    }
}
