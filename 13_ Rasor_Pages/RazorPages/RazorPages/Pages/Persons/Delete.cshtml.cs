
namespace RazorPages.Pages.Persons
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using RazorPages.Services.Persons;

    public class DeleteModel : PageModel
    {
        private readonly IPersonService personService;

        public DeleteModel(IPersonService personService)
        {
            this.personService = personService;
        }

        public IActionResult OnGet(string id)
        {
            personService.Delete(id);

            return RedirectToPage("Index");
        }
    }
}