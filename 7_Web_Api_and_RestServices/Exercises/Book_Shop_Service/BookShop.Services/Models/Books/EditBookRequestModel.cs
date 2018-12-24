
namespace BookShop.Services.Models.Books
{
    using BookShop.Common.Mapping;
    using BookShop.Data;
    using BookShop.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class EditBookRequestModel: IMapFrom<Book>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(DataConstants.BookTitleMinLenght)]
        [MaxLength(DataConstants.BookTitleMaxLenght)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Copies { get; set; }

        public int? Edition { get; set; }

        public int? AgeRestriction { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int AuthorId { get; set; }
    }
}
