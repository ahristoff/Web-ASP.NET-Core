
namespace RazorPages.Pages.Persons
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using RazorPages.Models.Persons;
    using RazorPages.Services.Persons;

    public class DetailsModel : PageModel
    {
        private readonly IPersonService personService;

        public DetailsModel(IPersonService personService)
        {
            this.personService = personService;
        }

        public PersonDetailModel Persons { get; set; }

        //1var
        //public void OnGet(string id)
        //{
        //    this.Persons = this.personService.Details(id);
        //}

        //2 var
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        public void OnGet(string id)
        {
            this.Persons = this.personService.Details(this.Id);
        }
    }
}