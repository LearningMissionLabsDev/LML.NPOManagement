using System;
using System.Reflection;
using LML.ApiSpecGenerator;
using LML.NPOManagement.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject
{
    [TestClass]
    public class ControllersTest
    {
        [TestMethod]
        public void Controller_AuthorizationRoles_Test()
        {
            // Arange
            var utils = new AuthorizationHeaderRolesTestUtils();

            // Act
            var result = utils.IsProvided<AccountController>() &&
                         utils.IsProvided<UserController>();

            // Assert
            Assert.IsTrue(result, "Tests Cannot Continue. Missing API Specification. Some methods or general specialized files required for the tests are missing.");

            // Arrange
            utils = new();

            // Act
            result = utils.TestControllerAuthorizationRoles<UserController>();

            // Assert
            Assert.IsTrue(result, "Test Authorization Failed for UserController");


            // Arrange
            utils = new();

            // Act
            result = utils.TestControllerAuthorizationRoles<AccountController>();

            // Assert
            Assert.IsTrue(result, "Test Authorization Failed for AccountController");
        }
    }
}

