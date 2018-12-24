
namespace Camera.Services.Implementations
{
    using Camera.Data;
    using Camera.Data.Models;
    using Camera.Services.Models;
    using Microsoft.AspNetCore.Identity;
    using System.Linq;
    using System.Threading.Tasks;

    public class UsersService: IUsersService
    {
        private readonly CameraDbContext db;
        private readonly UserManager<User> userManager;

        public UsersService(CameraDbContext db, UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public CameraUserDetails UserDetails(string id)
        {
            return this.db.Users
                .Where(u => u.Id == id)
                .Select(u => new CameraUserDetails
                {
                    Id = u.Id,
                    Email = u.Email,
                    Phone = u.PhoneNumber,
                    Username = u.UserName,
                    CountInStockCameras = u.Cameras.Where(c => c.Quantity != 0).Count(),
                    CountOutOfStockCameras = u.Cameras.Where(c => c.Quantity == 0).Count(),
                    Cameras = u.Cameras.Select(c => new CameraAllListingModel
                    {
                        Id = c.Id,
                        Make = c.Make,
                        Model = c.Model,
                        Price = c.Price,
                        Details = "Details",
                        ImageUrl = c.ImageUrl,
                        InStock = c.Quantity == 0 ? InStock.OutOfStock : InStock.InStock
                    })                 
                })
                .FirstOrDefault();
        }

        public EditUserProfileModel Edit(string id)
        {
            return this.db.Users
                .Where(u => u.Id == id)
                .Select(u => new EditUserProfileModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    Phone = u.PhoneNumber
                })
                .FirstOrDefault();
        }

        public void SaveEdit(string id, string email, string phone)
        {           
            var user = this.db.Users.Find(id);

            user.PhoneNumber = phone;
            user.Email = email;                             
        }
    }
}
