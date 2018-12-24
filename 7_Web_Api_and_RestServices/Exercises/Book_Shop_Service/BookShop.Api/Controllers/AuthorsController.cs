
namespace BookShop.Api.Controllers
{
    using BookShop.Api.Infrastructure.Filters;
    using BookShop.Api.Models.Authors;
    using BookShop.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    public class AuthorsController: Controller
    {
        private readonly IAuthorService authors;

        public AuthorsController(IAuthorService authors)
        {
            this.authors = authors;
        }

        [HttpGet("{id}")]                                                           //1
        public async Task<IActionResult> Get(int id)
        {
            var author = await authors.Details(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody]AuthorRequestModel model)     //2
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            var authorId = await this.authors.Create(model.FirstName, model.LastName);

            return Ok(authorId);
        }

        [HttpGet("{authorId}" + "/books")]
        public async Task<IActionResult> GetBooks(int authorId)                       //3
        {
            var books = await this.authors.Books(authorId);

            return Ok(books);
        }
    }
}
