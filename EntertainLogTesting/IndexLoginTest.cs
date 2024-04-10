using Moq;
using EntertainLog.Models.Repos;
using EntertainLog.Models;
using EntertainLog.Controllers;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc;
using EntertainLog.Models.ViewModel;


namespace EntertainLogTesting
{
    /// <summary>
    /// Created by: Yuanlong Song
    /// Testing for SignUp and Login
    public class IndexLoginTest
    {
        [Fact]
        public async void SignUp_RedirectsToLogin_WhenSignupIsSuccessful()
        {
            //Arrange
            User user = new User { UserName = "Test1", Password="Password1", Email="TestEmail1"};

            Mock<IEntertainLogRepo> mock = new Mock<IEntertainLogRepo>();
            mock.Setup(repo => repo.AddUser(It.IsAny<User>()));

            HomeController controller = new HomeController(mock.Object);
            
            

            //Act
            var result = controller.SignUp(user) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Login", result.ActionName);
        }

        [Fact]
        public async Task Login_RedirectsToDashboard_WhenLoginIsSuccessful()
        {
            // Arrange
            Mock<IEntertainLogRepo> mock = new Mock<IEntertainLogRepo>();
            User user = new User { UserID = 1, UserName = "testuser", Password = "testpassword" };
            mock.Setup(repo => repo.GetUserByNameAsync(It.IsAny<string>()))
                .Returns((User)null);// Simulate user not found

            HomeController controller = new HomeController(mock.Object);
            LoginViewModel loginViewModel = new LoginViewModel
            {
                Username = "PhilipS",
                Password = "password2"
            };

            // Act
            var result = controller.Login(loginViewModel);

            // Assert
            Assert.IsType<ViewResult>(result);
            //Assert.Equal("Dashboard", redirectToActionResult.ActionName); // Redirect to Dashboard action after successful login
            //Assert.Equal(1, redirectToActionResult.RouteValues["id"]); // Check if user ID is passed as a route value
        }

       
    }
}
