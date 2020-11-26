using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.Web.Controllers;
using InternetAuction.Web.ViewModels;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace InternetAuction.Tests.Web.Tests.ControllersTests
{
    [TestFixture]
    public class AccountControllerTests
    {
        private IQueryable<UserModel> GetTestUsersModels()
        {
            return new List<UserModel>
            {
                new UserModel { Id = "1", Email = "1" },
                new UserModel { Id = "2", Email = "2" },
                new UserModel { Id = "3", Email = "3" },
            }.AsQueryable();
        }

        [Test]
        public void AccountController_Login_ReturnsProperView()
        {
            var mockUserService = new Mock<IUserService>();
            var accountController = new AccountController(mockUserService.Object);

            var result = accountController.Login();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(ViewResult), result);
        }

        [Test]
        public void AccountController_Register_ReturnsProperView()
        {
            var mockUserService = new Mock<IUserService>();
            var accountController = new AccountController(mockUserService.Object);

            var result = accountController.Register();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(ViewResult), result);
        }

        [Test]
        public void AccountController_Users_ReturnsViewWithUserViewModel()
        {
            var mockUserService = new Mock<IUserService>();
            mockUserService
                .Setup(m => m.SearchUsers(It.IsAny<UserSearchModel>()))
                .Returns(GetTestUsersModels());
            var accountController = new AccountController(mockUserService.Object);

            var result = (ViewResult)(accountController.Users(null, null));

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(UserViewModel), result.Model);
            Assert.AreEqual(GetTestUsersModels().Count(), ((UserViewModel)result.Model).Users.Count());
        }

        [Test]
        public void AccountController_Users_WithSearchModelReturnsViewWithProperUserViewModel()
        {
            var mockUserService = new Mock<IUserService>();
            mockUserService
                .Setup(m => m.SearchUsers(It.IsAny<UserSearchModel>()))
                .Returns(GetTestUsersModels().Where(u => u.Email == "1"));
            var accountController = new AccountController(mockUserService.Object);
            var searchModel = new UserSearchModel { Email = "1" };
            var result = (ViewResult)(accountController.Users(searchModel, null));

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(UserViewModel), result.Model);
            Assert.AreEqual(GetTestUsersModels().Where(u => u.Email == "1").Count(), ((UserViewModel)result.Model).Users.Count());
        }
    }
}
