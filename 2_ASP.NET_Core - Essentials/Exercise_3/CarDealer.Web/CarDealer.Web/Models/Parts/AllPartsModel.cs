
namespace CarDealer.Web.Models.Parts
{
    using CarDealer.Services.Models.Parts;
    using System.Collections.Generic;

    public class AllPartsModel  // without Pagination
    {
        public IEnumerable<PartListingModel> Parts { get; set; }
    }
}
