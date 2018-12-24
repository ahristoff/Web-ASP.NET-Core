
namespace LearningSystem.Test.Web.Controllers
{
    using FluentAssertions;
    using LearningSystem.Data.Models;
    using LearningSystem.Services.Home.Interfaces;
    using LearningSystem.Services.Home.Models;
    using LearningSystem.Web.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Xunit;

    public class UserControllerTest
    {
        private Mock<UserManager<User>> userManager;
        private Mock<IUserService> userService;

        public UserControllerTest()
        {
            userManager = Tests.GetUserManager();
            userService = Tests.GetUserService();
        }

        //---------------tests in UsersController -> 3 tests---------------------------
        [Fact]
        public void DownloadSertificateShouldBeOnlyForAuthorizeUsers()
        {
            //Arrange
            var method = typeof(UsersController).GetMethod(nameof(UsersController.DownloadSertificate));

            //Act
            var authorizeAttribute = method.GetCustomAttributes();

            //Assert
            authorizeAttribute.Should()
                .Match(attr => attr.Any(a => a.GetType() == typeof(AuthorizeAttribute)));
        }

        [Fact]
        public async Task ProfileShouldReturnNotFoundWithInvalidUsername()
        {
            //Arrenge
            //var userManager = new Mock<UserManager<User>>(
            //    Mock.Of<IUserStore <User>>(), null, null, null, null, null, null, null, null);

            var controller = new UsersController(null, this.userManager.Object);

            //Act
            var result = await controller.Profile("Username");

            //Assert
            result.Should()
                .BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task ProfileShouldReturnViewWithValidModelWithValidUsername()
        {
            //Arrange
            this.userManager
                .Setup(u => u.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new User { Id = "someId"});

            this.userService
                .Setup(u => u.ProfileAsync(It.Is<string>(s => s == "someId")))
                .ReturnsAsync(new UserProfileServiceModel { UserName = "Username"});

            var controller = new UsersController(userService.Object, userManager.Object);

            //Act
            var result = await controller.Profile("something");

            //Assert

            result.Should()
                .BeOfType<ViewResult>()
                .Subject     //this ViewResult with Model matched "UserProfileServiceModel"
                .Model.Should()  
                .Match(m => m.As<UserProfileServiceModel>().UserName == "Username");
        }
    }
}
