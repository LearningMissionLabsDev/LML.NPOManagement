using Amazon.S3;
using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Common;
using LML.NPOManagement.Controllers;
using LML.NPOManagement.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace TestProject
{
    [TestClass]
    public class TestMethods
    {
        [TestMethod]
        public async Task Registration_ValidRequest_ReturnsOkResult()
        {
            //// Arrange
            //var mapperMock = new Mock<IMapper>();
            //var configurationMock = new Mock<IConfiguration>();
            //var amazonMock = new Mock<IAmazonS3>();
            //var userServiceMock = new Mock<IUserService>();
            //var notificationMock = new Mock<INotificationService>();

            //var controller = new RegisterController(
            //registrationServiceMock.Object,
            //userServiceMock.Object,
            //notificationMock.Object,
            //amazonMock.Object,
            //configurationMock.Object
            //);

            //var userRequest = new UserRequest
            //{
            //    Email = "using@mail.ru",
            //    Password = "Password12>",
            //    ConfirmPassword = "Password12>",
            //};

            //var userModel = new UserModel(); // Set up your expected user model here

            //mapperMock.Setup(m => m.Map<UserRequest, UserModel>(userRequest)).Returns(userModel);
            //var expectedUser = new UserModel
            //{
            //    Email = "using@mail.ru",
            //    Password = "Password12>",
            //};

            //// Example of setting up the IRegistrationService mock with specific IConfiguration instance
            //registrationServiceMock
            //    .Setup(s => s.Registration(It.IsAny<UserModel>(), It.IsAny<IConfiguration>()))
            //.ReturnsAsync(expectedUser);

            //// Act
            //var result = await controller.Registration(userRequest);
            //var result1 = result.Result as ObjectResult;
            //if (result1 != null)
            //{
            //    Assert.AreEqual(200, result1.StatusCode);
            //}
            //else
            //{
            //    Assert.Fail("Method Failed");
            //}
        }
    }
}
