
namespace CarDealer.Services
{
    using CarDealer.Data.Models;
    using Models.Parts;
    using System.Collections.Generic;

    public interface IPartService
    {
        IEnumerable<PartListingModel> AllListings(int page = 1, int pageSize = 10);

        IEnumerable<PartBasicModel> All();

        int Total();

        void Create(string name, decimal price, int supplierId, int quantity = 1);

        void Delete(int id);

        void SaveEdit(int id, decimal price, int quantity);

        EditPartModel Edit(int id);

        bool Exists(int id);
    }
}
