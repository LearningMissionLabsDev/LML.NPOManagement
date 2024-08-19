using LML.NPOManagement.Bll.Services;
using LML.NPOManagement.Common.Model;
using LML.NPOManagement.Common;
using LML.NPOManagement.Dal.Repositories.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TestProject.BLL.Test
{
    [TestClass]
    public class TestAccountService
    {
        [TestMethod]
        public async Task GetAllAccounts_WheneThereAreNotAccounts_ReturnsNull()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();
            var accountService = GetAccountService(accountRepositoryMock);
            accountRepositoryMock.Setup(repo => repo.GetAllAccounts()).ReturnsAsync((List<AccountModel>)null);

            // Act
            var retrievedAccounts = await accountService.GetAllAccounts();

            // Assert
            Assert.IsNull(retrievedAccounts);
        }

        [TestMethod]
        public async Task GetAllAccounts_WheneThereAreAccounts_ReturnsAccounts()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();
            var accountService = GetAccountService(accountRepositoryMock);

            var accounts = new List<AccountModel> {
                new AccountModel { Id = 1, Name = "TestAccount" },
                new AccountModel { Id = 2, Name = "TestAccount" },
                new AccountModel { Id = 3, Name = "TestAccount" }
            };

            accountRepositoryMock.Setup(repo => repo.GetAllAccounts()).ReturnsAsync(accounts);

            // Act
            var retrievedAccounts = await accountService.GetAllAccounts();

            // Assert
            Assert.AreEqual(retrievedAccounts, accounts);
        }



        [TestMethod]
        public async Task GetAccountById_ExistingAccountId_ReturnsAccount()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();
            var accountService = GetAccountService(accountRepositoryMock);

            var existingAccountId = 1;
            var existingAccount = new AccountModel { Id = existingAccountId, Name = "Account" };
            accountRepositoryMock.Setup(repo => repo.GetAccountById(existingAccountId)).ReturnsAsync(existingAccount);

            // Act
            var retrievedAccountWhenIdIsValid = await accountService.GetAccountById(existingAccountId);

            // Assert
            Assert.IsNotNull(retrievedAccountWhenIdIsValid, "The retrieved account should not be null for a valid ID.");
            Assert.AreEqual(existingAccountId, retrievedAccountWhenIdIsValid.Id, "The ID of the retrieved account should match the requested ID for a valid ID.");
        }

        [TestMethod]
        public async Task GetAccountById_NonExistingAccountId_ReturnsNull()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();
            var accountService = GetAccountService(accountRepositoryMock);

            var nonExistentAccountId = 1;
            accountRepositoryMock.Setup(repo => repo.GetAccountById(nonExistentAccountId)).ReturnsAsync((AccountModel)null);

            // Act
            var retrievedAccountWhenIdIsNotValid = await accountService.GetAccountById(nonExistentAccountId);

            // Assert
            Assert.IsNull(retrievedAccountWhenIdIsNotValid, "The account should be null for a non-existent ID.");
        }



        //[TestMethod]
        //public async Task GetUsersByAccount_WhenInAccountWithAccountIdExistsUsers_ReturnsUsers()
        //{
        //    // Arrange
        //    var accountRepositoryMock = GetAccountRepository();
        //    var accountService = GetAccountService(accountRepositoryMock);

        //    int validAccountId = 1;

        //    var users = new List<UserModel> {
        //        new UserModel { Id = 1, Email = "Testemail1@gmail.com" },
        //        new UserModel { Id = 2, Email = "Testemail2@gmail.com" }
        //    };

        //    accountRepositoryMock.Setup(repo => repo.GetAccountById(validAccountId)).ReturnsAsync(new AccountModel());
        //    accountRepositoryMock.Setup(repo => repo.GetUsersByAccount(validAccountId)).ReturnsAsync(users);

        //    // Act
        //    var usersOfAccountWithValidId = await accountService.GetUsersByAccount(validAccountId);

        //    // Assert
        //    Assert.IsTrue(usersOfAccountWithValidId.SequenceEqual(users));
        //}

        //[TestMethod]
        //public async Task GetUsersByAccount_WhenAccountWithAccountIdIsNotExist_ReturnsNull()
        //{
        //    // Arrange
        //    var accountRepositoryMock = GetAccountRepository();
        //    var accountService = GetAccountService(accountRepositoryMock);

        //    int nonExistingAccountId = 1;

        //    accountRepositoryMock.Setup(repo => repo.GetUsersByAccount(nonExistingAccountId)).ReturnsAsync((List<UserModel>)null);

        //    // Act
        //    var usersOfNonExistingAccount = await accountService.GetUsersByAccount(nonExistingAccountId);

        //    // Assert
        //    Assert.IsNull(usersOfNonExistingAccount);
        //}

        //[TestMethod] 
        //public async Task GetUsersByAccount_WhenThereAreNotUsersInRequestedAccount_ReturnsNull()
        //{
        //    // Arrange
        //    var accountRepositoryMock = GetAccountRepository();
        //    var accountService = GetAccountService(accountRepositoryMock);

        //    int requestedAccountId = 1;

        //    accountRepositoryMock.Setup(repo => repo.GetAccountById(requestedAccountId)).ReturnsAsync(new AccountModel());
        //    accountRepositoryMock.Setup(repo => repo.GetUsersByAccount(requestedAccountId)).ReturnsAsync((List<UserModel>)null);

        //    // Act
        //    var usersOfUsersAbsentAccount = await accountService.GetUsersByAccount(requestedAccountId);

        //    // Assert
        //    Assert.IsNull(usersOfUsersAbsentAccount, "Expected null when there are not users in requested account.");
        //}

        [TestMethod]
        public async Task GetUsersByAccount_WhenAccountIdIsNotPositive_ReturnsNull()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();
            var accountService = GetAccountService(accountRepositoryMock);

            int notPositiveAccountId = -1;

            // Act
            var usersOfAccountWithNegativeId = await accountService.GetUsersByAccount(notPositiveAccountId);

            // Assert
            Assert.IsNull(usersOfAccountWithNegativeId, "Expected null when requested id is not positive.");
        }



        [TestMethod]
        public async Task GetAccountsByName_WhenNameIsEmptyOrNull_ReturnsNull()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();
            var accountService = GetAccountService(accountRepositoryMock);

            // Act
            var accountsWhichNameIsEmpty = await accountService.GetAccountsByName(string.Empty);
            var accountsWhichNameIsNull = await accountService.GetAccountsByName(null);

            // Assert
            Assert.IsNull(accountsWhichNameIsEmpty, "Expected null for empty account name");
            Assert.IsNull(accountsWhichNameIsNull, "Expected null for null account name");
        }

        [TestMethod]
        public async Task GetAccountsByName_WhenAccountsByNameExist_ReturnsAccount()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();
            var accountService = GetAccountService(accountRepositoryMock);

            var nameOfAccount = "account";

            var accounts = new List<AccountModel> {
                new AccountModel { Id = 1, Name = nameOfAccount},
                new AccountModel { Id = 2, Name = nameOfAccount }
            };

            accountRepositoryMock.Setup(repo => repo.GetAccountsByName(nameOfAccount)).ReturnsAsync(accounts);

            // Act
            var accountsWhichNameIsNotNullOrEmpty = await accountService.GetAccountsByName(nameOfAccount);

            // Assert
            Assert.AreEqual(accountsWhichNameIsNotNullOrEmpty, accounts, "When accounts by name exist, the result should be a list of accounts.");
        }

        [TestMethod]
        public async Task GetAccountsByName_WhenAccountsByNameAreNotExist_ReturnsAccount()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();
            var accountService = GetAccountService(accountRepositoryMock);

            var nameOfAccount = "account";

            var accounts = new List<AccountModel> {
                new AccountModel { Id = 1, Name = nameOfAccount},
                new AccountModel { Id = 2, Name = nameOfAccount }
            };

            accountRepositoryMock.Setup(repo => repo.GetAccountsByName("testname")).ReturnsAsync(accounts);

            // Act
            var accountsWhichNameIsNotNullOrEmpty = await accountService.GetAccountsByName(nameOfAccount);

            // Assert
            Assert.IsNull(accountsWhichNameIsNotNullOrEmpty, "When accounts by name are not exist, the result should be null.");
        }



        [TestMethod]
        public async Task GetAccountRoleProgress_WhenAccountIdIsNotPositive_ReturnsNull()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();
            var accountService = GetAccountService(accountRepositoryMock);

            int accountId = 0;

            // Act
            var result = await accountService.GetAccountRoleProgress(accountId, 0);

            // Assert
            Assert.IsNull(result, "Expected null when accountId is zero");
        }

        [TestMethod]
        public async Task GetAccountRoleProgress_WhenAccountUserActivityModelesAreNull_ReturnsNull()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();
            var accountService = GetAccountService(accountRepositoryMock);

            int idleUsersAccountId = 1; 
            int accountRoleId = 1;

            accountRepositoryMock.Setup(repo => repo.GetAccountRoleProgress(idleUsersAccountId)).ReturnsAsync((List<AccountUserActivityModel>)null);

            // Act
            var result = await accountService.GetAccountRoleProgress(idleUsersAccountId, accountRoleId);
            // Assert
            Assert.IsNull(result, "Expected null when user progresses are null");
        }

        [TestMethod]
        public async Task GetAccountRoleProgress_WhenNoMatchingAccountUserActivities_ReturnsNull()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();
            var accountService = GetAccountService(accountRepositoryMock);

            int existingAccountId = 1; 
            int requestedAccountRoleId = 1;

            var accountUserActivityModeles = new List<AccountUserActivityModel> {
                new AccountUserActivityModel { Account2UserModel = new Account2UserModel { AccountRoleId = requestedAccountRoleId + 1} }
            };

            accountRepositoryMock.Setup(repo => repo.GetAccountRoleProgress(existingAccountId)).ReturnsAsync(accountUserActivityModeles);

            // Act
            var result = await accountService.GetAccountRoleProgress(existingAccountId, requestedAccountRoleId);

            // Assert
            Assert.IsNull(result, "Expected null when no matching account user activities");
        }

        [TestMethod]
        public async Task GetAccountRoleProgress_WhenMatchingAccountUserActivitiesExist_ReturnsList()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();
            var accountService = GetAccountService(accountRepositoryMock);

            int existingAccountId = 1; 
            int requestedAccountRoleId = 1; 

            var accountUserActivityModeles = new List<AccountUserActivityModel> {
                new AccountUserActivityModel { Account2UserModel = new Account2UserModel { AccountRoleId = requestedAccountRoleId } }
            };

            accountRepositoryMock.Setup(repo => repo.GetAccountRoleProgress(existingAccountId)).ReturnsAsync(accountUserActivityModeles);

            // Act
            var result = await accountService.GetAccountRoleProgress(existingAccountId, requestedAccountRoleId);
            
            // Assert 
            Assert.IsTrue(result.SequenceEqual(accountUserActivityModeles), "Expected non-null result when matching account user activities exist");
        }



        [TestMethod]
        public async Task AccountLogin_WhenAccountIsNull_ReturnsNull()
        {
            // Arrange
            var userRepositoryMock = GetUserRepository();
            var accountRepositoryMock = GetAccountRepository();
            var accountService = GetAccountService(accountRepositoryMock, userRepositoryMock);

            int loginAttemptUserId = 1; 
            var account2UserModel = new Account2UserModel { UserId = loginAttemptUserId, AccountId = 1 }; 

            userRepositoryMock.Setup(repo => repo.GetUsersInfoAccount(loginAttemptUserId)).ReturnsAsync((List<Account2UserModel>)null);

            // Act
            var loginResult = await accountService.AccountLogin(account2UserModel);

            // Assert
            Assert.IsNull(loginResult, "Expected null when account is null");
        }

        [TestMethod]
        public async Task AccountLogin_WhenAccount2UserIsNull_ReturnsNull()
        {
            // Arrange
            var userRepositoryMock = GetUserRepository();
            var accountRepositoryMock = GetAccountRepository();
            var accountService = GetAccountService(accountRepositoryMock, userRepositoryMock);

            int loginAttemptUserId = 1;
            int requestedAccountId = 1;
            var account2UserModel = new Account2UserModel { UserId = loginAttemptUserId, AccountId = requestedAccountId };

            var accounts = new List<Account2UserModel> {
                new Account2UserModel { AccountId = requestedAccountId + 1 }
            };

            userRepositoryMock.Setup(repo => repo.GetUsersInfoAccount(loginAttemptUserId)).ReturnsAsync(accounts);

            // Act
            var loginResult = await accountService.AccountLogin(account2UserModel);

            // Assert
            Assert.IsNull(loginResult, "Expected null when Account2UserModel is null");
        }

        [TestMethod]
        public async Task AccountLogin_WhenAccountExists_ReturnsAccount2UserModel()
        {
            // Arrange
            var userRepositoryMock = GetUserRepository();
            var accountRepositoryMock = GetAccountRepository();
            var accountService = GetAccountService(accountRepositoryMock, userRepositoryMock);

            int loginAttemptUserId = 1;
            var account2UserModel = new Account2UserModel { UserId = loginAttemptUserId, AccountId = 1 }; 

            var accounts = new List<Account2UserModel> {
                new Account2UserModel { AccountId = account2UserModel.AccountId }
            };

            userRepositoryMock.Setup(repo => repo.GetUsersInfoAccount(loginAttemptUserId)).ReturnsAsync(accounts);

            // Act
            var loginResult = await accountService.AccountLogin(account2UserModel);

            // Assert
            Assert.IsNotNull(loginResult, "Expected non-null result when account exists");
            Assert.AreEqual(account2UserModel.Id, loginResult.Id, "Returned Account2UserModel should match the provided one");
        }



        [TestMethod]
        public async Task AddAccount_WhenAccountModelIsNull_ReturnsNull()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();
            var accountService = GetAccountService(accountRepositoryMock);

            // Act
            var result = await accountService.AddAccount(null);

            // Assert
            Assert.IsNull(result, "Expected null when accountModel is null");
        }

        [TestMethod]
        public async Task AddAccount_WhenAccountNameIsNullOrEmpty_ReturnsNull()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();
            var accountService = GetAccountService(accountRepositoryMock);

            // Act
            var resultEmptyName = await accountService.AddAccount(new AccountModel { Name = string.Empty });
            var resultNullName = await accountService.AddAccount(new AccountModel {});

            // Assert
            Assert.IsNull(resultEmptyName, "Expected null when accountModel name is empty");
            Assert.IsNull(resultNullName, "Expected null when accountModel name is null");
        }

        [TestMethod]
        public async Task AddAccount_WhenAccountAddedSuccessfully_ReturnsAccountModelWithActiveStatus()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();
            var accountService = GetAccountService(accountRepositoryMock);

            var accountToAdd = new AccountModel { Name = "Test Account" };

            var addedAccount = new AccountModel { Id = 1, Name = accountToAdd.Name, StatusId = (int)AccountStatusEnum.Deleted };

            accountRepositoryMock.Setup(repo => repo.AddAccount(accountToAdd)).ReturnsAsync(addedAccount);

            // Act
            var result = await accountService.AddAccount(accountToAdd);

            // Assert
            Assert.IsNotNull(result, "Expected non-null result when account is added successfully");
            Assert.AreEqual(addedAccount.Id, result.Id, "Expected added account id to match");
            Assert.AreEqual(addedAccount.Name, result.Name, "Expected added account name to match");
            Assert.AreEqual(result.StatusId, (int)AccountStatusEnum.Active, "Expected added account status to be Active");
        }

        [TestMethod]
        public async Task AddAccount_WhenAccountDidNotAdd_ReturnsNull()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();
            var accountService = GetAccountService(accountRepositoryMock);

            var accountToAdd = new AccountModel { Name = "Test Account" };

            accountRepositoryMock.Setup(repo => repo.AddAccount(accountToAdd)).ReturnsAsync((AccountModel)null);

            // Act
            var result = await accountService.AddAccount(accountToAdd);

            // Assert
            Assert.IsNull(result, "Expected null when account repository returns null");
        }



        [TestMethod]
        public async Task AddUserToAccount_WhenAccount2UserModelIsNull_ReturnsFalse()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();
            var accountService = GetAccountService(accountRepositoryMock);

            // Act
            var result = await accountService.AddUserToAccount(null);

            // Assert
            Assert.IsFalse(result, "Expected false when account2UserModel is null");
        }

        [TestMethod]
        public async Task AddUserToAccount_WhenAccountIsNull_ReturnsFalse()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();
            var accountService = GetAccountService(accountRepositoryMock);

            var existingAccountModel = new Account2UserModel { AccountId = 1 }; 

            accountRepositoryMock.Setup(repo => repo.GetAccountById(existingAccountModel.AccountId)).ReturnsAsync((AccountModel)null);

            // Act
            var result = await accountService.AddUserToAccount(existingAccountModel);

            // Assert
            Assert.IsFalse(result, "Expected false when account is null");
        }

        [TestMethod]
        public async Task AddUserToAccount_WhenUserAddedSuccessfully_ReturnsTrue()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();
            var accountService = GetAccountService(accountRepositoryMock);

            var account2UserModel = new Account2UserModel { AccountId = 1 }; 
            var account = new AccountModel { Id = account2UserModel.AccountId };

            accountRepositoryMock.Setup(repo => repo.GetAccountById(account2UserModel.AccountId)).ReturnsAsync(account);
            accountRepositoryMock.Setup(repo => repo.AddUserToAccount(account2UserModel)).ReturnsAsync(true);

            // Act
            var result = await accountService.AddUserToAccount(account2UserModel);

            // Assert
            Assert.IsTrue(result, "Expected true when user is added successfully");
        }

        [TestMethod]
        public async Task AddUserToAccount_WhenUserDidNotAdd_ReturnsFalse()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();
            var accountService = GetAccountService(accountRepositoryMock);

            var account2UserModel = new Account2UserModel { AccountId = 1 }; 
            var account = new AccountModel { Id = account2UserModel.AccountId };

            accountRepositoryMock.Setup(repo => repo.GetAccountById(account2UserModel.AccountId)).ReturnsAsync(account);
            accountRepositoryMock.Setup(repo => repo.AddUserToAccount(account2UserModel)).ReturnsAsync(false);

            // Act
            var result = await accountService.AddUserToAccount(account2UserModel);

            // Assert
            Assert.IsFalse(result, "Expected false when user is not added");
        }



        [TestMethod]
        public async Task AddAccountUserActivityProgress_WhenAccount2UsersIsNull_ReturnsNull()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();
            var accountService = GetAccountService(accountRepositoryMock);

            // Act
            var result = await accountService.AddAccountUserActivityProgress(new AccountUserActivityModel { Account2UserId = 1, Account2UserModel = null });

            // Assert
            Assert.IsNull(result, "Expected null when account2Users is null");
        }

        [TestMethod]
        public async Task AddAccountUserActivityProgress_WhenGivenAccount2UserDoesNotExist_ReturnsNull()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();
            var accountService = GetAccountService(accountRepositoryMock);

            accountRepositoryMock.Setup(repo => repo.GetAccount2Users()).ReturnsAsync(new List<Account2UserModel>());

            // Act
            var result = await accountService.AddAccountUserActivityProgress(new AccountUserActivityModel { Account2UserId = 1 });

            // Assert
            Assert.IsNull(result, "Expected null when account2UserId does not exist");
        }

        [TestMethod]
        public async Task AddAccountUserActivityProgress_WhenActivityUserIsNull_ReturnsNull()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();
            var accountService = GetAccountService(accountRepositoryMock);

            var account2UserModels = new List<Account2UserModel>
            {
                new Account2UserModel { Id = 1 } 
            };

            accountRepositoryMock.Setup(repo => repo.GetAccount2Users()).ReturnsAsync(account2UserModels);
            accountRepositoryMock.Setup(repo => repo.AddAccountUserActivityProgress(It.IsAny<AccountUserActivityModel>())).ReturnsAsync((AccountUserActivityModel)null);

            // Act
            var result = await accountService.AddAccountUserActivityProgress(new AccountUserActivityModel { Account2UserId = 1 });

            // Assert
            Assert.IsNull(result, "Expected null when activityUser is null");
        }

        [TestMethod]
        public async Task AddAccountUserActivityProgress_WhenActivityUserIsNotNull_ReturnsActivityUser()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();
            var accountService = GetAccountService(accountRepositoryMock);

            var account2UserModels = new List<Account2UserModel>
            {
                new Account2UserModel { Id = 1 }
            };

            var activityUserModel = new AccountUserActivityModel { Account2UserId = 1 }; 

            accountRepositoryMock.Setup(repo => repo.GetAccount2Users()).ReturnsAsync(account2UserModels);
            accountRepositoryMock.Setup(repo => repo.AddAccountUserActivityProgress(activityUserModel)).ReturnsAsync(activityUserModel); // Simulate successful addition

            // Act
            var result = await accountService.AddAccountUserActivityProgress(activityUserModel);

            // Assert
            Assert.IsNotNull(result, "Expected non-null result when activityUser is not null");
            Assert.AreEqual(activityUserModel, result, "Returned activityUser should match the provided one");
        }



        [TestMethod]
        public async Task ModifyAccount_WhenAccountToModifyIsNull_ReturnsNull()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();
            var accountService = GetAccountService(accountRepositoryMock);

            // Act
            var result = await accountService.ModifyAccount(null);

            // Assert
            Assert.IsNull(result, "Expected null when accountModel is null");
        }

        [TestMethod]
        public async Task ModifyAccount_WhenRepositoryReturnsNull_ReturnsNull()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();
            var accountService = GetAccountService(accountRepositoryMock);

            var accountModel = new AccountModel { Id = 1 };

            accountRepositoryMock.Setup(repo => repo.ModifyAccount(accountModel)).ReturnsAsync((AccountModel)null);

            // Act
            var result = await accountService.ModifyAccount(accountModel);

            // Assert
            Assert.IsNull(result, "Expected null when repository returns null");
        }

        [TestMethod]
        public async Task ModifyAccount_WhenRepositoryReturnsNonNullAccount_ReturnsModifiedAccount()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();
            var accountService = GetAccountService(accountRepositoryMock);

            var accountToModify = new AccountModel { Id = 1 }; 
            var modifiedAccount = new AccountModel { Id = accountToModify.Id, Name = "Modified Name" };
            accountRepositoryMock.Setup(repo => repo.ModifyAccount(accountToModify)).ReturnsAsync(modifiedAccount);

            // Act
            var result = await accountService.ModifyAccount(accountToModify);

            // Assert
            Assert.IsNotNull(result, "Expected non-null result when repository returns non-null account");
            Assert.AreEqual(modifiedAccount.Id, result.Id, "Expected modified account id to match");
            Assert.AreEqual(modifiedAccount.Name, result.Name, "Expected modified account name to match");
        }



        [TestMethod]
        public async Task RemoveUserFromAccount_WhenAccountIdIsNotPositive_ReturnsFalse()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();
            var userRepositoryMock = GetUserRepository();
            var accountService = GetAccountService(accountRepositoryMock, userRepositoryMock);

            // Act
            var result = await accountService.RemoveUserFromAccount(0, 1);

            // Assert
            Assert.IsFalse(result, "Expected false when accountId is not positive");
        }

        [TestMethod]
        public async Task RemoveUserFromAccount_WhenUserIdIsNotPositive_ReturnsFalse()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();
            var userRepositoryMock = GetUserRepository();
            var accountService = GetAccountService(accountRepositoryMock, userRepositoryMock);

            // Act
            var result = await accountService.RemoveUserFromAccount(1, 0);

            // Assert
            Assert.IsFalse(result, "Expected false when userId is not positive");
        }

        [TestMethod]
        public async Task RemoveUserFromAccount_WhenAccountOrUserNotFound_ReturnsFalse()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();
            var userRepositoryMock = GetUserRepository();
            var accountService = GetAccountService(accountRepositoryMock, userRepositoryMock);

            accountRepositoryMock.Setup(repo => repo.GetAccountById(It.IsAny<int>())).ReturnsAsync((AccountModel)null);
            userRepositoryMock.Setup(repo => repo.GetUserById(It.IsAny<int>())).ReturnsAsync((UserModel)null);

            // Act
            var result = await accountService.RemoveUserFromAccount(1, 1);

            // Assert
            Assert.IsFalse(result, "Expected false when account or user is not found");
        }

        [TestMethod]
        public async Task RemoveUserFromAccount_WhenUserRemoved_ReturnsTrue()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();
            var userRepositoryMock = GetUserRepository();
            var accountService = GetAccountService(accountRepositoryMock, userRepositoryMock);

            int accountToDeleteFromId = 1;
            int userToDeleteId = 1;

            var accountToDeleteFrom = new AccountModel { Id = accountToDeleteFromId };
            var userToDelete = new UserModel { Id = userToDeleteId };

            accountRepositoryMock.Setup(repo => repo.GetAccountById(accountToDeleteFromId)).ReturnsAsync(accountToDeleteFrom);
            userRepositoryMock.Setup(repo => repo.GetUserById(userToDeleteId)).ReturnsAsync(userToDelete);
            accountRepositoryMock.Setup(repo => repo.RemoveUserFromAccount(accountToDeleteFromId, userToDeleteId)).ReturnsAsync(accountToDeleteFrom);

            // Act
            var result = await accountService.RemoveUserFromAccount(accountToDeleteFromId, userToDeleteId);

            // Assert
            Assert.IsTrue(result, "Expected true when user is removed");
        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task DeleteAccount_WhenAccountIdIsNotPositive_ThrowsArgumentException()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();
            var accountService = GetAccountService(accountRepositoryMock);

            // Act
            await accountService.DeleteAccount(0);
            await accountService.DeleteAccount(-1);
        }

        [TestMethod]
        public async Task DeleteAccount_WhenAccountDeletedSuccessfully_ReturnsTrue()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();
            var accountService = GetAccountService(accountRepositoryMock);

            int accountIdToDelete = 1;

            accountRepositoryMock.Setup(repo => repo.DeleteAccount(accountIdToDelete)).ReturnsAsync(true);
            
            // Act
            var result = await accountService.DeleteAccount(accountIdToDelete);

            // Assert
            Assert.IsTrue(result, "Expected true when account is deleted successfully");
        }



        private static AccountService GetAccountService(Mock<IAccountRepository> mockedRepo)
        {
            return new AccountService(mockedRepo.Object, null, null);
        }

        private static AccountService GetAccountService(Mock<IAccountRepository> mockedAccountRepo, Mock<IUserRepository> mockedUserRepo)
        {
            return new AccountService(mockedAccountRepo.Object, mockedUserRepo.Object, null);
        }

        private static Mock<IAccountRepository> GetAccountRepository()
        {
            return new Mock<IAccountRepository>();
        }

        private static Mock<IUserRepository> GetUserRepository()
        {
            return new Mock<IUserRepository>();
        }
    }
}
