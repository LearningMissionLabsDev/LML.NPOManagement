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
    public class TestUserRepository
    {

        [TestMethod]
        public async Task GetAllUsers_WhenThereAreNotUsers_ReturnsNull()
        {
            //Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            mockedDbContext.Setup(context => context.Users).ReturnsDbSet(new List<User>());

            // Act
            var result = await userRepository.GetAllUsers();

            // Assert
            Assert.IsNull(result, "Result must be null when there are not users.");
        }

        [TestMethod]
        public async Task GetAllusers_WhenThereAreUsers_Returnsusers()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            var user = new List<User> {
              new User { Id = 1, Email = "First@example.com" },
              new User { Id = 2, Email = "Second@example.com" },
            };

            mockedDbContext.Setup(context => context.Users).ReturnsDbSet(user);

            // Act
            var retriviedusers = await userRepository.GetAllUsers();

            // Assert
            Assert.AreEqual(retriviedusers.Count, user.Count, "Retrivied users and mocked users must be equal.");
        }



        [TestMethod]
        public async Task GetUserById_WhenUserIdIsNotPositive_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            var negativeId = -1;
            var zeroId = 0;

            // Act
            var retrievedUserWhenIdIsNegative = await userRepository.GetUserById(negativeId);
            var retrievedUserWhenIdIsZero = await userRepository.GetUserById(zeroId);

            // Assert
            Assert.IsNull(retrievedUserWhenIdIsNegative, "Result must be null when id is negative.");
            Assert.IsNull(retrievedUserWhenIdIsZero, "Result must be null when id equal to zero.");
        }

        [TestMethod]
        public async Task GetUserById_ExistingUserId_ReturnsUser()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            var existingUserId = 1;
            var users = new List<User> { new User { Id = existingUserId, Email = "test@example.com" } };

            mockedDbContext.Setup(context => context.Users).ReturnsDbSet(users);

            // Act
            var retrievedUserWhenIdIsValid = await userRepository.GetUserById(existingUserId);

            // Assert
            Assert.IsNotNull(retrievedUserWhenIdIsValid, "The retrieved user should not be null for a existing ID.");
            Assert.AreEqual(existingUserId, retrievedUserWhenIdIsValid.Id, "The ID of the retrieved user should match the requested ID for a existing ID.");
        }

        [TestMethod]
        public async Task GetUserById_NonExistingUserId_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            var nonExistentUserId = 2;
            var user = new List<User>
            {
                new User {Id = 1, Email = "test@example.com"}
            };

            mockedDbContext.Setup(context => context.Users).ReturnsDbSet(user);


            // Act
            var retrievedUserWhenIdIsNotValid = await userRepository.GetUserById(nonExistentUserId);

            // Assert
            Assert.IsNull(retrievedUserWhenIdIsNotValid, "The user should be null for a non-existent user ID.");
        }



        [TestMethod]
        public async Task GetUsersByEmail_WhenEmailIsEmptyOrNull_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository();

            // Act
            var users = new List<User>
            {
                new User {Id = 1, Email = "test@example.com" }
            };

            mockedDbContext.Setup(context => context.Users).ReturnsDbSet(users);

            // Act
            var resultWhenEmailIsEmpty = await userRepository.GetUserByEmail(string.Empty);
            var resultWhenEmailIsNull = await userRepository.GetUserByEmail(null);

            // Assert
            Assert.IsNull(resultWhenEmailIsEmpty, "Expected null for empty user email.");
            Assert.IsNull(resultWhenEmailIsNull, "Expected null for null user email.");
        }

        [TestMethod]
        public async Task GetUsersByEmail_WhenExistingEmail_ReturnsUser()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            // Act
            var existingEmail = "test@example.com";
            var users = new List<User>
            {
                new User {Id = 1, Email = existingEmail }
            };

            mockedDbContext.Setup(context => context.Users).ReturnsDbSet(users);

            // Act
            var retrievedUser = await userRepository.GetUserByEmail(existingEmail);

            // Assert
            Assert.IsNotNull(retrievedUser, "Retrivied user must be not null when email exists.");
            Assert.AreEqual(existingEmail, retrievedUser.Email, "Email must be equal to retrivied user email.");
        }

        [TestMethod]
        public async Task GetUsersByEmail_WhenUsersByEmailDoNotExist_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            // Act
            var nonExistingEmail = "user@example.com";
            var users = new List<User>
            {
                new User {Id = 1, Email = "test@example.com" }
            };

            mockedDbContext.Setup(context => context.Users).ReturnsDbSet(users);

            // Act
            var retriviedUser = await userRepository.GetUserByEmail(nonExistingEmail);

            // Assert
            Assert.IsNull(retriviedUser, "Result be null when users by given email dont exist.");
        }



        [TestMethod]
        public async Task GetSearchResult_WhenNameIsEmptyOrNull_ReturnsNull()
        {
            // Arrange
            var userRepository = GetUserRepository();

            // Act
            var usersWhichNameIsEmpty = await userRepository.GetSearchResults(string.Empty, false);
            var usersWhichNameIsNull = await userRepository.GetSearchResults(null, false);

            // Assert
            Assert.IsNull(usersWhichNameIsEmpty, "Expected null for empty user name");
            Assert.IsNull(usersWhichNameIsNull, "Expected null for null user name");
        }

        [TestMethod]
        public async Task GetSearchResult_WhenUsersByNameNullOrEmptyIncludeGroupFalse_ReturnsUser()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            var searchParam = "John";

            var userInformation = new List<UserInformation> {
                new UserInformation { Id = 1, FirstName = "Johnson", LastName = "Abruize" },
                new UserInformation { Id = 2, FirstName = "Jack", LastName = "Johnik" },
                new UserInformation { Id = 3, FirstName = "Tom", LastName = "Cruize" }
            };

            mockedDbContext.Setup(context => context.UserInformations).ReturnsDbSet(userInformation);

            // Act
            var usersWhichNameIsNotNullOrEmpty = await userRepository.GetSearchResults(searchParam, false);

            // Assert
            Assert.AreEqual(usersWhichNameIsNotNullOrEmpty.Count, 2, "When the name isn't null or empty, the result should be a list of users.");
        }

        [TestMethod]
        public async Task GetSearchResultByGroup_WhenGroupByNameNullOrEmpty_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            // Act
            var groupWhichNameIsEmpty = await userRepository.GetSearchResults(string.Empty, true);
            var groupWhichNameIsNull = await userRepository.GetSearchResults(null, true);

            // Assert
            Assert.IsNull(groupWhichNameIsEmpty, "Expected null for empty group name");
            Assert.IsNull(groupWhichNameIsNull, "Expected null for null group name");
        }

        [TestMethod]
        public async Task GetSearchResultByGroup_WhenGroupByNameExist_ReturnsGroup()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            var searchParam = "John";

            var userInformation = new List<UserInformation> {
                new UserInformation { Id = 1, FirstName = "Johnson", LastName = "Abruize" },
                new UserInformation { Id = 2, FirstName = "Jack", LastName = "Johnik" },
                new UserInformation { Id = 3, FirstName = "Tom", LastName = "Cruize" }
            };

            var groupInformation = new List<UsersGroup> {
                new UsersGroup { Id = 1, GroupName = "JohnGroup" }
            };

            mockedDbContext.Setup(context => context.UserInformations).ReturnsDbSet(userInformation);
            mockedDbContext.Setup(context => context.UsersGroups).ReturnsDbSet(groupInformation);

            // Act
            var groupsWhichNameIs = await userRepository.GetSearchResults(searchParam, true);

            // Assert
            Assert.AreEqual(groupsWhichNameIs.Count, 3, "When the name isn't null or empty, the result should be a list of users.");
        }



        [TestMethod]
        public async Task GetAllIdeas_WhenThereAreUserIdeas_ReturnsIdeas()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            var userIdeas = new List<UserIdea> {
                new UserIdea { Id = 1 },
                new UserIdea { Id = 2 },
            };

            mockedDbContext.Setup(context => context.UserIdeas).ReturnsDbSet(userIdeas);

            // Act
            var retriviedIdeas = await userRepository.GetAllIdeas();

            // Assert
            Assert.AreEqual(retriviedIdeas.Count, userIdeas.Count, "Retrivied ideas count and given ideas count must be equal.");
        }

        [TestMethod]
        public async Task GetAllIdeas_WhenThereAreNoUserIdeas_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            mockedDbContext.Setup(context => context.UserIdeas).ReturnsDbSet(new List<UserIdea>());
            // Act
            var retriviedIdeas = await userRepository.GetAllIdeas();

            // Assert
            Assert.IsNull(retriviedIdeas, "Result must be null when there are not user ideas.");
        }



        [TestMethod]
        public async Task GetAllGroups_WhenThereAreGroups_ReturnsGroups()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            var usersGroups = new List<UsersGroup> {
                new UsersGroup { Id = 1 },
            };

            mockedDbContext.Setup(context => context.UsersGroups).ReturnsDbSet(usersGroups);

            // Act
            var retriviedGroups = await userRepository.GetAllGroups();

            // Assert
            Assert.AreEqual(retriviedGroups.Count, usersGroups.Count, "Retrivied groups count and given groups count must be equal.");
        }

        [TestMethod]
        public async Task GetAllGroups_WhenThereAreNotGroups_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            mockedDbContext.Setup(context => context.UsersGroups).ReturnsDbSet(new List<UsersGroup>());

            // Act
            var retriviedGroups = await userRepository.GetAllGroups();

            // Assert
            Assert.IsNull(retriviedGroups, "Result must be null when there are not groups.");
        }



        [TestMethod]
        public async Task GetGroupById_WhenGroupIdIsNotPositive_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            var negativeId = -1;
            var zeroId = 0;

            // Act
            var retrievedGroupWhenIdIsNegative = await userRepository.GetGroupById(negativeId);
            var retrievedGroupWhenIdEqualToZero = await userRepository.GetGroupById(zeroId);

            // Assert
            Assert.IsNull(retrievedGroupWhenIdIsNegative, "Result must be null when id is negative.");
            Assert.IsNull(retrievedGroupWhenIdEqualToZero, "Result must be null when id is equal to zero.");
        }

        [TestMethod]
        public async Task GetGroupById_ExistingGroupId_ReturnsGroup()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            var existingGroupId = 1;
            var userGroups = new List<UsersGroup>
            {
                new UsersGroup { Id = 1 }
            };

            mockedDbContext.Setup(context => context.UsersGroups).ReturnsDbSet(userGroups);

            // Act
            var retrievedGroupWhenIdIsValid = await userRepository.GetGroupById(existingGroupId);

            // Assert
            Assert.IsNotNull(retrievedGroupWhenIdIsValid, "The retrieved group should not be null for a valid ID.");
            Assert.AreEqual(existingGroupId, retrievedGroupWhenIdIsValid.Id, "The ID of the retrieved group should match the requested ID for a valid ID.");
        }

        [TestMethod]
        public async Task GetGroupById_NonExistingGroupId_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            var nonExistentGroupId = 2;
            var usergroup = new List<User>
            {
                new User { Id = 1 }
            };

            mockedDbContext.Setup(context => context.Users).ReturnsDbSet(usergroup);


            // Act
            var retrievedGroupWhenIdIsNotValid = await userRepository.GetUserById(nonExistentGroupId);

            // Assert
            Assert.IsNull(retrievedGroupWhenIdIsNotValid, "The group should be null for a non-existent ID.");
        }



        [TestMethod]
        public async Task GetUsersByGroup_WhenGroupIdIsNotPositive_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            int negativeGroupId = -1;
            int zeroGroupId = 0;

            // Act
            var usersOfGroupWithNegativeId = await userRepository.GetUsersByGroupId(negativeGroupId);
            var usersOfGroupWithZeroId = await userRepository.GetUsersByGroupId(zeroGroupId);

            // Assert
            Assert.IsNull(usersOfGroupWithNegativeId, "Expected null when requested id is negative.");
            Assert.IsNull(usersOfGroupWithZeroId, "Expected null when requested id is zero.");
        }

        [TestMethod]
        public async Task GetUsersByGroup_WhenGroupWithGroupIdIsNotExist_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            int nonExistingGroupId = 1;

            var userGroups = new List<UsersGroup> { new UsersGroup { Id = 2 } };

            mockedDbContext.Setup(context => context.UsersGroups).ReturnsDbSet(userGroups);

            // Act
            var usersOfGroupWhichIsNotExist = await userRepository.GetUsersByGroupId(nonExistingGroupId);

            // Assert
            Assert.IsNull(usersOfGroupWhichIsNotExist, "Expected null when requested group is not exist.");
        }

        [TestMethod]
        public async Task GetUsersByGroup_WhenThereAreNotUsersInRequestedGroup_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            int requestedGroupId = 1;

            var userGroups = new List<UsersGroup> { new UsersGroup { Id = requestedGroupId } };

            mockedDbContext.Setup(context => context.UsersGroups).ReturnsDbSet(userGroups);

            // Act
            var usersOfGroup = await userRepository.GetUsersByGroupId(requestedGroupId);

            // Assert
            Assert.IsNull(usersOfGroup, "Expected null when there are not users in requested group.");
        }

        [TestMethod]
        public async Task GetUsersByGroup_WhenUserOfRequestedGroupFoundSuccessfully_ReturnsUsers()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            int requestedGroupId = 1;

            var usersGroup = new List<UsersGroup> { new UsersGroup { Id = requestedGroupId } };
            var userOfGroup = new User() { Id = 1 };
            usersGroup[0].Users.Add(userOfGroup);

            mockedDbContext.Setup(context => context.UsersGroups).ReturnsDbSet(usersGroup);

            // Act
            var foundedUsersOfGroup = await userRepository.GetUsersByGroupId(requestedGroupId);

            // Assert
            Assert.IsNotNull(foundedUsersOfGroup, "Expected not null result when users found successfully.");
            Assert.AreEqual(foundedUsersOfGroup.First().Id, 1, "Expected founded user id be equals to 1.");
        }



        [TestMethod]
        public async Task GetGroupsByName_WhenNameIsEmptyOrNull_ReturnsNull()
        {
            // Arrange
            var userRepository = GetUserRepository();

            // Act
            var groupsWhichNameIsEmpty = await userRepository.GetGroupsByName(string.Empty);
            var groupsWhichNameIsNull = await userRepository.GetGroupsByName(null);

            // Assert
            Assert.IsNull(groupsWhichNameIsEmpty, "Expected null for empty group name");
            Assert.IsNull(groupsWhichNameIsNull, "Expected null for null group name");
        }

        [TestMethod]
        public async Task GetGroupsByName_WhenGroupsByNameExist_ReturnsGroups()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            var requestedName = "group";

            var groups = new List<UsersGroup> {
                new UsersGroup { Id = 1, GroupName = requestedName},
                new UsersGroup { Id = 2, GroupName = requestedName }
            };

            mockedDbContext.Setup(context => context.UsersGroups).ReturnsDbSet(groups);

            // Act
            var retriviedGroups = await userRepository.GetGroupsByName(requestedName);

            // Assert
            Assert.AreEqual(retriviedGroups.Count, groups.Count, "The result should be a list of groups when groups by given name exist.");
        }

        [TestMethod]
        public async Task GetGroupsByName_WhenGroupsByNameAreNotExist_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            var groups = new List<UsersGroup> {
                new UsersGroup { Id = 1, GroupName = "group"}
            };

            mockedDbContext.Setup(context => context.UsersGroups).ReturnsDbSet(groups);

            // Act
            var retriviedGroups = await userRepository.GetGroupsByName("non-existing-group");

            // Assert
            Assert.IsNull(retriviedGroups, "The result must be null when groups by given name are not exist..");
        }



        [TestMethod]
        public async Task GetGroupsForUser_WhenUserIdIsNotPositive_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            var zeroId = 0;
            var negativeId = -1;

            // Act
            var retrievedGroupsWithZeroUserId = await userRepository.GetGroupsForUser(zeroId);
            var retrievedGroupsWithNegativeUserId = await userRepository.GetGroupsForUser(negativeId);

            // Assert
            Assert.IsNull(retrievedGroupsWithZeroUserId, "Result must be null when the user ID is not positive.");
            Assert.IsNull(retrievedGroupsWithNegativeUserId, "Result must be null when the user ID is not positive.");
        }

        [TestMethod]
        public async Task GetGroupsForUser_ExistingUserId_ReturnsGroup()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            var existingUserId = 1;
            var user = new User
            {
                Id = existingUserId
            };
            user.Groups.Add(new UsersGroup { Id = 1, GroupName = "Group 1" });
            user.Groups.Add(new UsersGroup { Id = 2, GroupName = "Group 2" });

            mockedDbContext.Setup(context => context.Users).ReturnsDbSet(new List<User>() { user });

            // Act
            var retrievedGroups = await userRepository.GetGroupsForUser(existingUserId);

            // Assert
            Assert.IsNotNull(retrievedGroups, "The retrieved groups should not be null for a valid user ID.");
            Assert.AreEqual(existingUserId, retrievedGroups.First().Id, "The ID of the first retrieved group should match the requested ID for a valid user ID.");
        }

        [TestMethod]
        public async Task GetGroupsForUser_NonExistingUserId_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            var user = new User
            {
                Id = 1
            };
            user.Groups.Add(new UsersGroup { Id = 1, GroupName = "Group 1" });
            user.Groups.Add(new UsersGroup { Id = 2, GroupName = "Group 2" });

            mockedDbContext.Setup(context => context.Users).ReturnsDbSet(new List<User>() { user });

            // Act
            var retrievedGroupWhenIdIsNotValid = await userRepository.GetGroupsForUser(2);

            // Assert
            Assert.IsNull(retrievedGroupWhenIdIsNotValid, "The retrieved groups should be null for a non-existent user ID.");
        }



        [TestMethod]
        public async Task DeleteGroup_WhenGroupIdIsNotPositive_ReturnsFalse()
        {
            // Arrange
            var userRepository = GetUserRepository();

            // Act
            var groupIdZeroResult = await userRepository.DeleteGroup(0);
            var negativeGroupIdResult = await userRepository.DeleteGroup(-1);

            // Assert
            Assert.IsFalse(groupIdZeroResult, "Result must be false when group ID is zero.");
            Assert.IsFalse(negativeGroupIdResult, "Result must be false when group ID is negative.");
        }

        [TestMethod]
        public async Task DeleteGroup_WhenGroupDeletedSuccessfully_ReturnsTrue()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            int groupIdToDelete = 1;

            var userGroups = new List<UsersGroup> { new UsersGroup { Id = groupIdToDelete } };
            var userOfGroup = new User { Id = groupIdToDelete };
            userGroups[0].Users.Add(userOfGroup);

            mockedDbContext.Setup(context => context.UsersGroups).ReturnsDbSet(userGroups);

            // Act
            var result = await userRepository.DeleteGroup(groupIdToDelete);

            // Assert
            Assert.IsTrue(result, "Expected true when group is deleted successfully");
        }

        [TestMethod]
        public async Task DeleteGroup_WhenGroupIsNotDeletedSuccessfully_ReturnsFalse()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            int groupIdToDelete = 1;

            var groups = new List<UsersGroup> { new UsersGroup { Id = 2 } };

            var userGroups = new List<UsersGroup> { new UsersGroup { Id = groupIdToDelete } };
            var userOfGroup = new User() { Id = groupIdToDelete };
            userGroups[0].Users.Add(userOfGroup);

            mockedDbContext.Setup(context => context.UsersGroups).ReturnsDbSet(groups);

            // Act
            var result = await userRepository.DeleteGroup(groupIdToDelete);

            // Assert
            Assert.IsFalse(result, "Expected false when group is not deleted successfully");
        }



        [TestMethod]
        public async Task DeleteUserFromGroup_InvalidInputs_ReturnsNull()
        {
            // Arrange
            var userRepository = GetUserRepository();

            // Act
            var result = await userRepository.DeleteUserFromGroup(-1, -1);
            
            // Assert
            Assert.IsNull(result, "Expected null, when IDs are negative.");
        }

        [TestMethod]
        public async Task DeleteUserFromGroup_Null_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            var userId = 1;
            var groupId = 1;

            var user = new User { Id = 4 };
            var groups = new List<UsersGroup> {
                new UsersGroup { Id = 4 }
            };

            groups.First().Users.Add(user);

            mockedDbContext.Setup(context => context.Users).ReturnsDbSet(new List<User>() { user });
            mockedDbContext.Setup(context => context.UsersGroups).ReturnsDbSet(groups);

            // Act
            var result = await userRepository.DeleteUserFromGroup(userId, groupId);

            // Assert
            Assert.IsNull(result, "The result should be null after deleting the user from the group.");
        }

        [TestMethod]
        public async Task DeleteUserFromGroup_InvalidUserId_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            var userId = 1;
            var groupId = 1;

            var user = new User { Id = 4 };
            var groups = new List<UsersGroup> {
                new UsersGroup { Id = 1 }
            };

            groups.First().Users.Add(user);

            mockedDbContext.Setup(context => context.Users).ReturnsDbSet(new List<User>() { user });
            mockedDbContext.Setup(context => context.UsersGroups).ReturnsDbSet(groups);

            // Act
            var result = await userRepository.DeleteUserFromGroup(userId, groupId);

            // Assert
            Assert.IsNull(result, "The result should be null when deleting a user with an invalid ID from the group.");
        }

        [TestMethod]
        public async Task DeleteUserFromGroup_ValidInputs_ReturnsUsersGroupModel()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            var userId = 1;
            var groupId = 1;

            var user = new User { Id = userId };
            var groups = new List<UsersGroup> {
                new UsersGroup { Id = 1 }
            };

            groups.First().Users.Add(user);

            mockedDbContext.Setup(context => context.Users).ReturnsDbSet(new List<User>() { user });
            mockedDbContext.Setup(context => context.UsersGroups).ReturnsDbSet(groups);

            // Act
            var result = await userRepository.DeleteUserFromGroup(userId, groupId);

            // Assert
            Assert.IsNotNull(result, "The result should not be null when deleting a user from the group.");
            Assert.AreEqual(groupId, result.Id, "The ID of the returned UsersGroup model should match the provided group ID.");
            Assert.AreEqual(0, result.Users.Count, "The UsersGroup model should have no users after deleting the user from the group.");
        }



        [TestMethod]
        public async Task GetUsersByInvestorTier_InvalidInputs_ReturnsNull()
        {
            // Arrange
            var userRepository = GetUserRepository();

            // Act
            var result = await userRepository.GetUsersByInvestorTier(-1);

            // Assert
            Assert.IsNull(result, "The result should be null when providing an invalid investor tier ID.");
        }

        [TestMethod]
        public async Task GetUsersByInvestorTier_InvalidInputsInvestor_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            // Act
            var investorTierId = 1;
            var investorInfoId = 1;

            var investor = new List<InvestorInformation> { new InvestorInformation { Id = investorInfoId } };

            mockedDbContext.Setup(context => context.InvestorInformations).ReturnsDbSet(investor);

            var result = await userRepository.GetUsersByInvestorTier(investorTierId);

            // Assert
            Assert.IsNull(result, "The result should be null when providing an invalid investor tier ID.");
        }

        [TestMethod]
        public async Task GetUsersByInvestorTier_InvalidInputsInvestorAndUser_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            // Act
            var investorTierId = 1;
            var invetsorInfoId = 1;

            var user = new User { Id = investorTierId };
            var investor = new List<InvestorInformation> { new InvestorInformation { Id = invetsorInfoId } };

            mockedDbContext.Setup(context => context.Users).ReturnsDbSet(new List<User>() { user });
            mockedDbContext.Setup(context => context.InvestorInformations).ReturnsDbSet(investor);

            var result = await userRepository.GetUsersByInvestorTier(investorTierId);

            // Assert
            Assert.IsNull(result, "The result should be null when providing invalid investor and user IDs.");
        }

        [TestMethod]
        public async Task GetUsersByInvestorTier_ValidInputs_ReturnsUsersGroupModel()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            var investorTierId = 1;
            var invetsorInfoId = 1;

            var user = new User { Id = investorTierId };
            var investor = new List<InvestorInformation> { new InvestorInformation { Id = invetsorInfoId, InvestorTierId = investorTierId, UserId = investorTierId } };

            investor.First().User = user;

            mockedDbContext.Setup(context => context.Users).ReturnsDbSet(new List<User>() { user });
            mockedDbContext.Setup(context => context.InvestorInformations).ReturnsDbSet(investor);

            // Act
            var result = await userRepository.GetUsersByInvestorTier(investorTierId);

            // Assert
            Assert.IsNotNull(result, "The result should not be null when providing valid inputs.");
        }



        [TestMethod]
        public async Task ModifyUserInfo_WhenUserToModifyIsNull_ReturnsFalse()
        {
            // Arrange
            var userRepository = GetUserRepository();

            // Act
            var result = await userRepository.ModifyUserInfo(null);

            // Assert
            Assert.IsFalse(result, "Expected null when accountModel is null");
        }

        [TestMethod]
        public async Task ModifyUserCredentials_InvalidUserId_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            var userId = 0;
            var email = "test@example.com";
            var password = "newpassword";
            var statusId = 2;

            // Act
            var result = await userRepository.ModifyUserCredentials(email, password, userId, statusId);

            // Assert
            Assert.IsNull(result, "The result should be null when providing an invalid user ID.");
        }

        [TestMethod]
        public async Task ModifyUserCredentials_InvalidInput_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            var userId = 1;
            var statusId = 2;
            var email = "test@example.com";
            var password = "testpassword";

            var userToModify = new UserModel
            {
                Id = userId,
                Email = "old@example.com",
                Password = "oldpassword",
                StatusId = 1
            };
            var user = new User
            {
                Id = 2,
                Email = "neww@example.com",
                Password = "newpassword",
                StatusId = 2
            };

            var users = new List<User> { user };

            mockedDbContext.Setup(context => context.Users).ReturnsDbSet(users);

            // Act
            var result = await userRepository.ModifyUserCredentials(email, password, userId, statusId);

            // Assert
            Assert.IsNull(result, "The result should be null when providing invalid input.");
        }

        [TestMethod]
        public async Task ModifyUserCredentials_ValidInput_ReturnsUserModel()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            var userId = 1;
            var statusId = 2;
            var email = "new@example.com";
            var password = "newpassword";

            var userToModify = new User
            {
                Id = userId,
                Email = "old@example.com",
                Password = "oldpassword",
                StatusId = 1
            };

            var users = new List<User> { userToModify };

            mockedDbContext.Setup(context => context.Users).ReturnsDbSet(users);

            // Act
            var result = await userRepository.ModifyUserCredentials(email, password, userId, statusId);

            // Assert
            Assert.IsNotNull(result, "The result should not be null when providing valid input.");
            Assert.AreEqual(email, result.Email, "The email of the returned UserModel should match the provided email.");
            Assert.AreEqual(password, result.Password, "The password of the returned UserModel should match the provided password.");
            Assert.AreEqual(statusId, result.StatusId, "The status ID of the returned UserModel should match the provided status ID.");
        }



        [TestMethod]
        public async Task UpdateUserStatus_NonExistingUser_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            // Act
            var result = await userRepository.UpdateUserStatus(0, StatusEnumModel.Active);

            // Assert
            Assert.IsNull(result, "The result should be null when updating status for a non-existing user.");
        }

        [TestMethod]
        public async Task UpdateUserStatus_NonUser_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            var userId = 1;
            mockedDbContext.Setup(context => context.Users).ReturnsDbSet(new List<User>());

            // Act
            var result = await userRepository.UpdateUserStatus(userId, StatusEnumModel.Active);

            // Assert
            Assert.IsNull(result, "The result should be null when updating status for a non-user.");
        }

        [TestMethod]
        public async Task UpdateUserStatus_ExistingUser_ReturnsUserModel()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            var userId = 1;
            var statusToUpdate = 2;

            var user = new User
            {
                Id = userId,
                StatusId = statusToUpdate
            };

            var users = new List<User> { user };
            mockedDbContext.Setup(context => context.Users).ReturnsDbSet(users);

            // Act
            var result = await userRepository.UpdateUserStatus(userId, (StatusEnumModel)statusToUpdate);

            // Assert
            Assert.IsNotNull(result, "The result should not be null when updating status for an existing user.");
            Assert.AreEqual(result.StatusId, statusToUpdate, "The status ID of the returned User should match the provided status ID.");
        }



        [TestMethod]
        public async Task UpdateUsersGroupStatus_ExistingUserGroup_StatusUpdatedSuccessfully()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            var statusToSet = GroupStatusEnum.Deleted;

            var users = new UsersGroup { Id = 1, StatusId = (int)GroupStatusEnum.Active };
            mockedDbContext.Setup(context => context.UsersGroups).ReturnsDbSet(new List<UsersGroup>() { users });
            // Act
            await userRepository.UpdateGroupStatus(1, statusToSet);

            // Assert
            mockedDbContext.Verify(d => d.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once, "Expected SaveChangesAsync to be called once.");
        }



        [TestMethod]
        public async Task AddUserIdea_WhenUserIdeaModelToAddIsNottNull_ReturnsUserIdea()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            mockedDbContext.Setup(context => context.UserIdeas.AddAsync(It.IsAny<UserIdea>(), It.IsAny<CancellationToken>()));
            mockedDbContext.Setup(context => context.SaveChangesAsync(It.IsAny<CancellationToken>()));

            // Act
            var result = await userRepository.AddUserIdea(new UserIdeaModel() { Id = 1 });

            // Assert
            mockedDbContext.Verify(m => m.UserIdeas.AddAsync(It.IsAny<UserIdea>(), It.IsAny<CancellationToken>()), Times.Once(), "Expected invocation on the mock once.");
            mockedDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once(), "Expected invocation on the mock once.");
            Assert.IsNotNull(result, "Expected result not be null.");
        }

        [TestMethod]
        public async Task AddUserIdea_WhenUserIdeaModelToAddIsNull_ReturnsNull()
        {
            // Arrange
            var userRepository = GetUserRepository();


            // Act
            var result = await userRepository.AddUserIdea(null);

            Assert.IsNull(result, "Expected result to be null.");
        }



        [TestMethod]
        public async Task AddUser_WhenUserModelIsNull_DidNotAdd()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            mockedDbContext.Setup(context => context.Users.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()));
            mockedDbContext.Setup(context => context.SaveChangesAsync(It.IsAny<CancellationToken>()));

            // Act
            await userRepository.AddUser(null);

            // Assert
            mockedDbContext.Verify(d => d.Users.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never, "Expected invocation on the mock never.");
            mockedDbContext.Verify(d => d.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never, "Expected invocation on the mock never.");
        }

        [TestMethod]
        public async Task AddUser_WhenUserModelIsNotNull_AddedSuccessfully()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            mockedDbContext.Setup(context => context.Users.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()));
            mockedDbContext.Setup(context => context.SaveChangesAsync(It.IsAny<CancellationToken>()));

            // Act
            var user = new UserModel { Id = 1, Email = "user@example.com" };
            await userRepository.AddUser(user);

            // Assert
            mockedDbContext.Verify(d => d.Users.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once, "Expected invocation on the mock once.");
            mockedDbContext.Verify(d => d.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once, "Expected invocation on the mock once.");
        }



        [TestMethod]
        public async Task AddUserInformationIdea_WhenUserInformationToAddIsNull_ReturnsNull()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            mockedDbContext.Setup(context => context.UserInformations.AddAsync(It.IsAny<UserInformation>(), It.IsAny<CancellationToken>()));
            mockedDbContext.Setup(context => context.SaveChangesAsync(It.IsAny<CancellationToken>()));


            // Act
            await userRepository.AddUserInformation(null);

            // Assert
            mockedDbContext.Verify(m => m.UserInformations.AddAsync(It.IsAny<UserInformation>(), It.IsAny<CancellationToken>()), Times.Never(), "Expected invocation on the mock never.");
            mockedDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never(), "Expected invocation on the mock never.");
        }

        [TestMethod]
        public async Task AddUserInformationIdea_WhenUserInformationToAddIsNotNull_ReturnsUserInformation()
        {
            // Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            var userInfoToAdd = new UserInformationModel { UserId = 1, Id = 1 };
            var user = new User() { Id = 1 };

            mockedDbContext.Setup(context => context.Users).ReturnsDbSet(new List<User>() { user });
            mockedDbContext.Setup(context => context.UserInformations.AddAsync(It.IsAny<UserInformation>(), It.IsAny<CancellationToken>()));
            mockedDbContext.Setup(context => context.SaveChangesAsync(It.IsAny<CancellationToken>()));

            // Act
            await userRepository.AddUserInformation(userInfoToAdd);

            // Assert
            mockedDbContext.Verify(m => m.UserInformations.AddAsync(It.IsAny<UserInformation>(), It.IsAny<CancellationToken>()), Times.Once(), "Expected invocation on the mock once.");
            mockedDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once(), "Expected invocation on the mock once.");
        }



        [TestMethod]
        public async Task AddUserToGroup_WhenUserExistsInGroup_ReturnsFalse()
        {
            //Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            var user = new User { Id = 1 };
            var group = new UsersGroup { Id = 1 };
            group.Users.Add(user);

            mockedDbContext.Setup(context => context.UsersGroups).ReturnsDbSet(new List<UsersGroup>() { group });
            mockedDbContext.Setup(context => context.Users).ReturnsDbSet(new List<User>() { user });

            //Act
            var result = await userRepository.AddUserToGroup(1, 1);

            //Assert
            Assert.IsFalse(result, "Expected false when user exists in group.");
        }

        [TestMethod]
        public async Task AddUserToGroup_WhenUserIdOrGroupIdIsNotPositive_ReturnsFalse()
        {
            //Arrange
            var userRepository = GetUserRepository();

            //Act
            var zeroResult = await userRepository.AddUserToGroup(0, 0);
            var negativeResult = await userRepository.AddUserToGroup(-1, -1);

            //Assert
            Assert.IsFalse(zeroResult, "Expected False when IDs equal to zero.");
            Assert.IsFalse(negativeResult, "Expected False When Ids Are Negative.");

        }

        [TestMethod]
        public async Task AddUserToGroup_WhenUserExistsInGroup_ReturnsTrue()
        {
            //Arrange
            var mockedDbContext = GetMockedDbContext();
            var userRepository = GetUserRepository(mockedDbContext);

            var group = new UsersGroup { Id = 1 };
            var user = new User { Id = 1 };

            mockedDbContext.Setup(context => context.UsersGroups).ReturnsDbSet(new List<UsersGroup>() { group });
            mockedDbContext.Setup(context => context.Users).ReturnsDbSet(new List<User>() { user });
            mockedDbContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()));

            //Act
            var result = await userRepository.AddUserToGroup(1, 1);

            //Assert
            mockedDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once(), "Expected invocation on the mock once.");
            Assert.IsTrue(result, "Expected True When User Dont Exists In Group.");
        }



        private static UserRepository GetUserRepository(Mock<NpomanagementContext> mockedDbContext)
        {
            return new UserRepository(mockedDbContext.Object);
        }

        private static UserRepository GetUserRepository()
        {
            return new UserRepository(null);
        }

        private static Mock<NpomanagementContext> GetMockedDbContext()
        {
            return new Mock<NpomanagementContext>();
        }
    }
}

