using Amazon.S3;
using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Services;
using LML.NPOManagement.Common;
using LML.NPOManagement.Common.Model;
using LML.NPOManagement.Controllers;
using LML.NPOManagement.Dal.Models;
using LML.NPOManagement.Dal.Repositories.Interfaces;
using LML.NPOManagement.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace TestProject
{
    [TestClass]
    public class TestMethods
    {
        [TestMethod]
        public async Task MockTest()
        {
            var mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<Account, AccountModel>();
            });

            var mapper = mapperConfig.CreateMapper();
            var accountId = 1;

            var expectedAccount = new Account { Id = accountId, /* Other properties */ };
            var expectedAccountModel = mapper.Map<AccountModel>(expectedAccount);

            // Mock dependencies
            var accountRepositoryMock = new Mock<IAccountRepository>();
            accountRepositoryMock.Setup(repo => repo.GetAccountById(accountId))
                .ReturnsAsync(expectedAccountModel);

            var userRepositoryMock = new Mock<IUserRepository>();
            // Mock any necessary methods or properties of userRepositoryMock if needed

            var configurationMock = new Mock<IConfiguration>();
            // Mock any necessary methods or properties of configurationMock if needed

            var accountService = new AccountService(accountRepositoryMock.Object, userRepositoryMock.Object, configurationMock.Object);

            // Act
            var result = await accountService.GetAccountById(accountId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(accountId, result.Id);
        }
    }
}
