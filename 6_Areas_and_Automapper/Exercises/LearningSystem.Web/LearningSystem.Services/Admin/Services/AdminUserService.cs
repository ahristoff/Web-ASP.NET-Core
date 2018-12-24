
namespace LearningSystem.Services.Admin.Services
{
    using AutoMapper.QueryableExtensions;
    using LearningSystem.Data;
    using LearningSystem.Services.Admin.Models;
    using LearningSystem.Services.ServicesInterfaces;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AdminUserService : IAdminUserService
    {
        public LearningSystemDbContext db;

        public AdminUserService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminUserListingServiceModel>> Allasync()
        {
            return  await this.db.Users
                .ProjectTo<AdminUserListingServiceModel>()
                .ToListAsync();
        }
    }
}
