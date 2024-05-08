using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Services;
using LML.NPOManagement.Common;
using LML.NPOManagement.Dal.Models;
using LML.NPOManagement.Dal.Repositories;
using LML.NPOManagement.Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq.Expressions;

namespace TestProject.BLL.Test
{
    [TestClass]
    public class TestUserService
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly UserService _userService;

        public TestUserService()
        {
            _userRepository = new Mock<IUserRepository>();
            _userService = new UserService(_userRepository.Object, null);
        }

        [TestMethod]
        public async Task GetAllUsers()
        {
            _userRepository.Setup(x => x.GetAllUsers()).ReturnsAsync((List<UserModel>)null);
            var resultNullBll = await _userService.GetAllUsers();
            Assert.IsNull(resultNullBll);

            var usersExist = new List<UserModel>()
            {
                new UserModel() { Id = 1, Email = "First@example.com" },
                new UserModel() { Id = 2, Email = "Second@example.com" },
            };

            _userRepository.Setup(x => x.GetAllUsers()).ReturnsAsync(usersExist);
            var resultBll = await _userService.GetAllUsers();
            Assert.IsNotNull(resultBll);
            Assert.AreEqual(usersExist.Count, resultBll.Count);
        }

        [TestMethod]
        public async Task GetUserById_invalid()
        {
            // Define invalid and not found user IDs
            int invalidUserId = -1;
            int notFoundUserId = 2;

            // Act & Assert for invalid user ID
            var invalidUserIdResult = await _userService.GetUserById(invalidUserId);
            Assert.IsNull(invalidUserIdResult);

            // Act & Assert for user not found
            _userRepository.Setup(x => x.GetUserById(notFoundUserId)).ReturnsAsync((UserModel)null);
            var userNotFoundResult = await _userService.GetUserById(notFoundUserId);
            Assert.IsNull(userNotFoundResult);

            // Define an expected user model
            var expectedUser = new UserModel() { Id = 1, Email = "test@example.com" };

            // Act & Assert for user found
            _userRepository.Setup(x => x.GetUserById(expectedUser.Id)).ReturnsAsync(expectedUser);
            var result = await _userService.GetUserById(expectedUser.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedUser.Id, result.Id);
            Assert.AreEqual(expectedUser.Email, result.Email);
        }
    }
}
