
namespace RazorPages.Pages.Persons
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using RazorPages.Models.Persons;
    using RazorPages.Services.Persons;

    public class CreateModel : PageModel
    {
        private readonly IPersonService personService;

        public CreateModel(IPersonService personService)
        {
            this.personService = personService;
        }

        [BindProperty(SupportsGet = true)]
        public PersonDetailModel Person { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            personService.CreatePerson(Person.Firstname, Person.Lastname, Person.Email, Person.Username, Person.Age);

            return new RedirectToPageResult("Index");
        }
    }
}