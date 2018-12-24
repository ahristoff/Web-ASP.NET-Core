
namespace CarDealer.Web.Models.Suppliers
{
    using CarDealer.Services.Models.Suppliers;
    using System.Collections.Generic;

    //pagenation
    public class SuppliersPaginationModel
    {
        public int Id { get; set; }

        public IEnumerable<AllSuppliersModel> Suppliers { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.TotalPages ? this.TotalPages : this.CurrentPage + 1;
    }
}
