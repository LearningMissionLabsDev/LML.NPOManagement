using LML.NPOManagement.Bll.Services;
using LML.NPOManagement.Common;
using LML.NPOManagement.Common.Model;
using LML.NPOManagement.Dal.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BC = BCrypt.Net.BCrypt;

namespace TestProject.BLL.Test
{
    [TestClass]
    public class TestUserService
    {
        [TestMethod]
        public async Task GetAllUsers_WhenUsersCountIsNull_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            userRepositoryMock.Setup(x => x.GetAllUsers()).ReturnsAsync((List<UserModel>)null);
            #endregion

            #region Act
            var result = await userService.GetAllUsers();
            #endregion

            #region Assert
            Assert.IsNull(result, "The result should be null when the users count is null.");
            #endregion
        }

        [TestMethod]
        public async Task GetAllUsers_WhenUsersExist_ReturnsUserModel()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);

            var users = new List<UserModel>()
            {
                new UserModel() { Id = 1, Email = "First@example.com" },
                new UserModel() { Id = 2, Email = "Second@example.com" },
            };

            userRepositoryMock.Setup(x => x.GetAllUsers()).ReturnsAsync(users);
            #endregion

            #region Act
            var result = await userService.GetAllUsers();
            #endregion

            #region Assert
            Assert.IsNotNull(result, "The result should not be null when users exist.");
            Assert.AreEqual(users.Count, result.Count, "The number of users returned should match the number of users in the list.");
            #endregion
        }



        [TestMethod]
        public async Task GetUserById_WhenUserIdIsNotPositive_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            int invalidUserId = -1;
            #endregion

            #region Act
            var result = await userService.GetUserById(invalidUserId);
            #endregion

            #region Assert
            Assert.IsNull(result, "The result should be null when the user ID is not positive.");
            #endregion
        }

        [TestMethod]
        public async Task GetUserById_WhenUserNotFound_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            int userId = 1;

            userRepositoryMock.Setup(x => x.GetUserById(userId)).ReturnsAsync((UserModel)null);
            #endregion

            #region Act
            var result = await userService.GetUserById(userId);
            #endregion

            #region Assert
            Assert.IsNull(result, "Expected result to be null when user is not found.");
            #endregion
        }

        [TestMethod]
        public async Task GetUserById_WhenUserExists_ReturnsUserModel()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            var user = new UserModel { Id = 1, Email = "test@example.com" };

            userRepositoryMock.Setup(x => x.GetUserById(user.Id)).ReturnsAsync(user);
            #endregion

            #region Act
            var result = await userService.GetUserById(user.Id);
            #endregion

            #region Assert
            Assert.IsNotNull(result, "Expected result to be not null when user exists.");
            Assert.AreEqual(user.Id, result.Id, "User IDs should match.");
            Assert.AreEqual(user.Email, result.Email, "User emails should match.");
            #endregion
        }



        [TestMethod]
        public async Task GetUserByEmail_WhenEmailIsEmptyOrNull_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            string emptyEmail = "";
            #endregion

            #region Act
            var result = await userService.GetUserByEmail(emptyEmail);
            #endregion

            #region Assert
            Assert.IsNull(result, "Expected result to be null when email is empty or null.");
            #endregion
        }

        [TestMethod]
        public async Task GetUserByEmail_WhenUserNotFound_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            var user = new UserModel { Id = 1, Email = "test@example.com" };

            userRepositoryMock.Setup(x => x.GetUserByEmail(user.Email)).ReturnsAsync((UserModel)null);
            #endregion

            #region Act
            var result = await userService.GetUserByEmail(user.Email);
            #endregion

            #region Assert
            Assert.IsNull(result, "Expected result to be null when user is not found.");
            #endregion
        }

        [TestMethod]
        public async Task GetUserByEmail_WhenUserExists_ReturnsUserModel()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            var user = new UserModel { Id = 1, Email = "test@example.com" };

            userRepositoryMock.Setup(x => x.GetUserByEmail(user.Email)).ReturnsAsync(user);
            #endregion

            #region Act
            var result = await userService.GetUserByEmail(user.Email);
            #endregion

            #region Assert
            Assert.IsNotNull(result, "Expected result to be not null when user exists.");
            Assert.AreEqual(user.Id, result.Id, "User IDs should match.");
            Assert.AreEqual(user.Email, result.Email, "User emails should match.");
            #endregion
        }



        [TestMethod]
        public async Task GetUsersByInvestorTier_WhenInvestorTierIdIsNotPositive_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            int invalidInvestorTierId = -1;
            #endregion

            #region Act
            var result = await userService.GetUsersByInvestorTier(invalidInvestorTierId);
            #endregion

            #region Assert
            Assert.IsNull(result, "Expected result to be null when investor tier ID is not positive.");
            #endregion
        }

        [TestMethod]
        public async Task GetUsersByInvestorTier_WhenUsersNotFound_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            int investorTierId = 1;
            #endregion

            #region Act
            userRepositoryMock.Setup(x => x.GetUsersByInvestorTier(investorTierId)).ReturnsAsync((List<UserModel>)null);
            var result = await userService.GetUsersByInvestorTier(investorTierId);
            #endregion

            #region Assert
            Assert.IsNull(result, "Expected result to be null when users are not found for the given investor tier ID.");
            #endregion
        }

        [TestMethod]
        public async Task GetUsersByInvestorTier_WhenUsersExist_ReturnsUserModel()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            int investorTierId = 1;

            var users = new List<UserModel>()
            {
                new UserModel() { Id = 1, Email = "test@example.com" },
                new UserModel() { Id = 2, Email = "test1@example.com" },
                new UserModel() { Id = 3, Email = "test2@example.com" }
            };
            userRepositoryMock.Setup(x => x.GetUsersByInvestorTier(investorTierId)).ReturnsAsync(users);
            #endregion

            #region Act
            var result = await userService.GetUsersByInvestorTier(investorTierId);
            #endregion

            #region Assert
            Assert.IsNotNull(result, "Expected result to be not null when users exist for the given investor tier ID.");
            Assert.AreEqual(users.Count, result.Count, "Expected the number of returned users to match the number of users in the list.");
            #endregion
        }



        [TestMethod]
        public async Task GetAllIdeas_WhenUserIdeasCountNull_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            userRepositoryMock.Setup(x => x.GetAllIdeas()).ReturnsAsync((List<UserIdeaModel>)null);
            #endregion

            #region Act
            var result = await userService.GetAllIdeas();
            #endregion

            #region Assert
            Assert.IsNull(result, "Expected result to be null when user ideas count is null.");
            #endregion
        }

        [TestMethod]
        public async Task GetAllIdeas_WhenUserIdeasExist_ReturnsUserIdeaModel()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            var userIdeas = new List<UserIdeaModel>()
            {
                 new UserIdeaModel() { Id = 1, UserId = 1, IdeaCategory = "Test1", IdeaDescription = "Testing" },
                 new UserIdeaModel() { Id = 2, UserId = 2, IdeaCategory = "Test2", IdeaDescription = "Testing" }
            };

            userRepositoryMock.Setup(x => x.GetAllIdeas()).ReturnsAsync(userIdeas);
            #endregion

            #region Act
            var result = await userService.GetAllIdeas();
            #endregion

            #region Assert
            Assert.IsNotNull(result, "Expected result to be not null when user ideas exist.");
            Assert.AreEqual(userIdeas.Count, result.Count, "Check if the count of user ideas matches the expected count.");
            #endregion
        }



        [TestMethod]
        public async Task GetSearchResults_WhenSearchParamIsEmptyOrNull_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            string emptySearchParam = "";
            bool isGroup = false;
            #endregion

            #region Act
            var result = await userService.GetSearchResults(emptySearchParam, isGroup);
            #endregion

            #region Assert
            Assert.IsNull(result, "Result is null for empty or null search parameter.");
            #endregion
        }

        [TestMethod]
        public async Task GetSearchResults_WhenSearchResultsIsNull_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            string searchParam = "Test";
            bool isGroup = true;
            userRepositoryMock.Setup(x => x.GetSearchResults(searchParam, isGroup)).ReturnsAsync((List<SearchModel>)null);
            #endregion

            #region Act
            var result = await userService.GetSearchResults(searchParam, isGroup);
            #endregion

            #region Assert
            Assert.IsNull(result, "Result is null when search results are null.");
            #endregion
        }

        [TestMethod]
        public async Task GetSearchResults_WhenSearchResultsExist_ReturnsSearchModel()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            string searchParam = "Test";
            bool isGroup = true;
            var searchModels = new List<SearchModel>()
            {
                new SearchModel { Id = 1, Name = "Testing1", Type = SearchTypeEnum.Group },
                new SearchModel { Id = 2, Name = "Testing2", Type = SearchTypeEnum.User }
            };

            userRepositoryMock.Setup(x => x.GetSearchResults(searchParam, isGroup)).ReturnsAsync(searchModels);
            #endregion

            #region Act
            var result = await userService.GetSearchResults(searchParam, isGroup);
            #endregion

            #region Assert
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(searchModels, result, "Result should match the expected search model.");
            #endregion
        }



        [TestMethod]
        public async Task GetAllGroups_WhenGroupsCountIsNull_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            userRepositoryMock.Setup(x => x.GetAllGroups()).ReturnsAsync((List<UsersGroupModel>)null);
            #endregion

            #region Act
            var result = await userService.GetAllGroups();
            #endregion

            #region Assert
            Assert.IsNull(result, "Result should be null when groups count is null.");
            #endregion
        }

        [TestMethod]
        public async Task GetAllGroups_WhenGroupsExist_ReturnsUsersGroupModel()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            var usersGroups = new List<UsersGroupModel>()
            {
                new UsersGroupModel() { Id = 1, GroupName = "First", DateCreated = DateTime.UtcNow, CreatorId = 1, StatusId = 2 },
                new UsersGroupModel() { Id = 2, GroupName = "Second", DateCreated = DateTime.UtcNow, CreatorId = 2, StatusId = 2 },
            };

            userRepositoryMock.Setup(x => x.GetAllGroups()).ReturnsAsync(usersGroups);
            #endregion

            #region Act
            var result = await userService.GetAllGroups();
            #endregion

            #region Assert
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(usersGroups.Count, result.Count, "Result should contain the same number of groups as usersGroups.");
            #endregion
        }



        [TestMethod]
        public async Task GetGroupById_WhenGroupIdIsNotPositive_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            int negativeGroupId = -1;
            #endregion

            #region Act
            var result = await userService.GetGroupById(negativeGroupId);
            #endregion

            #region Assert
            Assert.IsNull(result, "Result should be null when group ID is not positive.");
            #endregion
        }

        [TestMethod]
        public async Task GetGroupById_WhenGroupNotFound_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            int nonExistingGroupId = 1;

            userRepositoryMock.Setup(x => x.GetGroupById(nonExistingGroupId)).ReturnsAsync((UsersGroupModel)null);
            #endregion

            #region Act
            var result = await userService.GetGroupById(nonExistingGroupId);
            #endregion

            #region Assert
            Assert.IsNull(result, "Result should be null when group is not found.");
            #endregion
        }

        [TestMethod]
        public async Task GetGroupById_WhenGroupExists_ReturnsUsersGroupModel()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            var usersGroup = new UsersGroupModel() { Id = 1, GroupName = "First", DateCreated = DateTime.UtcNow, CreatorId = 1, StatusId = 2 };

            userRepositoryMock.Setup(x => x.GetGroupById(usersGroup.Id)).ReturnsAsync(usersGroup);
            #endregion

            #region Act
            var result = await userService.GetGroupById(usersGroup.Id);
            #endregion

            #region Assert
            Assert.IsNotNull(result, "Result should not be null when group is found.");
            Assert.AreEqual(usersGroup, result, "Retrieved group should match the expected group.");
            #endregion
        }



        [TestMethod]
        public async Task GetGroupsByGroupName_WhenGroupNameIsEmptyOrNull_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            string emptyGroupName = "";
            #endregion

            #region Act
            var result = await userService.GetGroupsByName(emptyGroupName);
            #endregion

            #region Assert
            Assert.IsNull(result, "The result should be null when the group name is empty or null.");
            #endregion
        }

        [TestMethod]
        public async Task GetGroupsByGroupName_WhenGroupsNotFound_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            string nonExistingGroupName = "Test";

            userRepositoryMock.Setup(x => x.GetGroupsByName(nonExistingGroupName)).ReturnsAsync((List<UsersGroupModel>)null);
            #endregion

            #region Act
            var result = await userService.GetGroupsByName(nonExistingGroupName);
            #endregion

            #region Assert
            Assert.IsNull(result, "Result should be null when groups are not found.");
            #endregion
        }

        [TestMethod]
        public async Task GetGroupsByGroupName_WhenGroupsExist_ReturnsUsersGroupModel()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            string existingGroupName = "Test";

            var usersGroups = new List<UsersGroupModel>()
            {
                new UsersGroupModel() { Id = 1, GroupName = "Test1", DateCreated = DateTime.UtcNow, CreatorId = 1, StatusId = 2 },
                new UsersGroupModel() { Id = 2, GroupName = "Test2", DateCreated = DateTime.UtcNow, CreatorId = 2, StatusId = 2 },
            };

            userRepositoryMock.Setup(x => x.GetGroupsByName(existingGroupName)).ReturnsAsync(usersGroups);
            #endregion

            #region Act
            var result = await userService.GetGroupsByName(existingGroupName);
            #endregion

            #region Assert
            Assert.IsNotNull(result, "The result should not be null when groups exist for the specified group name.");
            Assert.AreEqual(usersGroups, result, "The retrieved groups should match the expected list of groups.");
            #endregion
        }



        [TestMethod]
        public async Task GetUsersByGroupId_WhenGroupIdIsNotPositive_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            int negativeGroupId = -1;
            #endregion

            #region Act
            var result = await userService.GetUsersByGroupId(negativeGroupId);
            #endregion

            #region Assert
            Assert.IsNull(result, "The result should be null when the group ID is not positive.");
            #endregion
        }

        [TestMethod]
        public async Task GetUsersByGroupId_WhenGroupNotFound_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            int nonExistingGroupId = 1;

            userRepositoryMock.Setup(x => x.GetUsersByGroupId(nonExistingGroupId)).ReturnsAsync((List<UserModel>)null);
            #endregion

            #region Act
            var result = await userService.GetUsersByGroupId(nonExistingGroupId);
            #endregion

            #region Assert
            Assert.IsNull(result, "The result should be null when the group is not found.");
            #endregion
        }

        [TestMethod]
        public async Task GetUsersByGroupId_WhenUsersInGroupExist_ReturnsUserModel()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);

            var usersGroup = new UsersGroupModel() { Id = 1, GroupName = "First", DateCreated = DateTime.UtcNow, CreatorId = 1, StatusId = 2 };
            usersGroup.Users.Add(new UserModel() { Id = 1, Email = "test@example.com" });
            usersGroup.Users.Add(new UserModel() { Id = 2, Email = "test2@example.com" });
            usersGroup.Users.Add(new UserModel() { Id = 3, Email = "test3@example.com" });
            userRepositoryMock.Setup(x => x.GetUsersByGroupId(usersGroup.Id)).ReturnsAsync(usersGroup.Users.ToList());
            #endregion

            #region Act
            var result = await userService.GetUsersByGroupId(usersGroup.Id);
            #endregion

            #region Assert
            Assert.IsNotNull(result, "The result should not be null when users in the group exist.");
            Assert.AreEqual(usersGroup.Users.Count, result.Count, "The count of users in the result should match the count of users in the group.");
            #endregion
        }



        [TestMethod]
        public async Task GetGroupsForUser_WhenUserIdIsNotPositive_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            int negativeUserId = -1;
            #endregion

            #region Act
            var result = await userService.GetGroupsForUser(negativeUserId);
            #endregion

            #region Assert
            Assert.IsNull(result, "The result should be null when the user ID is not positive.");
            #endregion
        }

        [TestMethod]
        public async Task GetGroupsForUser_WhenGroupsNotFound_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            int userId = 1;

            userRepositoryMock.Setup(x => x.GetGroupsForUser(userId)).ReturnsAsync((List<UsersGroupModel>)null);
            #endregion

            #region Act
            var result = await userService.GetGroupsForUser(userId);
            #endregion

            #region Assert
            Assert.IsNull(result, "The result should be null when no groups are found for the user.");
            #endregion
        }

        [TestMethod]
        public async Task GetGroupsForUser_WhenUserIncludeGroups_ReturnsUsersGroupModel()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);

            var usersGroups = new List<UsersGroupModel>()
            {
                new UsersGroupModel() { Id = 1, GroupName = "Test1" },
                new UsersGroupModel() { Id = 2, GroupName = "Test2" },
            };
            usersGroups.First().Users.Add(new UserModel() { Id = 1, Email = "test@example.com" });
            usersGroups.Last().Users.Add(new UserModel() { Id = 1, Email = "test@example.com" });
            userRepositoryMock.Setup(x => x.GetGroupsForUser(1)).ReturnsAsync(usersGroups);
            #endregion

            #region Act
            var result = await userService.GetGroupsForUser(1);
            #endregion

            #region Assert
            Assert.IsNotNull(result, "The result should not be null when user groups are found.");
            Assert.AreEqual(usersGroups.Count, result.Count, "The count of user groups should match the expected count.");
            #endregion
        }




        [TestMethod]
        public async Task UpdateUserStatus_WhenUserIdIsNotPositive_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            int negativeUserId = -1;
            #endregion

            #region Act
            var result = await userService.DeleteUser(negativeUserId);
            #endregion

            #region Assert
            Assert.IsNull(result, "Result should be null when userId is not positive.");
            #endregion
        }

        [TestMethod]
        public async Task UpdateUserStatus_WhenUserNotFound_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            int userId = 1;

            userRepositoryMock.Setup(x => x.GetUserById(userId)).ReturnsAsync((UserModel)null);
            #endregion

            #region Act
            var result = await userService.DeleteUser(userId);
            #endregion

            #region Assert
            Assert.IsNull(result, "Result should be null when user is not found.");
            #endregion
        }

        [TestMethod]
        public async Task UpdateUserStatus_WhenUserIsDeleted_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            int userId = 1;
            var deletedUser = new UserModel() { Id = userId, StatusId = (int)StatusEnumModel.Deleted };

            userRepositoryMock.Setup(x => x.GetUserById(userId)).ReturnsAsync(deletedUser);
            #endregion

            #region Act
            var result = await userService.DeleteUser(userId);
            #endregion

            #region Assert
            Assert.IsNull(result, "Result should be null when user is already deleted.");
            #endregion
        }

        [TestMethod]
        public async Task UpdateUserStatus_WhenUserIsUpdated_ReturnsUpdatedUserModel()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            int userId = 1;
            var user = new UserModel() { Id = userId, StatusId = (int)StatusEnumModel.Active };
            var updatedUser = new UserModel() { Id = userId, StatusId = (int)StatusEnumModel.Deleted };

            userRepositoryMock.Setup(x => x.GetUserById(userId)).ReturnsAsync(user);
            userRepositoryMock.Setup(x => x.UpdateUserStatus(userId, StatusEnumModel.Deleted)).ReturnsAsync(updatedUser);
            #endregion

            #region Act
            var result = await userService.DeleteUser(userId);
            #endregion

            #region Assert
            Assert.IsNotNull(result, "Result should not be null when user status is successfully updated.");
            Assert.AreEqual(updatedUser, result, "Result should match the updated user model.");
            #endregion
        }




        [TestMethod]
        public async Task DeleteUserFromGroup_WhenUserIdIsNotPositive_ReturnsFalse()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            int negativeUserId = -1;
            int groupId = 1;
            #endregion

            #region Act
            var result = await userService.DeleteUserFromGroup(negativeUserId, groupId);
            #endregion

            #region Assert
            Assert.IsFalse(result, "Result should be false when userId is not positive.");
            #endregion
        }

        [TestMethod]
        public async Task DeleteUserFromGroup_WhenGroupIdIsNotPositive_ReturnsFalse()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            int userId = 1;
            int negativeGroupId = -1;
            #endregion

            #region Act
            var result = await userService.DeleteUserFromGroup(userId, negativeGroupId);
            #endregion

            #region Assert
            Assert.IsFalse(result, "Result should be false when groupId is not positive.");
            #endregion
        }

        [TestMethod]
        public async Task DeleteUserFromGroup_WhenUserNotFound_ReturnsFalse()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            int userId = 1;
            int groupId = 1;

            userRepositoryMock.Setup(x => x.GetUserById(userId)).ReturnsAsync((UserModel)null);
            #endregion

            #region Act
            var result = await userService.DeleteUserFromGroup(userId, groupId);
            #endregion

            #region Assert
            Assert.IsFalse(result, "Result should be false when user is not found.");
            #endregion
        }

        [TestMethod]
        public async Task DeleteUserFromGroup_WhenGroupNotFound_ReturnsFalse()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            int userId = 1;
            int groupId = 1;
            var user = new UserModel() { Id = userId };

            userRepositoryMock.Setup(x => x.GetUserById(userId)).ReturnsAsync(user);
            userRepositoryMock.Setup(x => x.GetGroupById(groupId)).ReturnsAsync((UsersGroupModel)null);
            #endregion

            #region Act
            var result = await userService.DeleteUserFromGroup(userId, groupId);
            #endregion

            #region Assert
            Assert.IsFalse(result, "Result should be false when group is not found.");
            #endregion
        }

        [TestMethod]
        public async Task DeleteUserFromGroup_WhenUserAndGroupFoundButDeletionFailed_ReturnsFalse()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            int userId = 1;
            int groupId = 1;
            var user = new UserModel() { Id = userId };
            var group = new UsersGroupModel() { Id = groupId };

            userRepositoryMock.Setup(x => x.GetUserById(userId)).ReturnsAsync(user);
            userRepositoryMock.Setup(x => x.GetGroupById(groupId)).ReturnsAsync(group);
            userRepositoryMock.Setup(x => x.DeleteUserFromGroup(userId, groupId)).ReturnsAsync((UsersGroupModel)null);
            #endregion

            #region Act
            var result = await userService.DeleteUserFromGroup(userId, groupId);
            #endregion

            #region Assert
            Assert.IsFalse(result, "Result should be false when deletion of user from group fails.");
            #endregion
        }

        [TestMethod]
        public async Task DeleteUserFromGroup_WhenUserAndGroupFoundAndDeletionSuccessful_ReturnsTrue()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            int userId = 1;
            int groupId = 1;
            var user = new UserModel() { Id = userId };
            var group = new UsersGroupModel() { Id = groupId };

            userRepositoryMock.Setup(x => x.GetUserById(userId)).ReturnsAsync(user);
            userRepositoryMock.Setup(x => x.GetGroupById(groupId)).ReturnsAsync(group);
            userRepositoryMock.Setup(x => x.DeleteUserFromGroup(userId, groupId)).ReturnsAsync(group);
            #endregion

            #region Act
            var result = await userService.DeleteUserFromGroup(userId, groupId);
            #endregion

            #region Assert
            Assert.IsTrue(result, "User deletion from group should return true when the deletion is successful.");
            #endregion
        }




        [TestMethod]
        public async Task DeleteGroup_WhenGroupIdIsNotPositive_ReturnsFalse()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            int negativeGroupId = -1;
            #endregion

            #region Act
            var result = await userService.DeleteGroup(negativeGroupId);
            #endregion

            #region Assert
            Assert.IsFalse(result, "Deleting a group with a non-positive group ID should return false.");
            #endregion
        }

        [TestMethod]
        public async Task DeleteGroup_WhenGroupNotFound_ReturnsFalse()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            int groupId = 1;

            userRepositoryMock.Setup(x => x.DeleteGroup(groupId)).ReturnsAsync(false);
            #endregion

            #region Act
            var result = await userService.DeleteGroup(groupId);
            #endregion

            #region Assert
            Assert.IsFalse(result, "Deleting a group that does not exist should return false.");
            #endregion
        }

        [TestMethod]
        public async Task DeleteGroup_WhenGroupDeletedSuccessfully_ReturnsTrue()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            int groupId = 1;

            userRepositoryMock.Setup(x => x.DeleteGroup(groupId)).ReturnsAsync(true);
            #endregion

            #region Act
            var result = await userService.DeleteGroup(groupId);
            #endregion

            #region Assert
            Assert.IsTrue(result, "Deleting an existing group should return true.");
            #endregion
        }



        [TestMethod]
        public async Task ActivationUser_WhenTokenIsEmptyOrNull_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            string emptyToken = "";
            var configurationMock = GetConfigurationMock("your_secret_key", 60);
            #endregion

            #region Act
            var result = await userService.ActivationUser(emptyToken, configurationMock.Object);
            #endregion

            #region Assert
            Assert.IsNull(result, "Activating a user with an empty or null token should return null.");
            #endregion
        }

        [TestMethod]
        public async Task ActivationUser_WhenTokenIsValidButUserValidationFails_ReturnsNull()
        {
            #region Arrange
            var userService = new UserService(null);
            var configurationMock = GetConfigurationMock("MER DEM XAX CHKA", 20);
            var userToAdd = new UserModel() { Id = 1, Email = "test@example.com", StatusId = (int)StatusEnumModel.Pending };
            string token = TokenCreationHelper.GenerateJwtToken(userToAdd, configurationMock.Object, null);

            configurationMock.Setup(x => x.GetSection("AppSettings:TokenExpiration").Value).Returns("20");
            #endregion

            #region Act
            var result = await userService.ActivationUser(token, configurationMock.Object);
            #endregion

            #region Assert
            Assert.IsNull(result, "Activating a user with a valid token but failed user validation should return null.");
            #endregion
        }

        [TestMethod]
        public async Task ActivationUser_WhenTokenIsInvalid_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            var configurationMock = GetConfigurationMock("MER DEM XAX CHKA", 20);
            string invalidToken = "invalid_token";

            configurationMock.Setup(x => x.GetSection("AppSettings:TokenExpiration").Value).Returns("20");
            #endregion

            #region Act
            var result = await userService.ActivationUser(invalidToken, configurationMock.Object);
            #endregion

            #region Assert
            Assert.IsNull(result, "Activating a user with an invalid token should return null.");
            #endregion
        }

        [TestMethod]
        public async Task ActivationUser_WhenTokenIsValidAndUserValidationSuccess_ActivatesUser()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            var configurationMock = GetConfigurationMock("MER DEM XAX CHKA", 20);
            var userToActivate = new UserModel() { Id = 1, Email = "test@example.com", StatusId = (int)StatusEnumModel.Pending };
            var activatedUser = new UserModel() { Id = 1, Email = "test@example.com", StatusId = (int)StatusEnumModel.Active };

            string token = TokenCreationHelper.GenerateJwtToken(userToActivate, configurationMock.Object, userRepositoryMock.Object);
            configurationMock.Setup(x => x.GetSection("AppSettings:TokenExpiration").Value).Returns("20");
            #endregion

            #region Act
            var result = await userService.ActivationUser(token, configurationMock.Object);
            #endregion

            #region Assert
            Assert.IsNotNull(result, "Activating a user with a valid token and successful user validation should return the activated user.");
            Assert.AreEqual(activatedUser.Id, result.Id, "The user id of the activated user should match the expected user id.");
            Assert.AreEqual(activatedUser.StatusId, result.StatusId, "The user status should be set to Active after successful activation.");
            #endregion
        }




        [TestMethod]
        public async Task AddUserIdea_WhenIdeaIsNull_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            #endregion

            #region Act
            var result = await userService.AddUserIdea(null);
            #endregion

            #region Assert
            Assert.IsNull(result, "Adding an idea with null or empty description should return null.");
            #endregion
        }

        [TestMethod]
        public async Task AddUserIdea_WhenIdeaNotAdded_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            userRepositoryMock.Setup(x => x.AddUserIdea(It.IsAny<UserIdeaModel>())).ReturnsAsync((UserIdeaModel)null);
            #endregion

            #region Act
            var result = await userService.AddUserIdea(null);
            #endregion

            #region Assert
            Assert.IsNull(result, "When the idea is not added, the method should return null.");
            #endregion
        }

        [TestMethod]
        public async Task AddUserIdea_WhenIdeaAddedSuccessfully_ReturnsIdea()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            var userIdeaToAdd = new UserIdeaModel { UserId = 2, IdeaCategory = "Test", IdeaDescription = "Test Desciption" };
            var addedUserIdea = new UserIdeaModel { Id = 1, UserId = 2, IdeaCategory = "Test", IdeaDescription = "Test Desciption" };

            userRepositoryMock.Setup(x => x.AddUserIdea(userIdeaToAdd)).ReturnsAsync(addedUserIdea);
            #endregion

            #region Act
            var result = await userService.AddUserIdea(userIdeaToAdd);
            #endregion

            #region Assert
            Assert.IsNotNull(result, "The added idea should not be null.");
            Assert.AreEqual(addedUserIdea.Id, result.Id, "The returned idea ID should match the added idea ID.");
            Assert.AreEqual(addedUserIdea.UserId, result.UserId, "The returned user ID should match the added user ID.");
            Assert.AreEqual(addedUserIdea.IdeaCategory, result.IdeaCategory, "The returned idea category should match the added idea category.");
            #endregion
        }



        [TestMethod]
        public async Task AddUserToGroup_WithValidIds_AddsUserToGroup()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            int validUserId = 1;
            int validGroupId = 1;

            userRepositoryMock.Setup(repo => repo.AddUserToGroup(validUserId, validGroupId)).ReturnsAsync(true);
            #endregion

            #region Act
            var result = await userService.AddUserToGroup(validUserId, validGroupId);
            #endregion

            #region Assert
            Assert.IsTrue(result, "Adding a user to a group with valid user and group IDs should return true.");
            #endregion
        }

        [TestMethod]
        public async Task AddUserToGroup_WithInvalidUserId_ReturnsFalse()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            int negativeUserId = -1;
            int validGroupId = 1;
            #endregion

            #region Act
            var result = await userService.AddUserToGroup(negativeUserId, validGroupId);
            #endregion

            #region Assert
            Assert.IsFalse(result, "Adding a user to a group with an invalid user ID should return false.");
            #endregion
        }

        [TestMethod]
        public async Task AddUserToGroup_WithInvalidGroupId_ReturnsFalse()
        {
            #region Arrange
            int validUserId = 1;
            int negativeGroupId = -1;
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            #endregion

            #region Act
            var result = await userService.AddUserToGroup(validUserId, negativeGroupId);
            #endregion

            #region Assert
            Assert.IsFalse(result, "Adding a user to a group with an invalid group ID should return false.");
            #endregion
        }

        [TestMethod]
        public async Task AddUserToGroup_OperationFails_ReturnsFalse()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            int userId = 1;
            int groupId = 1;

            userRepositoryMock.Setup(repo => repo.AddUserToGroup(userId, groupId)).ReturnsAsync(false);
            #endregion

            #region Act
            var result = await userService.AddUserToGroup(userId, groupId);
            #endregion

            #region Assert
            Assert.IsFalse(result, "Adding a user to a group where the operation fails should return false.");
            #endregion
        }



        [TestMethod]
        public async Task CreateGroup_WhenGroupCreatedSuccessfully_CreatesGroup()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            var usersGroupToAdd = new UsersGroupModel { GroupName = "Test Group", CreatorId = 1, UserIds = new List<int>() { 1 } };
            var addedUsersGroup = new UsersGroupModel { Id = 1, GroupName = "Test Group", CreatorId = 1, UserIds = new List<int>() { 1 } };

            userRepositoryMock.Setup(repo => repo.GetUserById(usersGroupToAdd.CreatorId)).ReturnsAsync(new UserModel { Id = 1 });
            userRepositoryMock.Setup(repo => repo.AddGroup(usersGroupToAdd)).ReturnsAsync(addedUsersGroup);
            #endregion

            #region Act
            var result = await userService.CreateGroup(usersGroupToAdd);
            #endregion

            #region Assert
            Assert.IsNotNull(result, "Creating a group with a valid model should return the created group.");
            Assert.AreEqual(addedUsersGroup.Id, result.Id, "The group ID of the created group should match the expected group ID.");
            Assert.AreEqual(addedUsersGroup.GroupName, result.GroupName, "The group name of the created group should match the expected group name.");
            Assert.AreEqual(addedUsersGroup.CreatorId, result.CreatorId, "The creator ID of the created group should match the expected creator ID.");
            CollectionAssert.AreEqual(addedUsersGroup.UserIds, result.UserIds, "The user IDs of the created group should match the expected user IDs.");
            #endregion
        }

        [TestMethod]
        public async Task CreateGroup_WithNullOrEmptyGroupName_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            var usersGroupToAdd = new UsersGroupModel { GroupName = "", CreatorId = 1, UserIds = new List<int>() { 1 } };
            #endregion

            #region Act
            var result = await userService.CreateGroup(usersGroupToAdd);
            #endregion

            #region Assert
            Assert.IsNull(result, "Creating a group with a null or empty group name should return null.");
            #endregion
        }

        [TestMethod]
        public async Task CreateGroup_CreatorUserNotFound_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            var usersGroupToAdd = new UsersGroupModel { GroupName = "Test Group", CreatorId = 1, UserIds = new List<int>() { 1 } };

            userRepositoryMock.Setup(repo => repo.GetUserById(usersGroupToAdd.CreatorId)).ReturnsAsync((UserModel)null); // Simulating user not found
            #endregion

            #region Act
            var result = await userService.CreateGroup(usersGroupToAdd);
            #endregion

            #region Assert
            Assert.IsNull(result, "Creating a group with a non-existent creator user should return null.");
            #endregion
        }

        [TestMethod]
        public async Task CreateGroup_WithNonPositiveCreatorId_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            var usersGroupToAdd = new UsersGroupModel { GroupName = "Test Group", CreatorId = -1, UserIds = new List<int>() { 1 } };
            #endregion

            #region Act
            var result = await userService.CreateGroup(usersGroupToAdd);
            #endregion

            #region Assert
            Assert.IsNull(result, "Creating a group with a non-positive creatorId should return null.");
            #endregion
        }

        [TestMethod]
        public async Task CreateGroup_WhenOperationFails_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            var usersGroupToAdd = new UsersGroupModel { GroupName = "Test Group", CreatorId = 1, UserIds = new List<int>() { 1 } };

            userRepositoryMock.Setup(repo => repo.GetUserById(usersGroupToAdd.CreatorId)).ReturnsAsync(new UserModel { Id = 1 }); // Simulating user exists
            userRepositoryMock.Setup(repo => repo.AddGroup(usersGroupToAdd)).ReturnsAsync((UsersGroupModel)null); // Simulating operation failure
            #endregion

            #region Act
            var result = await userService.CreateGroup(usersGroupToAdd);
            #endregion

            #region Assert
            Assert.IsNull(result, "Creating a group where the operation fails should return null.");
            #endregion
        }



        [TestMethod]
        public async Task UserInformationRegistration_WhenUserInfoAddedSuccessfully_RegistersUserInformation()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            var configurationMock = GetConfigurationMock("secret key", 60);
            var userInformation = new UserInformationModel { Id = 1 };

            userRepositoryMock.Setup(repo => repo.AddUserInformation(userInformation));
            #endregion

            #region Act
            var result = await userService.UserInformationRegistration(userInformation, configurationMock.Object);
            #endregion

            #region Assert
            Assert.IsNotNull(result, "The result should not be null.");
            Assert.AreEqual(userInformation.Id, result, "The returned user information ID should match the provided user information ID.");
            #endregion
        }

        [TestMethod]
        public async Task UserInformationRegistration_WhenUserInfoModelIsNull_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            var configurationMock = GetConfigurationMock("secret key", 60);
            #endregion

            #region Act
            var result = await userService.UserInformationRegistration(null, configurationMock.Object);
            #endregion

            #region Assert
            Assert.IsNull(result, "The result should be null when userInformationModel is null.");
            #endregion
        }


        [TestMethod]
        public async Task Registration_WithNewUser_SuccessfullyRegistersUser()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var configurationMock = GetConfigurationMock("MER DEM XAX CHKA", 20);
            var userService = GetUserService(userRepositoryMock);
            var userModel = new UserModel { Email = "new@example.com", Password = "password" };
            var newUser = new UserModel { Id = 1, Email = "new@example.com", Password = BC.HashPassword("password") };

            userRepositoryMock.SetupSequence(repo => repo.GetUserByEmail(userModel.Email))
                              .ReturnsAsync((UserModel)null)  // First invocation returns null
                              .ReturnsAsync(newUser);        // Second invocation returns valid user model

            userRepositoryMock.Setup(repo => repo.AddUser(newUser));
            configurationMock.Setup(x => x.GetSection("AppSettings:TokenExpiration").Value).Returns("20");
            #endregion

            #region Act
            var result = await userService.Registration(userModel, configurationMock.Object);
            #endregion

            #region Assert
            Assert.IsNotNull(result, "The result should not be null.");
            Assert.AreEqual(newUser.Email, result.Email, "The email of the returned user should match the email of the added user.");
            Assert.IsNotNull(result.Token, "The returned user should have a token.");
            #endregion
        }

        [TestMethod]
        public async Task Registration_WithExistingUser_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            var userModel = new UserModel { Email = "existing@example.com", Password = "password" };
            var existingUser = new UserModel { Id = 1, Email = "existing@example.com", Password = BC.HashPassword("password") };

            userRepositoryMock.Setup(repo => repo.GetUserByEmail(userModel.Email)).ReturnsAsync(existingUser);
            #endregion

            #region Act
            var result = await userService.Registration(userModel, null);
            #endregion

            #region Assert
            Assert.IsNull(result, "Registration should return null when the user already exists.");
            #endregion
        }

        [TestMethod]
        public async Task Registration_WithNullUserModel_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            var configurationMock = GetConfigurationMock("secret key", 60);
            #endregion

            #region Act
            var result = await userService.Registration(null, configurationMock.Object);
            #endregion

            #region Assert
            Assert.IsNull(result, "Registration should return null when the user model is null.");
            #endregion
        }



        [TestMethod]
        public async Task ModifyUserCredentials_WithValidData_ModifiesUserCredentials()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            var userId = 1;
            var email = "newemail@example.com";
            var password = "newPassword";

            var existingUser = new UserModel { Id = userId, Email = "oldemail@example.com", Password = "oldPassword", StatusId = (int)StatusEnumModel.Active };
            var modifiedUser = new UserModel { Id = userId, Email = email, Password = password, StatusId = (int)StatusEnumModel.Pending };

            userRepositoryMock.Setup(repo => repo.GetUserByEmail(email)).ReturnsAsync((UserModel)null);
            userRepositoryMock.Setup(repo => repo.GetUserById(userId)).ReturnsAsync(existingUser);
            userRepositoryMock.Setup(repo => repo.ModifyUserCredentials(email, password, userId, (int)StatusEnumModel.Pending)).ReturnsAsync(modifiedUser);
            #endregion

            #region Act
            var result = await userService.ModifyUserCredentials(email, password, userId);
            #endregion

            #region Assert
            Assert.IsNotNull(result, "The result should not be null.");
            Assert.AreEqual(email, result.Email, "The email of the returned user should match the modified email.");
            Assert.AreEqual((int)StatusEnumModel.Pending, result.StatusId, "The status ID of the returned user should be Pending.");
            Assert.IsNull(result.Password, "The password of the returned user should be null.");
            #endregion
        }

        [TestMethod]
        public async Task ModifyUserCredentials_WithInvalidUserId_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            var negativeUserId = -1;
            var email = "newemail@example.com";
            var password = "newPassword";
            #endregion

            #region Act
            var result = await userService.ModifyUserCredentials(email, password, negativeUserId);
            #endregion

            #region Assert
            Assert.IsNull(result, "Modifying user credentials with an invalid user ID should return null.");
            #endregion
        }

        [TestMethod]
        public async Task ModifyUserCredentials_WithNullOrEmptyEmail_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            var emptyEmail = "";
            var password = "newPassword";
            var userId = 1;
            #endregion

            #region Act
            var result = await userService.ModifyUserCredentials(emptyEmail, password, userId);
            #endregion

            #region Assert
            Assert.IsNull(result, "Modifying user credentials with an empty email should return null.");
            #endregion
        }

        [TestMethod]
        public async Task ModifyUserCredentials_WithNullOrEmptyPassword_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            var email = "test@example.com";
            var emptyPassword = "";
            var userId = 1;
            #endregion

            #region Act
            var result = await userService.ModifyUserCredentials(email, emptyPassword, userId);
            #endregion

            #region Assert
            Assert.IsNull(result, "Modifying user credentials with an empty password should return null.");
            #endregion
        }

        [TestMethod]
        public async Task ModifyUserCredentials_WithExistingEmail_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            var userId = 1;
            var existingEmail = "existingemail@example.com";
            var password = "existingPassword";

            userRepositoryMock.Setup(repo => repo.GetUserByEmail(existingEmail)).ReturnsAsync(new UserModel { Id = 2, Email = existingEmail, Password = password, StatusId = (int)StatusEnumModel.Active });
            #endregion

            #region Act
            var result = await userService.ModifyUserCredentials(existingEmail, password, userId);
            #endregion

            #region Assert
            Assert.IsNull(result, "Modifying user credentials with an email that already exists should return null.");
            #endregion
        }

        [TestMethod]
        public async Task ModifyUserCredentials_WithValidUserIdButUserNotFound_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var userService = new UserService(userRepositoryMock.Object);
            var userId = 1;
            var email = "newemail@example.com";
            var password = "newPassword";

            userRepositoryMock.Setup(repo => repo.GetUserById(userId)).ReturnsAsync((UserModel)null);
            #endregion

            #region Act
            var result = await userService.ModifyUserCredentials(email, password, userId);
            #endregion

            #region Assert
            Assert.IsNull(result, "Modifying user credentials with a valid user ID but user not found should return null.");
            #endregion
        }



        [TestMethod]
        public async Task ModifyUserInfo_WithNullModel_ReturnsFalse()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            #endregion

            #region Act
            var result = await userService.ModifyUserInfo(null);
            #endregion

            #region Assert
            Assert.IsFalse(result, "Modifying user information with a null model should return false.");
            #endregion
        }



        [TestMethod]
        public async Task Login_WithValidCredentials_ReturnsUserModelWithToken()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            string password = BC.HashPassword("testpassword");
            var userModel = new UserModel { Email = "test@example.com", Password = "testpassword" };
            var userInDatabase = new UserModel { Id = 1, Email = "test@example.com", Password = password };

            var account2users = new List<Account2UserModel> {
               new Account2UserModel{ UserId = 1, AccountId = 1, AccountRoleId = 1 },
               new Account2UserModel{ UserId = 2, AccountId = 2, AccountRoleId = 2 },
               new Account2UserModel{ UserId = 3, AccountId = 3, AccountRoleId = 3 },
            };

            var configurationMock = GetConfigurationMock("MER DEM XAX CHKA", 20);
            configurationMock.Setup(x => x.GetSection("AppSettings:TokenExpiration").Value).Returns("20");
            userRepositoryMock.Setup(repo => repo.GetUserByEmail(userModel.Email)).ReturnsAsync(userInDatabase);
            userRepositoryMock.Setup(repo => repo.GetUsersInfoAccount(userInDatabase.Id)).ReturnsAsync(account2users);
            #endregion

            #region Act
            var result = await userService.Login(userModel, configurationMock.Object);
            #endregion

            #region Assert
            Assert.IsNotNull(result, "Logging in with valid credentials should return a UserModel.");
            Assert.AreEqual(userInDatabase.Id, result.Id, "The ID of the returned user should match the ID in the database.");
            Assert.IsNull(result.Password, "The password of the returned user should be null.");
            Assert.IsNotNull(result.Token, "The token of the returned user should not be null.");
            #endregion
        }

        [TestMethod]
        public async Task Login_WithNonExistentUser_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            var userModel = new UserModel { Email = "nonexistent@example.com", Password = "testpassword" };
            userRepositoryMock.Setup(repo => repo.GetUserByEmail(userModel.Email)).ReturnsAsync((UserModel)null);
            #endregion

            #region Act
            var result = await userService.Login(userModel, null);
            #endregion

            #region Assert
            Assert.IsNull(result, "Logging in with a non-existent user should return null.");
            #endregion
        }

        [TestMethod]
        public async Task Login_WithIncorrectPassword_ReturnsNull()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            string correctPassword = BC.HashPassword("correctpassword");
            var userModel = new UserModel { Email = "test@example.com", Password = "incorrectpassword" };
            var userInDatabase = new UserModel { Id = 1, Email = "test@example.com", Password = correctPassword };

            userRepositoryMock.Setup(repo => repo.GetUserByEmail(userModel.Email)).ReturnsAsync(userInDatabase);
            #endregion

            #region Act
            var result = await userService.Login(userModel, null);
            #endregion

            #region Assert
            Assert.IsNull(result, "Logging in with an incorrect password should return null.");
            #endregion
        }

        [TestMethod]
        public async Task Login_WithNullAccounts_ReturnsUserModelWithoutAccounts()
        {
            #region Arrange
            var userRepositoryMock = GetUserRepository();
            var userService = GetUserService(userRepositoryMock);
            string correctPassword = BC.HashPassword("correctpassword");

            var userModel = new UserModel { Email = "test@example.com", Password = "correctpassword" };
            var userInDatabase = new UserModel { Id = 1, Email = "test@example.com", Password = correctPassword };
            var configurationMock = GetConfigurationMock("MER DEM XAX CHKA", 20);

            configurationMock.Setup(x => x.GetSection("AppSettings:TokenExpiration").Value).Returns("20");
            userRepositoryMock.Setup(repo => repo.GetUserByEmail(userModel.Email)).ReturnsAsync(userInDatabase);
            userRepositoryMock.Setup(repo => repo.GetUsersInfoAccount(userInDatabase.Id)).ReturnsAsync((List<Account2UserModel>)null);
            #endregion

            #region Act
            var result = await userService.Login(userModel, configurationMock.Object);
            #endregion

            #region Assert
            Assert.IsNotNull(result, "Logging in with valid credentials should return a non-null UserModel.");
            Assert.AreEqual(0, result.Account2Users.Count, "The number of accounts associated with the user should be zero.");
            #endregion
        }



        private static UserService GetUserService(Mock<IUserRepository> mockedUserRepo)
        {
            return new UserService(mockedUserRepo.Object);
        }

        private static Mock<IUserRepository> GetUserRepository()
        {
            return new Mock<IUserRepository>();
        }

        private static Mock<IConfiguration> GetConfigurationMock(string secretKey, int tokenExpiration)
        {
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(x => x.GetSection("AppSettings:SecretKey").Value).Returns(secretKey);
            configurationMock.Setup(x => x.GetSection("AppSettings:TokenExpiration").Value).Returns(tokenExpiration.ToString());
            return configurationMock;
        }
    }
}
