
namespace BookShop.Api.Models.Authors
{
    using BookShop.Data;
    using System.ComponentModel.DataAnnotations;

    public class AuthorRequestModel
    {
        [Required]
        [MaxLength(DataConstants.AuthorNameMaxLenght)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(DataConstants.AuthorNameMaxLenght)]
        public string LastName { get; set; }
    }
}
