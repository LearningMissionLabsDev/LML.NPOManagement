using LML.NPOManagement.Common;
using LML.NPOManagement.Common.Model;
using LML.NPOManagement.Dal.Models;
using LML.NPOManagement.Dal.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.EntityFrameworkCore;

namespace TestProject.DAL.Test
{
    [TestClass]
    public class TestAccountRepository
    {
        [TestMethod]
        public async Task GetAllAccounts_WhenThereAreAccounts_ReturnsAccounts()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var accountRepository = GetAccountRepository(mockedDbContext);

            var accounts = new List<Account> {
                new Account { Id = 1, Name = "TestAccount1" },
                new Account { Id = 2, Name = "TestAccount2" },
            };

            mockedDbContext.Setup(context => context.Accounts).ReturnsDbSet(accounts);

            // Act
            var retriviedAccounts = await accountRepository.GetAllAccounts();

            // Assert
            Assert.AreEqual(retriviedAccounts.Count, accounts.Count);
        }

        [TestMethod]
        public async Task GetAllAccounts_WhenThereAreNotAccounts_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var accountRepository = GetAccountRepository(mockedDbContext);

            mockedDbContext.Setup(context => context.Accounts).ReturnsDbSet(new List<Account>());
            // Act
            var retriviedAccounts = await accountRepository.GetAllAccounts();

            // Assert
            Assert.IsNull(retriviedAccounts, "Result must be null when there are not accounts.");
        }



        [TestMethod]
        public async Task GetAccountById_WhenAccountIdIsNotPositive_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var accountRepository = GetAccountRepository(mockedDbContext);

            var notPositiveId = -1;

            // Act
            var retrievedAccountWhenIdIsNotPositive = await accountRepository.GetAccountById(notPositiveId);

            // Assert
            Assert.IsNull(retrievedAccountWhenIdIsNotPositive, "Result must be null when id is not positive");
        }

        [TestMethod]
        public async Task GetAccountById_ExistingAccountId_ReturnsAccount()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var accountRepository = GetAccountRepository(mockedDbContext);

            var existingAccountId = 1;
            var accounts = new List<Account>
            {
                new Account { Id = existingAccountId, Name = "Account" }
            };

            mockedDbContext.Setup(context => context.Accounts).ReturnsDbSet(accounts);

            // Act
            var retrievedAccountWhenIdIsValid = await accountRepository.GetAccountById(existingAccountId);

            // Assert
            Assert.IsNotNull(retrievedAccountWhenIdIsValid, "The retrieved account should not be null for a valid ID.");
            Assert.AreEqual(existingAccountId, retrievedAccountWhenIdIsValid.Id, "The ID of the retrieved account should match the requested ID for a valid ID.");
        }

        [TestMethod]
        public async Task GetAccountById_NonExistingAccountId_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var accountRepository = GetAccountRepository(mockedDbContext);

            var nonExistentAccountId = 2;
            var accounts = new List<Account>
            {
                new Account { Id = 1, Name = "Account" }
            };

            mockedDbContext.Setup(context => context.Accounts).ReturnsDbSet(accounts);


            // Act
            var retrievedAccountWhenIdIsNotValid = await accountRepository.GetAccountById(nonExistentAccountId);

            // Assert
            Assert.IsNull(retrievedAccountWhenIdIsNotValid, "The account should be null for a non-existent ID.");
        }



        [TestMethod]
        public async Task GetAccountsByName_WhenNameIsEmptyOrNull_ReturnsNull()
        {
            // Arrange
            var accountRepository = GetAccountRepository();

            // Act
            var accountsWhichNameIsEmpty = await accountRepository.GetAccountsByName(string.Empty);
            var accountsWhichNameIsNull = await accountRepository.GetAccountsByName(null);

            // Assert
            Assert.IsNull(accountsWhichNameIsEmpty, "Expected null for empty account name");
            Assert.IsNull(accountsWhichNameIsNull, "Expected null for null account name");
        }

        [TestMethod]
        public async Task GetAccountsByName_WhenAccountsByNameExist_ReturnsAccount()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var accountRepository = GetAccountRepository(mockedDbContext);

            var nameOfAccount = "account";

            var accounts = new List<Account> {
                new Account { Id = 1, Name = nameOfAccount},
                new Account { Id = 2, Name = nameOfAccount }
            };

            mockedDbContext.Setup(context => context.Accounts).ReturnsDbSet(accounts);

            // Act
            var accountsWhichNameIsNotNullOrEmpty = await accountRepository.GetAccountsByName(nameOfAccount);

            // Assert
            Assert.AreEqual(accountsWhichNameIsNotNullOrEmpty.Count, accounts.Count, "When the name isn't null or empty, the result should be a list of accounts.");
        }

        [TestMethod]
        public async Task GetAccountsByName_WhenAccountsByNameAreNotExists_ReturnsAccount()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var accountRepository = GetAccountRepository(mockedDbContext);

            var nameOfAccount = "account";

            var accounts = new List<Account> {
                new Account { Id = 1, Name = nameOfAccount},
                new Account { Id = 2, Name = nameOfAccount }
            };

            mockedDbContext.Setup(context => context.Accounts).ReturnsDbSet(accounts);

            // Act
            var accountsWhichNameIsNotNullOrEmpty = await accountRepository.GetAccountsByName(nameOfAccount);

            // Assert
            Assert.AreEqual(accountsWhichNameIsNotNullOrEmpty.Count, accounts.Count, "When the name isn't null or empty, the result should be a list of accounts.");
        }



        [TestMethod]
        public async Task GetAccountRoleProgress_WhenAccountIdIsNotPositive_ReturnsNull()
        {
            // Arrange
            var accountRepository = GetAccountRepository();

            int zeroAccountId = 0;
            int negativeAccountId = 0;

            // Act
            var resultWhenIdIsZero = await accountRepository.GetAccountRoleProgress(zeroAccountId);
            var resultWhenIdIsNegative = await accountRepository.GetAccountRoleProgress(negativeAccountId);

            // Assert
            Assert.IsNull(resultWhenIdIsZero, "Expected null when accountId is zero");
            Assert.IsNull(resultWhenIdIsNegative, "Expected null when accountId is negative");
        }

        [TestMethod]
        public async Task GetAccountRoleProgress_WhenAccountUserActivityModelesAreNull_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var accountRepository = GetAccountRepository(mockedDbContext);

            int idleUsersAccount = 1;
            int accountRoleId = 1;

            mockedDbContext.Setup(context => context.AccountUserActivities).ReturnsDbSet(new List<AccountUserActivity>());

            // Act
            var result = await accountRepository.GetAccountRoleProgress(idleUsersAccount);

            // Assert
            Assert.IsNull(result, "Expected null when user progresses are null");
        }

        [TestMethod]
        public async Task GetAccountRoleProgress_WhenNoMatchingAccountUserActivities_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var accountRepository = GetAccountRepository(mockedDbContext);

            int existingAccountId = 1;
            int requestedAccountRoleId = 1;

            var accountUserActivityModeles = new List<AccountUserActivity> {
                new AccountUserActivity { Account2User = new Account2User { AccountRoleId = requestedAccountRoleId + 1} }
            };

            mockedDbContext.Setup(context => context.AccountUserActivities).ReturnsDbSet(accountUserActivityModeles);

            // Act
            var result = await accountRepository.GetAccountRoleProgress(existingAccountId);

            // Assert
            Assert.IsNull(result, "Expected null when no matching account user activities");
        }



        [TestMethod]
        public async Task GetUsersByAccount_WhenAccountIdIsNotPositive_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var accountRepository = GetAccountRepository(mockedDbContext);

            int negativeAccountId = -1;
            int zetoAccountId = 0;

            // Act
            var usersOfAccountWithNegativeId = await accountRepository.GetUsersByAccount(negativeAccountId);
            var usersOfAccountWithZeroId = await accountRepository.GetUsersByAccount(zetoAccountId);

            // Assert
            Assert.IsNull(usersOfAccountWithNegativeId, "Expected null when requested id is negative.");
            Assert.IsNull(usersOfAccountWithZeroId, "Expected null when requested id is zero.");
        }

        [TestMethod]
        public async Task GetUsersByAccount_WhenAccountWithAccountIdIsNotExist_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var accountRepository = GetAccountRepository(mockedDbContext);

            int nonExistingAccountId = 1;

            var accounts = new List<Account> { new Account { Id = 2, StatusId = (int)AccountStatusEnum.Active } };

            var account2User = new Account2User();
            var accountUserActivity = new AccountUserActivity {};
            account2User.AccountUserActivities.Add(accountUserActivity);
            accounts[0].Account2Users.Add(account2User);

            mockedDbContext.Setup(context => context.Accounts).ReturnsDbSet(accounts);

            // Act
            var usersOfAccountWhichIsNotExist = await accountRepository.GetUsersByAccount(nonExistingAccountId);

            // Assert
            Assert.IsNull(usersOfAccountWhichIsNotExist, "Expected null when requested account is not exist.");
        }

        [TestMethod]
        public async Task GetUsersByAccount_WhenThereAreNotUsersInRequestedAccount_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var accountRepository = GetAccountRepository(mockedDbContext);

            int requestedAccountId = 1;

            var accounts = new List<Account> { new Account { Id = requestedAccountId, StatusId = (int)AccountStatusEnum.Active } };

            mockedDbContext.Setup(context => context.Accounts).ReturnsDbSet(accounts);

            // Act
            var usersOfAccount = await accountRepository.GetUsersByAccount(requestedAccountId);

            // Assert
            Assert.IsNull(usersOfAccount, "Expected null when there are not users in requested account.");
        }

        [TestMethod]
        public async Task GetUsersByAccount_WhenUserOfRequestedAccountFoundSuccessfully_ReturnsUsers()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var accountRepository = GetAccountRepository(mockedDbContext);

            int requestedAccountId = 1;

            var accounts = new List<Account> { new Account { Id = requestedAccountId, StatusId = (int)AccountStatusEnum.Active } };

            var account2User = new Account2User() { User = new User() { Id = requestedAccountId, Password = "Not Null Password" } };
            var accountUserActivity = new AccountUserActivity { Id = requestedAccountId };
            account2User.AccountUserActivities.Add(accountUserActivity);
            accounts[0].Account2Users.Add(account2User);

            mockedDbContext.Setup(context => context.Accounts).ReturnsDbSet(accounts);

            // Act
            var usersOfAccountWhichIsNotExist = await accountRepository.GetUsersByAccount(requestedAccountId);

            // Assert
            Assert.IsNotNull(usersOfAccountWhichIsNotExist, "Expected not null result when users found successfully.");
            usersOfAccountWhichIsNotExist.ForEach(u => Assert.IsNull(u.Password, "Expected users passwords to be null."));
        }



        [TestMethod]
        public async Task AddAccount_WhenAccountModelIsNull_ReturnsNull()
        {
            // Arrange
            var accountRepository = GetAccountRepository();

            // Act
            var result = await accountRepository.AddAccount(null);

            // Assert
            Assert.IsNull(result, "Expected null when accountModel is null.");
        }

        [TestMethod]
        public async Task AddAccount_WhenAccountNameIsNullOrEmpty_ReturnsNull()
        {
            // Arrange
            var accountRepository = GetAccountRepository();

            // Act
            var resultEmptyName = await accountRepository.AddAccount(new AccountModel { Name = string.Empty });
            var resultNullName = await accountRepository.AddAccount(new AccountModel { });

            // Assert
            Assert.IsNull(resultEmptyName, "Expected null when accountModel name is empty.");
            Assert.IsNull(resultNullName, "Expected null when accountModel name is null.");
        }

        [TestMethod]
        public async Task AddAccount_WhenAccountDidNotAdd_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var accountRepository = GetAccountRepository(mockedDbContext);

            var accountToAdd = new AccountModel { Id = 1, Name = "Test Account" };
            var accounts = new List<Account> { new Account { Id = 2, Name = "Test Account" } };

            mockedDbContext.Setup(context => context.Accounts).ReturnsDbSet(accounts);

            // Act
            var result = await accountRepository.AddAccount(accountToAdd);

            // Assert
            Assert.IsNull(result, "Expected null when account did not add");
        }

        [TestMethod]
        public async Task AddAccount_WhenAccountAddedSuccesfully_ReturnsAddedAccount()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var accountRepository = GetAccountRepository(mockedDbContext);

            var accountToAdd = new AccountModel { Id = 1, Name = "Test Account" };
            var accounts = new List<Account> { new Account { Id = 1, Name = "Test Account" } };

            mockedDbContext.Setup(context => context.Accounts).ReturnsDbSet(accounts);

            // Act
            var result = await accountRepository.AddAccount(accountToAdd);

            // Assert
            Assert.IsNotNull(result, "Expected not null when account added successfully");
            Assert.AreEqual(result.Account2Users.First().AccountRoleId, (int)UserAccountRoleEnum.Admin, "Expected user role id admin.");
        }



        [TestMethod]
        public async Task GetAccount2Users_WhenThereAreNotAnyAccount2Users_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var accountRepository = GetAccountRepository(mockedDbContext);

            mockedDbContext.Setup(context => context.Account2Users).ReturnsDbSet(new List<Account2User>());

            // Act
            var account2UsersWhenThereIsNotAny = await accountRepository.GetAccount2Users();

            // Assert
            Assert.IsNull(account2UsersWhenThereIsNotAny, "Expected null when there are not any account2users.");
        }

        [TestMethod]
        public async Task GetAccount2Users_WhenThereAreAccount2Users_ReturnsAccount2Users()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var accountRepository = GetAccountRepository(mockedDbContext);

            var account2Users = new List<Account2User>() { new Account2User { Id = 1 } };

            mockedDbContext.Setup(context => context.Account2Users).ReturnsDbSet(account2Users);

            // Act
            var account2UsersWhenThereAreAny = await accountRepository.GetAccount2Users();

            // Assert
            Assert.IsNotNull(account2UsersWhenThereAreAny, "Expected not null when there are account2users.");
            Assert.AreEqual(account2Users.First().Id, account2UsersWhenThereAreAny.First().Id, "Expected account2users ids to match when there are account2users.");
        }



        [TestMethod]
        public async Task ModifyAccount_WhenAccountToModifyIsNull_ReturnsNull()
        {
            // Arrange
            var accountRepository = GetAccountRepository();

            // Act
            var result = await accountRepository.ModifyAccount(null);

            // Assert
            Assert.IsNull(result, "Expected null when accountModel is null");
        }

        [TestMethod]
        public async Task ModifyAccount_WhenAccountToModifyIsNotExist_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var accountRepository = GetAccountRepository(mockedDbContext);

            var accountToModify = new AccountModel { Id = 1 };
            mockedDbContext.Setup(context => context.Accounts).ReturnsDbSet(new List<Account>());

            // Act
            var result = await accountRepository.ModifyAccount(accountToModify);

            // Assert
            Assert.IsNull(result, "Expected null when account to modify is not exist");
        }

        [TestMethod]
        public async Task ModifyAccount_WhenAccountStatusIsDeleted_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var accountRepository = GetAccountRepository(mockedDbContext);

            var accountToModify = new Account { Id = 1, StatusId = (int)AccountStatusEnum.Deleted };
            var accountModelToModify = new AccountModel { Id = 1, StatusId = (int)AccountStatusEnum.Deleted }; 

            var accounts = new List<Account> {
                accountToModify,
                new Account { Id = 2, Name = "TestAccount2" },
            };

            mockedDbContext.Setup(context => context.Accounts).ReturnsDbSet(accounts);

            // Act
            var result = await accountRepository.ModifyAccount(accountModelToModify);

            // Assert
            Assert.IsNull(result, "Expected null when account status is deleted");
        }

        [TestMethod]
        public async Task ModifyAccount_WhenAccountModifiedSuccessfully_ReturnsModifiedAccount2()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var accountRepository = GetAccountRepository(mockedDbContext);

            var accounModelToModify = new AccountModel { Id = 1, Name = "New Name", Description = "New Description"};
            var account = new Account { Id = 1, Name = "Old Name", Description = "Old Description" };

            var accounts = new List<Account> { account };

            mockedDbContext.Setup(context => context.Accounts).ReturnsDbSet(accounts);

            // Act
            var result = await accountRepository.ModifyAccount(accounModelToModify);

            // Assert
            Assert.IsNotNull(result, "Expected non-null result when account modified successfully");
            Assert.AreEqual(result.Name, accounModelToModify.Name, "Expected modified account name to match");
            Assert.AreEqual(result.Description, accounModelToModify.Description, "Expected modified account description to match");
            Assert.AreEqual(result.StatusId, accounModelToModify.StatusId, "Expected modified account status to match");
        }



        [TestMethod]
        public async Task DeleteAccount_WhenAccountIdIsNotPositive_ReturnsFalse()
        {
            // Arrange
            var accountRepository = GetAccountRepository();

            // Act
            var accountIdZeroResult = await accountRepository.DeleteAccount(0);
            var negativeAccountIdResult = await accountRepository.DeleteAccount(-1);

            // Assert
            Assert.IsFalse(accountIdZeroResult, "Result must be false when accound Id is zero.");
            Assert.IsFalse(negativeAccountIdResult, "Result must be false when accound Id is negative.");
        }

        [TestMethod]
        public async Task DeleteAccount_WhenAccountDeletedSuccessfully_ReturnsTrue()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var accountRepository = GetAccountRepository(mockedDbContext);

            int accountIdToDelete = 1;

            var accounts = new List<Account> { new Account { Id = accountIdToDelete, StatusId = (int)AccountStatusEnum.Active } };

            var account2User = new Account2User();
            var accountUserActivity = new AccountUserActivity { Id = accountIdToDelete };
            account2User.AccountUserActivities.Add(accountUserActivity);
            accounts[0].Account2Users.Add(account2User);

            var deletedAccounts = new List<Account> { new Account { Id = accountIdToDelete, StatusId = (int)AccountStatusEnum.Active } };

            var account2UserDeleted = new Account2User();
            var accountUserActivityDeleted = new AccountUserActivity { Id = accountIdToDelete };
            account2UserDeleted.AccountUserActivities.Add(accountUserActivityDeleted);
            deletedAccounts[0].Account2Users.Add(account2UserDeleted);

            mockedDbContext.Setup(context => context.Accounts).ReturnsDbSet(accounts);
            mockedDbContext.Setup(context => context.AccountUserActivities.RemoveRange(It.IsAny<IEnumerable<AccountUserActivity>>()))
                    .Callback<IEnumerable<AccountUserActivity>>(entities =>
                           {
                               entities.All(entity => deletedAccounts.First().Account2Users.First().AccountUserActivities.Remove(entity));
                           });
            mockedDbContext.Setup(context => context.Account2Users.RemoveRange(It.IsAny<IEnumerable<Account2User>>()))
                    .Callback<IEnumerable<Account2User>>(entities =>
                    {
                        entities.All(entity => deletedAccounts.First().Account2Users.Remove(entity));
                    });

            // Act
            var result = await accountRepository.DeleteAccount(accountIdToDelete);

            // Assert
            Assert.IsTrue(result, "Expected true when account is deleted successfully");
            Assert.IsTrue(deletedAccounts.First().Account2Users.First().AccountUserActivities.First().Id == accountIdToDelete);
            Assert.AreEqual((int)AccountStatusEnum.Deleted, accounts[0].StatusId, "Expected account status to be set to Deleted");
        }

        [TestMethod]
        public async Task DeleteAccount_WhenAccountIsNotDeletedSuccessfully_ReturnsFalse()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var accountRepository = GetAccountRepository(mockedDbContext);

            int accountIdToDelete = 1;

            var accounts = new List<Account> { new Account { Id = 2, StatusId = (int)AccountStatusEnum.Active } };

            var account2User = new Account2User();
            var accountUserActivity = new AccountUserActivity { };
            account2User.AccountUserActivities.Add(accountUserActivity);
            accounts[0].Account2Users.Add(account2User);

            mockedDbContext.Setup(context => context.Accounts).ReturnsDbSet(accounts);

            // Act
            var result = await accountRepository.DeleteAccount(accountIdToDelete);

            // Assert
            Assert.IsFalse(result, "Expected false when account is not deleted successfully");
        }



        [TestMethod]
        public async Task AddUserToAccount_WhenAccount2UserModelIsNull_ReturnsFalse()
        {
            // Arrange
            var accountRepositoryMock = GetAccountRepository();

            // Act
            var result = await accountRepositoryMock.AddUserToAccount(null);

            // Assert
            Assert.IsFalse(result, "Expected false when account2UserModel is null");
        }

        [TestMethod]
        public async Task AddUserToAccount_WhenAccountOrUserIsNotExist_ReturnsFalse()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var accountRepository = GetAccountRepository(mockedDbContext);

            mockedDbContext.Setup(context => context.Accounts).ReturnsDbSet(new List<Account>());
            mockedDbContext.Setup(context => context.Users).ReturnsDbSet(new List<User>());

            // Act
            var result = await accountRepository.AddUserToAccount(new Account2UserModel());

            // Assert
            Assert.IsFalse(result, "Expected false when account or user is null");
        }

        [TestMethod]
        public async Task AddUserToAccount_WhenUserDidNotAddToAccount_ReturnsFalse()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var accountRepository = GetAccountRepository(mockedDbContext);

            var accounts = new List<Account> { new Account { Id = 1 } };
            var existingAccount = new Account2UserModel() { Id = 1, AccountId = 1, UserId = 1 };

            var account2User = new Account2User() { Id = 1 };
            accounts[0].Account2Users.Add(account2User);

            mockedDbContext.Setup(context => context.Accounts).ReturnsDbSet(accounts);
            mockedDbContext.Setup(context => context.Users).ReturnsDbSet(new List<User>());

            // Act
            var result = await accountRepository.AddUserToAccount(existingAccount);

            // Assert
            Assert.IsFalse(result, "Expected false when user did not add.");
        }

        [TestMethod]
        public async Task AddUserToAccount_WhenUserAddedSuccesfully_ReturnsTrue()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var accountRepository = GetAccountRepository(mockedDbContext);

            var accounts = new List<Account> { new Account { Id = 1 } };
            var existingAccount = new Account2UserModel() { Id = 1, AccountId = 1, UserId = 1 };

            var account2User = new Account2User() { Id = 1 };
            accounts[0].Account2Users.Add(account2User);

            mockedDbContext.Setup(context => context.Accounts).ReturnsDbSet(accounts);
            mockedDbContext.Setup(context => context.Users).ReturnsDbSet(new List<User> { new User { Id = 1 } });

            // Act
            var result = await accountRepository.AddUserToAccount(existingAccount);

            // Assert
            Assert.IsTrue(result, "Expected true when user added successfully.");
        }



        [TestMethod]
        public async Task AddAccountUserActivityProgress_WhenProgressDidNotAdd_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var accountRepository = GetAccountRepository(mockedDbContext);
            var accountUserActivityModel = new AccountUserActivityModel() { Account2UserModel = new Account2UserModel(), ActivityInfo = "" };

            mockedDbContext.Setup(context => context.AccountUserActivities).ReturnsDbSet(new List<AccountUserActivity>());

            // Act
            var result = await accountRepository.AddAccountUserActivityProgress(accountUserActivityModel);

            // Assert
            Assert.IsNull(result, "Expected null when progress did not add.");
        }

        [TestMethod]
        public async Task AddAccountUserActivityProgress_WhenProgressAddedSuccessfully_ReturnsAddedAccount()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var accountRepository = GetAccountRepository(mockedDbContext);
            var accountUserActivityModel = new AccountUserActivityModel() { Id = 1, Account2UserModel = new Account2UserModel(), ActivityInfo = "" };

            mockedDbContext.Setup(context => context.AccountUserActivities).ReturnsDbSet(new List<AccountUserActivity>() { new AccountUserActivity() { Id = 1 } });

            // Act
            var result = await accountRepository.AddAccountUserActivityProgress(accountUserActivityModel);

            // Assert
            Assert.IsNotNull(result, "Expected not null when progress added successfully.");
            Assert.AreEqual(result.Id, accountUserActivityModel.Id, "Expected not null when progress added successfully.");
        }



        private static AccountRepository GetAccountRepository(Mock<NpomanagementContext> mockedDbContext)
        {
            return new AccountRepository(mockedDbContext.Object);
        }

        private static AccountRepository GetAccountRepository()
        {
            return new AccountRepository(null);
        }

        private static Mock<NpomanagementContext> GetMockedDbContext()
        {
            return new Mock<NpomanagementContext>();
        }
    }
}