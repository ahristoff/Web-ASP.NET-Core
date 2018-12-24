
namespace RazorPages.Pages.Persons
{
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using RazorPages.Models.Persons;
    using RazorPages.Services.Persons;
    using System.Collections.Generic;

    public class IndexModel : PageModel
    {
        private readonly IPersonService personService;

        public IndexModel(IPersonService personService)
        {
            this.personService = personService;
        }

        public IEnumerable<PersonDetailModel> Persons { get; set; }
       
        public void OnGet()
        {
            this.Persons = personService.GetAllUsers();
        }
        
    }
}