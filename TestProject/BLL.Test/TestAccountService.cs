using LML.NPOManagement.Bll.Services;
using LML.NPOManagement.Common.Model;
using LML.NPOManagement.Common;
using LML.NPOManagement.Dal.Repositories.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TestProject.BLL.Test
{
    public class TestAccountService
    {
        [TestMethod]
        public async Task GetAllAccounts_ReturnExpectedResults()
        {
            // Arrange
            var accountRepositoryMock = new Mock<IAccountRepository>();
            var accountService = new AccountService(accountRepositoryMock.Object, null, null);

            var accounts = new List<AccountModel> {
                new AccountModel { Id = 1, Name = "TestAcc" },
                new AccountModel { Id = 2, Name = "TestAcc" },
                new AccountModel { Id = 3, Name = "TestAcc" }
            };

            accountRepositoryMock.Setup(repo => repo.GetAllAccounts()).ReturnsAsync(accounts);

            // Act
            var retrievedAccounts = await accountService.GetAllAccounts();

            // Assert
            Assert.IsTrue(retrievedAccounts.All(acc => acc != null), "All retrieved accounts should not be null.");

            accountRepositoryMock.Setup(repo => repo.GetAllAccounts()).ReturnsAsync((List<AccountModel>)null);

            // Act
            retrievedAccounts = await accountService.GetAllAccounts();

            // Assert
            Assert.IsNull(retrievedAccounts, "Retrieved Account must be null.");
        }

        [TestMethod]
        public async Task GetAccountById_ReturnExpectedResults()
        {
            // Arrange
            var accountRepositoryMock = new Mock<IAccountRepository>();
            var accountService = new AccountService(accountRepositoryMock.Object, null, null);

            var validAccountId = 1;
            var nonExistentAccountId = 4;
            var accountModel = new AccountModel { Id = validAccountId, Name = "TestAcc" };

            accountRepositoryMock.Setup(repo => repo.GetAccountById(validAccountId)).ReturnsAsync(accountModel);
            accountRepositoryMock.Setup(repo => repo.GetAccountById(nonExistentAccountId)).ReturnsAsync((AccountModel)null);

            // Act
            var retrievedAccountForValidId = await accountService.GetAccountById(validAccountId);
            var retrievedAccountForNonExistentId = await accountService.GetAccountById(nonExistentAccountId);

            // Assert
            Assert.IsNotNull(retrievedAccountForValidId, "The retrieved account should not be null for a valid ID.");
            Assert.AreEqual(validAccountId, retrievedAccountForValidId.Id, "The ID of the retrieved account should match the requested ID for a valid ID.");
            Assert.IsNull(retrievedAccountForNonExistentId, "The account should be null for a non-existent ID.");

            await Assert.ThrowsExceptionAsync<ArgumentException>(() => accountService.GetAccountById(-1), "An ArgumentException should be thrown when the ID is negative.");
        }

        [TestMethod]
        public async Task GetAccountsByName_NullAndReturnsArgumentException()
        {
            // Arrange
            var accountRepositoryMock = new Mock<IAccountRepository>();
            var accountService = new AccountService(accountRepositoryMock.Object, null, null);

            var emptyString = string.Empty;
            var accounts = new List<AccountModel> {
                new AccountModel { Id = 1, Name = "Account" },
                new AccountModel { Id = 2, Name = "Account" },
                new AccountModel { Id = 3, Name = "Account" }
            };

            accountRepositoryMock.Setup(repo => repo.GetAccountsByName("TestName")).ReturnsAsync((List<AccountModel>)null);
            accountRepositoryMock.Setup(repo => repo.GetAccountsByName("Account")).ReturnsAsync(accounts);

            // Act
            var resultForTestName = await accountService.GetAccountsByName("TestName");
            var resultForAccount = await accountService.GetAccountsByName("Account");

            // Assert
            Assert.IsNull(resultForTestName, "Expected null for non-existing account name 'TestName'");
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => accountService.GetAccountsByName(emptyString), "Expected ArgumentException for empty account name");
            Assert.IsNotNull(resultForAccount, "Expected not null for existing account name 'Account'");
            Assert.AreEqual(3, resultForAccount.Count, "Expected 3 accounts for account name 'Account'");
        }

        [TestMethod]
        public async Task GetAccountRoleProgress_ReturnsExpectedResults()
        {
            // Arrange
            var accountRepositoryMock = new Mock<IAccountRepository>();
            var accountService = new AccountService(accountRepositoryMock.Object, null, null);

            int validAccountId = 1;
            int invalidAccountId = 10;
            int validAccountRoleId = 1;
            int invalidAccountRoleId = 10;

            var validAccountUserActivity = new AccountUserActivityModel { Account2UserModel = new Account2UserModel { AccountRoleId = validAccountRoleId } };
            var invalidAccountUserActivity = new AccountUserActivityModel { Account2UserModel = new Account2UserModel { AccountRoleId = invalidAccountRoleId } };

            var validUserProgresses = new List<AccountUserActivityModel> { validAccountUserActivity };
            var invalidUserProgresses = new List<AccountUserActivityModel> { invalidAccountUserActivity };

            accountRepositoryMock.Setup(repo => repo.GetAccountRoleProgress(validAccountId, validAccountRoleId))
                .ReturnsAsync(validUserProgresses);

            accountRepositoryMock.Setup(repo => repo.GetAccountRoleProgress(validAccountId + 1, validAccountRoleId + 1))
                 .ReturnsAsync(invalidUserProgresses);

            accountRepositoryMock.Setup(repo => repo.GetAccountRoleProgress(invalidAccountId, validAccountRoleId))
                .ReturnsAsync((List<AccountUserActivityModel>)null);

            // Act
            var resultForValidId = await accountService.GetAccountRoleProgress(validAccountId, validAccountRoleId);
            var resultForInvalidId = await accountService.GetAccountRoleProgress(invalidAccountId, validAccountRoleId);
            var resultWithoutNeededAccountId = await accountService.GetAccountRoleProgress(validAccountId + 1, validAccountRoleId + 1);

            // Assert
            Assert.IsNotNull(resultForValidId, "Expected not null for valid account ID");
            Assert.AreEqual(1, resultForValidId.Count, "Expected 1 account user activity for valid account ID");
            Assert.IsNull(resultForInvalidId, "Expected null for invalid account ID");
            Assert.IsNull(resultWithoutNeededAccountId, "Expected null for invalid account ID");
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => accountService.GetAccountRoleProgress(-1, validAccountRoleId), "Expected ArgumentException for account ID less than or equal to zero");
        }

        [TestMethod]
        public async Task GetUsersByAccount_ReturnExpectedResults()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var accountService = new AccountService(mockAccountRepository.Object, null, null);

            int validAccountId = 1;
            int nonExistingAccountId = 2;
            int userExemptAccountId = 3;
            var users = new List<UserModel> { new UserModel { Id = 1, Email = "Testemail@gmail.com" } };

            mockAccountRepository.Setup(repo => repo.GetAccountById(validAccountId)).ReturnsAsync(new AccountModel());
            mockAccountRepository.Setup(repo => repo.GetUsersByAccount(validAccountId)).ReturnsAsync(users);
            mockAccountRepository.Setup(repo => repo.GetAccountById(nonExistingAccountId)).ReturnsAsync((AccountModel)null);
            mockAccountRepository.Setup(repo => repo.GetUsersByAccount(userExemptAccountId)).ReturnsAsync((List<UserModel>)null);

            // Act
            var validAccountModel = await accountService.GetUsersByAccount(validAccountId);
            var nonExistingAccountModel = await accountService.GetUsersByAccount(nonExistingAccountId);
            var userExemptAccountModel = await accountService.GetUsersByAccount(userExemptAccountId);

            // Assert
            Assert.IsNotNull(validAccountModel, "Result should not be null.");
            Assert.IsNull(nonExistingAccountModel, "Result must be null");
            Assert.IsNull(userExemptAccountModel, "Result must be null");
            Assert.AreEqual(users.Count, validAccountModel.Count, "Result count should match the expected value.");

            int negativeAccountId = -1;
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => accountService.GetUsersByAccount(negativeAccountId), "Expected ArgumentException when accountId is less than or equal to zero.");
        }

        [TestMethod]
        public async Task AccountLogin_ReturnExpectedResults()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var accountService = new AccountService(null, userRepositoryMock.Object, null);

            var validUserId = 1;
            var invalidUserId = 2;

            var validAccount2UserModel = new Account2UserModel { UserId = validUserId, AccountId = 1 };
            var invalidAccount2UserModel = new Account2UserModel { UserId = invalidUserId, AccountId = 2 };

            var accountModel = new List<Account2UserModel> { new Account2UserModel { UserId = validUserId, AccountId = 1 } };

            userRepositoryMock.Setup(repo => repo.GetUsersInfoAccount(validUserId)).ReturnsAsync(accountModel);
            userRepositoryMock.Setup(repo => repo.GetUsersInfoAccount(invalidUserId)).ReturnsAsync((List<Account2UserModel>)null);

            // Act
            var retrievedAccount = await accountService.AccountLogin(validAccount2UserModel);
            var invalidRetrievedAccount = await accountService.AccountLogin(invalidAccount2UserModel);

            // Assert
            Assert.IsNotNull(retrievedAccount, "Retrieved Account should not be null.");
            Assert.IsNull(invalidRetrievedAccount, "Retrieved Account must be null.");

            // Test Case 2 : TODO: Make more Readable 
            var invalidAccountModel = new List<Account2UserModel> { new Account2UserModel { UserId = 10, AccountId = 3 } };
            invalidAccount2UserModel = new Account2UserModel { UserId = 10, AccountId = 2 };
            userRepositoryMock.Setup(repo => repo.GetUsersInfoAccount(10)).ReturnsAsync(invalidAccountModel);
            retrievedAccount = await accountService.AccountLogin(invalidAccount2UserModel);

            Assert.IsNull(retrievedAccount, "Retrieved Account should not be null.");
        }

        [TestMethod]
        public async Task AddAccount_ReturnExpectedResults()
        {
            // Arrange
            var accountRepositoryMock = new Mock<IAccountRepository>();
            var accountService = new AccountService(accountRepositoryMock.Object, null, null);

            var validAccountModel = new AccountModel { Name = "TestAcc", StatusId = (int)AccountStatusEnum.Active };
            var invalidAccountModel = new AccountModel { Name = "TestAcc", StatusId = (int)AccountStatusEnum.Active };
            var accountWithoutName = new AccountModel { StatusId = (int)AccountStatusEnum.Active };

            accountRepositoryMock.Setup(repo => repo.AddAccount(validAccountModel)).ReturnsAsync(validAccountModel);
            accountRepositoryMock.Setup(repo => repo.AddAccount(invalidAccountModel)).ReturnsAsync((AccountModel)null);

            // Act
            var validAddedAccount = await accountService.AddAccount(validAccountModel);
            var invalidAddedAccount = await accountService.AddAccount(invalidAccountModel);

            // Assert
            Assert.IsNotNull(validAddedAccount, "Added Account should not be null.");
            Assert.IsNull(invalidAddedAccount, "Added Account must be null.");
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => accountService.AddAccount(null), "Account model or its name cannot be null or empty.");
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => accountService.AddAccount(accountWithoutName), "Account model or its name cannot be null or empty.");
        }

        [TestMethod]
        public async Task AddUserToAccount_Test() // TODO: FIX 
        {
            // Arrange
            var accountRepositoryMock = new Mock<IAccountRepository>();
            var accountService = new AccountService(accountRepositoryMock.Object, null, null);

            var account2UserModel = new Account2UserModel { UserId = 1, AccountId = 1 };

            // Test case 1: Returns true
            accountRepositoryMock.Setup(repo => repo.GetAccountById(account2UserModel.AccountId)).ReturnsAsync(new AccountModel());
            accountRepositoryMock.Setup(repo => repo.AddUserToAccount(account2UserModel)).ReturnsAsync(true);

            // Act
            var result = await accountService.AddUserToAccount(account2UserModel);

            // Assert
            Assert.IsTrue(result, "Result should be true.");

            // Test case 2: Returns false
            accountRepositoryMock.Setup(repo => repo.GetAccountById(account2UserModel.AccountId)).ReturnsAsync((AccountModel)null);
            accountRepositoryMock.Setup(repo => repo.AddUserToAccount(account2UserModel)).ReturnsAsync(false);

            // Act
            result = await accountService.AddUserToAccount(account2UserModel);

            // Assert
            Assert.IsFalse(result, "Result should be false.");
        }

        [TestMethod]
        public async Task AddAccountUserActivityProgress_ReturnExpectedResults() // TODO: Case: GetAccount2Users returns null 
        {
            // Arrange
            var accountRepositoryMock = new Mock<IAccountRepository>();
            var accountService = new AccountService(accountRepositoryMock.Object, null, null);

            var accountUserActivityModel = new AccountUserActivityModel { Account2UserId = 1 };
            var account2Users = new List<Account2UserModel> { new Account2UserModel { Id = 2 } };

            // Case: GetAccount2Users returns list but Account2UserId doesn't exist
            accountRepositoryMock.Setup(repo => repo.GetAccount2Users()).ReturnsAsync(account2Users);
            var result1 = await accountService.AddAccountUserActivityProgress(accountUserActivityModel);
            Assert.IsNull(result1, "Result must be null when Account2UserId doesn't exist in the list.");

            //Case: GetAccount2Users returns null
            accountRepositoryMock.Setup(repo => repo.GetAccount2Users()).ReturnsAsync((List<Account2UserModel>)null);
            var result2 = await accountService.AddAccountUserActivityProgress(accountUserActivityModel);
            Assert.IsNull(result2, "Result must be null when GetAccount2Users returns null.");

            // Case: AddAccountUserActivityProgress returns null
            accountRepositoryMock.Setup(repo => repo.GetAccount2Users()).ReturnsAsync(new List<Account2UserModel> { new Account2UserModel { Id = 1 } });
            accountRepositoryMock.Setup(repo => repo.AddAccountUserActivityProgress(accountUserActivityModel)).ReturnsAsync((AccountUserActivityModel)null);
            var result3 = await accountService.AddAccountUserActivityProgress(accountUserActivityModel);
            Assert.IsNull(result3, "Result must be null when AddAccountUserActivityProgress returns null.");

            // Case: AddAccountUserActivityProgress does not return null
            var expectedActivityUser = new AccountUserActivityModel { Account2UserId = 1 };
            accountRepositoryMock.Setup(repo => repo.AddAccountUserActivityProgress(accountUserActivityModel)).ReturnsAsync(expectedActivityUser);
            var result4 = await accountService.AddAccountUserActivityProgress(accountUserActivityModel);
            Assert.IsNotNull(result4, "Result should not be null when AddAccountUserActivityProgress does not return null.");
            Assert.AreEqual(expectedActivityUser, result4, "Result should match the expected value when AddAccountUserActivityProgress does not return null.");
        }

        [TestMethod]
        public async Task ModifyAccount_ReturnExpectedResults()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var accountService = new AccountService(mockAccountRepository.Object, null, null);

            var validAccountModel = new AccountModel { Id = 1, Name = "Test Account" };
            var invalidAccountModel = new AccountModel { Id = 2, Name = "Invalid Account" };
            var modifiedAccountModel = new AccountModel { Id = 1, Name = "Modified Test Account" };

            mockAccountRepository.Setup(repo => repo.ModifyAccount(validAccountModel)).ReturnsAsync(modifiedAccountModel);
            mockAccountRepository.Setup(repo => repo.ModifyAccount(invalidAccountModel)).ReturnsAsync((AccountModel)null);

            // Act
            var validModifiedAccount = await accountService.ModifyAccount(validAccountModel);
            var invalidModifiedAccount = await accountService.ModifyAccount(invalidAccountModel);

            // Assert
            Assert.IsNotNull(validModifiedAccount, "Modified account should not be null.");
            Assert.AreEqual(modifiedAccountModel.Name, validModifiedAccount.Name, "Modified account name should match the expected value.");
            Assert.IsNull(invalidModifiedAccount, "Result must be null, when modified account is null.");

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => accountService.ModifyAccount(null), "Expected ArgumentNullException when account model is null.");
        }

        [TestMethod]
        public async Task RemoveUserFromAccount_ReturnExpectedResults()
        {
            // Arrange
            var accountRepositoryMock = new Mock<IAccountRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();

            var accountService = new AccountService(accountRepositoryMock.Object, userRepositoryMock.Object, null);

            int validAccountId = 1;
            int invalidAccountId = 2;
            int validUserId = 1;
            int invalidUserId = 2;

            var validAccountModel = new AccountModel { Id = validAccountId };
            var invalidAccountModel = new AccountModel { Id = invalidAccountId };
            var validUserModel = new UserModel { Id = validUserId };
            var invalidUserModel = new UserModel { Id = invalidUserId };


            accountRepositoryMock.Setup(repo => repo.GetAccountById(validAccountId)).ReturnsAsync(validAccountModel);
            accountRepositoryMock.Setup(repo => repo.GetAccountById(invalidAccountId)).ReturnsAsync((AccountModel)null);
            accountRepositoryMock.Setup(repo => repo.RemoveUserFromAccount(validAccountId, validUserId)).ReturnsAsync(validAccountModel);

            accountRepositoryMock.Setup(repo => repo.RemoveUserFromAccount(invalidAccountId, validUserId)).ReturnsAsync((AccountModel)null);
            accountRepositoryMock.Setup(repo => repo.RemoveUserFromAccount(validAccountId, invalidUserId)).ReturnsAsync((AccountModel)null);
            accountRepositoryMock.Setup(repo => repo.RemoveUserFromAccount(invalidAccountId, invalidUserId)).ReturnsAsync((AccountModel)null);

            userRepositoryMock.Setup(repo => repo.GetUserById(validUserId)).ReturnsAsync(validUserModel);
            userRepositoryMock.Setup(repo => repo.GetUserById(invalidUserId)).ReturnsAsync((UserModel)null);


            var result1 = await accountService.RemoveUserFromAccount(validAccountId, validUserId);
            var result2 = await accountService.RemoveUserFromAccount(invalidAccountId, validUserId);
            var result3 = await accountService.RemoveUserFromAccount(validAccountId, invalidUserId);
            var result4 = await accountService.RemoveUserFromAccount(invalidUserId, invalidAccountId);

            Assert.IsTrue(result1);
            Assert.IsFalse(result2);
            Assert.IsFalse(result3);
            Assert.IsFalse(result4);


            await Assert.ThrowsExceptionAsync<ArgumentException>(() => accountService.RemoveUserFromAccount(-1, -1));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => accountService.RemoveUserFromAccount(1, -1));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => accountService.RemoveUserFromAccount(-1, 1));
        }

        [TestMethod]
        public async Task DeleteAccount_ReturnsExpectedResults()
        {
            // Arrange
            var accountRepositoryMock = new Mock<IAccountRepository>();
            var accountService = new AccountService(accountRepositoryMock.Object, null, null);

            int validAccountId = 1;
            int invalidAccountId = 10;
            int negativeAccountId = -1;

            accountRepositoryMock.Setup(repo => repo.DeleteAccount(validAccountId)).ReturnsAsync(true);
            accountRepositoryMock.Setup(repo => repo.DeleteAccount(invalidAccountId)).ReturnsAsync(false);

            // Act
            var resultForValidId = await accountService.DeleteAccount(validAccountId);
            var resultForInvalidId = await accountService.DeleteAccount(invalidAccountId);

            // Assert
            Assert.IsTrue(resultForValidId, "Expected true for valid account ID");
            Assert.IsFalse(resultForInvalidId, "Expected false for invalid account ID");
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => accountService.DeleteAccount(negativeAccountId), "Expected ArgumentException for account ID less than or equal to zero");
        }
    }
}
