
namespace LearningSystem.Services.Home.Interfaces
{
    using LearningSystem.Services.Home.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<UserProfileServiceModel> ProfileAsync(string username);

        //----------------------------------------------------------------

        Task<IEnumerable<UserListingServiceModel>> FindUsersAsync(string searchtext);

        //----------------------------------------------------------------

        Task<byte[]> GetPdfCertificate(int courseId, string studentId);
    }
}
