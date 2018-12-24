
namespace BookShop.Api.Controllers
{
    using BookShop.Api.Infrastructure.Filters;
    using BookShop.Services.Interfaces;
    using BookShop.Services.Models.Categories;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    public class CategoriesController: Controller
    {
        private readonly ICategoryService categories;

        public CategoriesController(ICategoryService categories)
        {
            this.categories = categories;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()                                   //9
        {
            var categories = await this.categories.AllCategories();

            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryById(int categoryId)                      //10
        {
            var category = await this.categories.CategoryById(categoryId);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPut("{categoryId}")]
        [ValidateModelState]
        public async Task<IActionResult> EditCategory(int categoryId, [FromBody]CategoriesDetailsServiceModel model)                                                 //11
        {
            var category = await this.categories.CategoryByIdFromDb(categoryId);

            if (category == null)
            {
                return NotFound();
            }

            await categories.SaveChangedCategory(categoryId, model.Name);

            return Ok();
        }

        [HttpDelete("{categoryId}")]
        [ValidateModelState]
        public async Task<IActionResult> Delete(int categoryId)                               //12
        {
            var category = await this.categories.CategoryByIdFromDb(categoryId);

            if (category == null)
            {
                return NotFound();
            }

            await categories.DeleteCategory(categoryId);

            return Ok();
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody]CategoriesDetailsServiceModel model)    //13
        {
            var categoryId = await this.categories.CreateCategory(model.Name);

            if (categoryId == 0)
            {
                return BadRequest("This category already exists");
            }

            return Ok(categoryId);
        }
    }
}
