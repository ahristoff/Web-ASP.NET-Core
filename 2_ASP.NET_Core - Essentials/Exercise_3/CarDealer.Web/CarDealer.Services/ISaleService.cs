
namespace CarDealer.Services
{
    using CarDealer.Services.Models.Sales;
    using System.Collections.Generic;

    public interface ISaleService
    {
        IEnumerable<SaleListModel> All();

        SaleDetailsModel Details(int id);

        IEnumerable<SaleListModel> DiscountedSales();

        IEnumerable<SaleListModel> DiscountedSalesByPersent(double persent);

        void Create(int customerId, int carId, double discount);
    }
}
