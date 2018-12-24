
namespace LearningSystem.Test
{
    using AutoMapper;
    using LearningSystem.Data;
    using LearningSystem.Data.Models;
    using LearningSystem.Services.Home.Interfaces;
    using LearningSystem.Web.Infrastructure.Mapping;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using System;

    public class Tests
    {
        private static bool isMapperInitialize = false;

        public static void Initialize()
        {
            if (!isMapperInitialize)
            {
                Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());

                isMapperInitialize = true;
            }
        }

        public static LearningSystemDbContext PrepareDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<LearningSystemDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;
            var db = new LearningSystemDbContext(dbOptions);

            return db;
        }

        //---------------------UsersController--------------------------------------

        public static Mock<UserManager<User>> GetUserManager()
        {
            var userManager = new Mock<UserManager<User>>(
               Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);

            return userManager;
        }

        public static Mock<IUserService> GetUserService()
        {
            return new Mock<IUserService>();
        }
    }
}
