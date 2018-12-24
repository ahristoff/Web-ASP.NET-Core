
namespace Camera.Services
{
    using Camera.Services.Models;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    public interface IUsersService
    {
        CameraUserDetails UserDetails(string id);

        EditUserProfileModel Edit(string id);

        //Task SaveEdit(string id, string password, string email, string phone, string currentPassword);

        void SaveEdit(string id, string email, string phone);
    }
}
