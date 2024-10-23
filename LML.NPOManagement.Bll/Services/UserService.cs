using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Shared;
using LML.NPOManagement.Common;
using LML.NPOManagement.Common.Model;
using LML.NPOManagement.Dal.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using BC = BCrypt.Net.BCrypt;

namespace LML.NPOManagement.Bll.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<ServiceResult<UserModel>> ActivationUser(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                ServiceResult<UserModel>.Failure("Invalid Token.", ServiceStatusCode.Unauthorized);
            }

            var user = await TokenCreationHelper.ValidateJwtToken(token, _configuration, _userRepository);
            if (user == null)
            {
               return ServiceResult<UserModel>.Failure("Invalid Token.", ServiceStatusCode.Unauthorized);
            }

            await _userRepository.UpdateUserStatus(user.Id, StatusEnumModel.Active);

            var newUser = await _userRepository.GetUserById(user.Id);
            

            return ServiceResult<UserModel>.Success(newUser);
        }

        public async Task<UserModel> DeleteUser(int userId)
        {
            if (userId <= 0)
            {
                return null;
            }

            var user = await _userRepository.GetUserById(userId);
            if (user == null || user.StatusId == (int)StatusEnumModel.Deleted)
            {
                return null;
            }

            var userModel = await _userRepository.UpdateUserStatus(user.Id, StatusEnumModel.Deleted);
            if (userModel == null)
            {
                return null;
            }

            return userModel;
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            var userModel = await _userRepository.GetAllUsers();
            if (userModel == null)
            {
                return null;
            }

            return userModel;
        }

        public async Task<List<UserModel>> GetUsersByCriteria(List<int>? statusIds)
        {
            var userModel = await _userRepository.GetUsersByCriteria(statusIds);
            if (userModel == null)
            {
                return null;
            }

            return userModel;
        }

        public async Task<bool> DeleteUserFromGroup(int userId, int groupId)
        {
            if (userId <= 0 || groupId <= 0)
            {
                return false;
            }

            var userModel = await _userRepository.GetUserById(userId);
            var groupModel = await _userRepository.GetGroupById(groupId);
            if (userModel == null || groupModel == null)
            {
                return false;
            }

            var group = await _userRepository.DeleteUserFromGroup(userModel.Id, groupModel.Id);
            if (group == null)
            {
                return false;
            }

            if (group.Users.FirstOrDefault(user => user.Id == userId) == null)
            {
                return true;
            }

            return false;
        }

        public async Task<UserModel> GetUserById(int userId)
        {
            if (userId <= 0)
            {
                return null;
            }

            var userModel = await _userRepository.GetUserById(userId);
            if (userModel == null)
            {
                return null;
            }

            return userModel;
        }

        public async Task<UserModel> GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return null;
            }

            var userModel = await _userRepository.GetUserByEmail(email);
            if (userModel == null)
            {
                return null;
            }

            return userModel;
        }

        public async Task<UserModel> GetUserForContactUs(int userId)
        {
            if (userId <= 0)
            {
                return null;
            }

            var userModel = await _userRepository.GetUserForContactUs(userId);
            if (userModel == null)
            {
                return null;
            }

            return userModel;
        }

        public async Task<List<UserModel>> GetUsersByInvestorTier(int investorTierId)
        {
            if (investorTierId <= 0)
            {
                return null;
            }

            var userModel = await _userRepository.GetUsersByInvestorTier(investorTierId);
            if (userModel == null)
            {
                return null;
            }

            return userModel;
        }

        public async Task<List<UserIdeaModel>> GetAllIdeas()
        {
            var ideas = await _userRepository.GetAllIdeas();
            if (ideas == null)
            {
                return null;
            }

            return ideas;
        }

        public async Task<UserModel> ModifyUserCredentials(string email, string password, int userId)
        {
            if (userId <= 0 || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            {
                return null;
            }

            var user = await _userRepository.GetUserByEmail(email);
            if (user != null)
            {
                return null;
            }

            var existingUser = await _userRepository.GetUserById(userId);
            if (existingUser == null)
            {
                return null;
            }

            if (string.Compare(existingUser.Email, email, true) != 0)
            {
                existingUser.Email = email;
                existingUser.StatusId = (int)StatusEnumModel.Pending;
            }
            existingUser.Password = BC.HashPassword(password);

            var newUserModel = await _userRepository.ModifyUserCredentials(email, password, userId, existingUser.StatusId.Value);
            if (newUserModel == null)
            {
                return null;
            }

            newUserModel.Password = null;
            return newUserModel;
        }

        public async Task<UserModel> ModifyUserEmail(string email, string password, int userId)
        {
            if (userId <= 0 || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            {
                return null;
            }

            var user = await _userRepository.GetUserByEmail(email);
            if (user != null)
            {
                return null;
            }

            var existingUser = await _userRepository.GetUserById(userId);
            if (existingUser == null)
            {
                return null;
            }

            if (!BC.Verify(password, existingUser.Password))
            {
                return null;
            }

            if (string.Compare(existingUser.Email, email, true) != 0)
            {
                existingUser.Email = email;
                existingUser.StatusId = (int)StatusEnumModel.Active;
            }

            var newUserModel = await _userRepository.ModifyUserEmail(email, password, userId, existingUser.StatusId.Value);
            if (newUserModel == null)
            {
                return null;
            }

            newUserModel.Password = null;
            return newUserModel;
        }

        public async Task<bool> ModifyUserPassword(string oldPassword, string newPassword, int userId)
        {
            if (userId <= 0 || string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword))
            {
                return false;
            }

            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                return false;
            }

            if (!BC.Verify(oldPassword, user.Password))
            {
                return false;
            }

            var hashedPassword = BC.HashPassword(newPassword);
            var isSuccessful = await _userRepository.ModifyUserPassword(hashedPassword, userId);
            return isSuccessful;
        }

        public async Task<ServiceResult<bool>> ResetUserPassword(string password, string token)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(token))
            {
                return ServiceResult<bool>.Failure("No password provided", ServiceStatusCode.BadRequest);
            }
            var user = await TokenCreationHelper.ValidateJwtToken(token, _configuration, _userRepository);
            if (user == null)
            {
                return ServiceResult<bool>.Failure("Invalid Token", ServiceStatusCode.Unauthorized);
            }

            var hashedPassword = BC.HashPassword(password);
            var isSuccessful = await _userRepository.ModifyUserPassword(hashedPassword, user.Id);

            return ServiceResult<bool>.Success(isSuccessful);

        }

        public async Task<bool> ModifyUserInfo(UserCredential userInformationModel)
        {
            if (userInformationModel == null)
            {
                return false;
            }

            var user = await _userRepository.ModifyUserInfo(userInformationModel);
            return user;
        }

        public async Task<ServiceResult<UserModel>> Login(UserModel userModel)
        {
            var user = await _userRepository.GetUserByEmail(userModel.Email);
            if (user == null)
            {
                return ServiceResult<UserModel>.Failure("User not found with the provided email.", ServiceStatusCode.UserNotFound);
            }

            if (!BC.Verify(userModel.Password, user.Password))
            {
                return ServiceResult<UserModel>.Failure("Incorrect password.", ServiceStatusCode.InvalidCredentials);
            }

            if (user.StatusId == (int)StatusEnumModel.Pending)
            {
                return ServiceResult<UserModel>.Failure("Email is not verified", ServiceStatusCode.UserInactive);
            }

            var accounts = await _userRepository.GetUsersInfoAccount(user.Id);
            if (accounts != null)
            {
                user.Account2Users = accounts;
            }

            user.Password = null;
            user.Token = TokenCreationHelper.GenerateJwtToken(user, _configuration, _userRepository);
            return ServiceResult<UserModel>.Success(user);
        }

        public async Task<ServiceResult<UserModel>> Registration(UserModel userModel)
        {
            if (userModel == null)
            {
                return ServiceResult<UserModel>.Failure("Invalid user data.", ServiceStatusCode.BadRequest);
            }

            var existingUser = await _userRepository.GetUserByEmail(userModel.Email);
            if (existingUser != null)
            {
                return ServiceResult<UserModel>.Failure("A user with this email already exists.", ServiceStatusCode.Conflict);
            }

            userModel.Password = BC.HashPassword(userModel.Password);
            await _userRepository.AddUser(userModel);

            var newUser = await _userRepository.GetUserByEmail(userModel.Email);
            newUser.Token = TokenCreationHelper.GenerateJwtToken(newUser, _configuration, _userRepository);
            newUser.Password = null;

            return ServiceResult<UserModel>.Success(newUser);
        }

        public async Task<ServiceResult<int>> UserInformationRegistration(UserInformationModel userInformationModel)
        {
            if (userInformationModel == null)
            {
                return ServiceResult<int>.Failure("Invalid user information data.", ServiceStatusCode.BadRequest);
            }

            await _userRepository.AddUserInformation(userInformationModel);
            return ServiceResult<int>.Success(userInformationModel.UserId);
        }

        public async Task<List<SearchModel>> GetSearchResults(string searchParam, bool includeGroups)
        {
            if (string.IsNullOrEmpty(searchParam))
            {
                return null;
            }

            var users = await _userRepository.GetSearchResults(searchParam, includeGroups);
            if (users == null)
            {
                return null;
            }

            return users;
        }

        public async Task<UsersGroupModel> CreateGroup(UsersGroupModel usersGroupModel)
        {
            if (string.IsNullOrEmpty(usersGroupModel.GroupName) || usersGroupModel.CreatorId <= 0)
            {
                return null;
            }

            var creatorUser = await _userRepository.GetUserById(usersGroupModel.CreatorId);
            if (creatorUser == null)
            {
                return null;
            }

            usersGroupModel.UserIds.Add(creatorUser.Id);

            var usersGroup = await _userRepository.AddGroup(usersGroupModel);
            if (usersGroup == null)
            {
                return null;
            }
            return usersGroup;
        }

        public async Task<List<UsersGroupModel>> GetAllGroups()
        {
            var groupsModel = await _userRepository.GetAllGroups();
            if (groupsModel == null)
            {
                return null;
            }

            return groupsModel;
        }

        public async Task<List<UsersGroupModel>> GetGroupsByName(string groupName)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                return null;
            }

            var groupsModel = await _userRepository.GetGroupsByName(groupName);
            if (groupsModel == null)
            {
                return null;
            }

            return groupsModel;
        }

        public async Task<UsersGroupModel> GetGroupById(int groupId)
        {
            if (groupId <= 0)
            {
                return null;
            }

            var groupModel = await _userRepository.GetGroupById(groupId);
            if (groupModel == null)
            {
                return null;
            }

            return groupModel;
        }

        public async Task<List<UserModel>> GetUsersByGroupId(int groupId)
        {
            if (groupId <= 0)
            {
                return null;
            }

            var users = await _userRepository.GetUsersByGroupId(groupId);
            if (users == null)
            {
                return null;
            }

            return users;
        }

        public async Task<List<UsersGroupModel>> GetGroupsForUser(int userId)
        {
            if (userId <= 0)
            {
                return null;
            }

            var groups = await _userRepository.GetGroupsForUser(userId);
            if (groups == null)
            {
                return null;
            }

            return groups;
        }

        public async Task<bool> DeleteGroup(int groupId)
        {
            if (groupId <= 0)
            {
                return false;
            }

            var group = await _userRepository.DeleteGroup(groupId);
            if (!group)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> AddUserToGroup(int userId, int groupId)
        {
            if (userId <= 0 || groupId <= 0)
            {
                return false;
            }

            var user = await _userRepository.AddUserToGroup(userId, groupId);
            if (!user)
            {
                return false;
            }

            return true;
        }

        public async Task<UserIdeaModel> AddUserIdea(UserIdeaModel userIdeaModel)
        {
            if (userIdeaModel == null)
            {
                return null;
            }

            var idea = await _userRepository.AddUserIdea(userIdeaModel);
            if (idea == null)
            {
                return null;
            }

            return idea;
        }
    }
}