
namespace RazorPages.Pages.Persons
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using RazorPages.Models.Persons;
    using RazorPages.Services.Persons;

    public class EditModel : PageModel
    {
        private readonly IPersonService personService;

        public EditModel(IPersonService personService)
        {
            this.personService = personService;
        }

        [BindProperty(SupportsGet = true)]
        public PersonDetailModel Person { get; set; }

        public IActionResult OnGet(string id)
        {
            Person = personService.Edit(id);

            return Page();
        }

        public IActionResult OnPost(string id)
        {
            Person.Id = id;

            personService.SaveEdit(Person.Id, Person.Firstname, Person.Lastname, Person.Email, Person.Username, Person.Age);

            return RedirectToPage("Index");
        }
    }
}