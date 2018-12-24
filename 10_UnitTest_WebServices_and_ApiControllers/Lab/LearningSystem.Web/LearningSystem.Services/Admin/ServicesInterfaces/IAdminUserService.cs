
namespace LearningSystem.Services.ServicesInterfaces
{
    using LearningSystem.Services.Admin.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminUserService
    {
        Task<IEnumerable<AdminUserListingServiceModel>> Allasync();
    }
}
