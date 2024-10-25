using MDisasaterDampener.Controllers;
using MDisasaterDampener.Models;
using MDisasaterDampener.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
#pragma warning disable IDE0008 // Use explicit type
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
namespace MDisasaterDampener.Test_
{

#pragma warning disable CA1001 // Types that own disposable fields should be disposable
    public class UserControllerTests
#pragma warning restore CA1001 // Types that own disposable fields should be disposable
    {
        private readonly Mock<IUserServices> mockUserServices;
        private readonly UserController userController;

        public UserControllerTests()
        {
            mockUserServices = new Mock<IUserServices>();
            userController = new UserController(mockUserServices.Object);
        }

        private static void SetMockHttpContext(UserController controller)
        {
            var httpContext = new DefaultHttpContext();
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };
        }

        [Fact]

        public void Login_ShouldReturnViewResult()
        {
            // Act
            var result = userController.Login();

            // Assert
            _ = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Register_ShouldReturnViewResult()
        {
            // Act
            var result = userController.Register();

            // Assert
            _ = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ProcessLogin_ValidUser_ShouldRedirectToHomeIndex()
        {
            // Arrange
            var loginViewModel = new LoginViewModel
            {
                username = "BigUnc",
                password = "Bigs=gestUnc23"
            };
            var userViewModel = new UserViewModel
            {
                username = "BigUnc"
            };

            _ = mockUserServices.Setup(s => s.Login(It.IsAny<LoginViewModel>()))
                             .Returns(userViewModel);


            var result = userController.ProcessLogin(loginViewModel) as RedirectToActionResult;


            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
            mockUserServices.Verify(s => s.Login(loginViewModel), Times.Once);
        }

        [Fact]
        public void ProcessLogin_InvalidUser_ShouldReturnLoginViewWithError()
        {

            var loginViewModel = new LoginViewModel
            {
                username = "invalidUser",
                password = "wrongpassword"
            };

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            _ = mockUserServices.Setup(s => s.Login(It.IsAny<LoginViewModel>()))
                             .Returns((UserViewModel)null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            SetMockHttpContext(userController);


            var result = userController.ProcessLogin(loginViewModel) as ViewResult;


            Assert.NotNull(result);
            Assert.Equal("Login", result.ViewName);
            Assert.True(userController.ModelState.ContainsKey(""));
            mockUserServices.Verify(s => s.Login(loginViewModel), Times.Once);
        }

        [Fact]
        public void ProcessRegistration_ShouldRedirectToLogin()
        {

            var registerViewModel = new RegisterViewModel
            {
                firstName = "John",
                lastName = "Doe",
                username = "johndoe",
                email = "johndoe@example.com",
                password = "password123"
            };


            var result = userController.ProcessRegistration(registerViewModel) as ViewResult;


            Assert.NotNull(result);
            Assert.Equal("Login", result.ViewName);
            mockUserServices.Verify(s => s.Register(registerViewModel), Times.Once);
        }

        [Fact]
        public void ProcessChangeUsername_ValidModel_ShouldRedirectToHomeIndex()
        {

            var userViewModel = new UserViewModel
            {
                username = "newUsername"
            };
            int userId = 1;


            var result = userController.ProcessChangeUsername(userViewModel, userId) as RedirectToActionResult;


            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
            mockUserServices.Verify(s => s.ChangeUsername(userViewModel, userId), Times.Once);
        }

        [Fact]
        public void ProcessChangeUsername_InvalidModel_ShouldReturnChangePasswordView()
        {

            var userViewModel = new UserViewModel
            {
                username = "invalidUsername"
            };
            int userId = 1;

            userController.ModelState.AddModelError("error", "invalid model");


            var result = userController.ProcessChangeUsername(userViewModel, userId) as ViewResult;


            Assert.NotNull(result);
            Assert.Equal("ChangePassword", result.ViewName);
        }

        [Fact]
        public void ProcessChangeEmail_ValidModel_ShouldRedirectToHomeIndex()
        {

            var userViewModel = new UserViewModel
            {
                email = "newemail@example.com"
            };
            int userId = 1;


            var result = userController.ProcessChangeEmail(userViewModel, userId) as RedirectToActionResult;


            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
            mockUserServices.Verify(s => s.ChangeEmail(userViewModel, userId), Times.Once);
        }

        [Fact]
        public void ProcessChangePassword_ValidModel_ShouldRedirectToHomeIndex()
        {

            var userViewModel = new UserViewModel
            {
                password = "newpassword123"
            };
            int userId = 1;


            var result = userController.ProcessChangePassword(userViewModel, userId) as RedirectToActionResult;


            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
            mockUserServices.Verify(s => s.ChangePassword(userViewModel, userId), Times.Once);
        }

        [Fact]
        public void AccountManagement_ShouldReturnViewResult()
        {

            var result = userController.AccountManagement();


            _ = Assert.IsType<ViewResult>(result);
        }


    }
}
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable IDE0008 // Use explicit type
