using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Common;
using LML.NPOManagement.Common.Model;
using LML.NPOManagement.Dal.Models;
using LML.NPOManagement.Dal.Repositories;
using LML.NPOManagement.Dal.Repositories.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BC = BCrypt.Net.BCrypt;

namespace LML.NPOManagement.Bll.Services
{
    public class UserService : IUserService
    {
        IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IInvestorRepository _investorRepository;

        public UserService(IUserRepository userRepository, IInvestorRepository investorRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserModel>();
                cfg.CreateMap<UserModel, User>();
            });
            _mapper = config.CreateMapper();
            _userRepository = userRepository;
            _investorRepository = investorRepository;
        }

        public async Task<UserModel> ActivationUser(string token, IConfiguration configuration)
        {
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }
            var newUser = TokenCreationHelper.ValidateJwtToken(token, configuration);

            if (newUser == null)
            {
                return null;
            }
            await _userRepository.UpdateUserStatus(newUser.Id, StatusEnumModel.Active);
            newUser.StatusId = (int)StatusEnumModel.Active;

            return newUser;
        }

        public async Task DeleteUser(int userId)
        {
            if(userId == 0)
            {
                throw new ArgumentException("Invalid user");
            }
            await _userRepository.UpdateUserStatus(userId, StatusEnumModel.Deleted);
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

        public async Task DeleteUserFromGroup(int userId, int groupId)
        {
            await _userRepository.DeleteUserFromGroup(userId, groupId);
        }

        public async Task<UserModel> GetUserById(int userId)
        {
            var userModel = await _userRepository.GetUserById(userId);

            if (userModel == null)
            {
                return null;
            }

            return userModel;
        }

        public async Task<UserModel> GetUserByEmail(string email)
        {
            var userModel = await _userRepository.GetUserByEmail(email);

            if (userModel == null)
            {
                return null;
            }

            return userModel;
        }

        public async Task<List<UserInformationModel>> GetUsersByName(UserInformationModel userInfo)
        {
            var userModel = await _userRepository.GetUsersByName(userInfo);

            if (userModel == null)
            {
                return null;
            }

            return userModel;
        }

        public async Task<List<UserModel>> GetUsersByAccount(int userId)
        {
            var usersByAccount = await _userRepository.GetUsersByAccount(userId);

            if (usersByAccount == null)
            {
                return null;
            }

            return usersByAccount;
        }

        public async Task<List<UserModel>> GetUsersByInvestorTier(int userId)
        {
            var userModel = await _userRepository.GetUsersByInvestorTier(userId);

            if (userModel == null)
            {
                return null;
            }

            return userModel;
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
                throw new ArgumentException("Email already exists", "Email");
            }

            var existingUser = await _userRepository.GetUserById(userId);

            if (existingUser == null)
            {
                return null;
            }

            if (string.Compare(existingUser.Email, email, true) != 0)
            {
                user.Email = email;
                user.StatusId = (int)StatusEnumModel.Pending;
            }
            user.Password = BC.HashPassword(password);

            var newUserModel = await _userRepository.ModifyUserCredentials(email, password, userId, user.StatusId.Value);

            if (newUserModel == null)
            {
                return null;
            }
            newUserModel.Password = null;

            return newUserModel;
        }

        public async Task<bool> ModifyUserInfo(UserInformationModel userInformationModel, int userId)
        {
            var user = await _userRepository.ModifyUserInfo(userInformationModel, userId);

            return user;
        }

        public async Task<UserModel> Login(UserModel userModel, IConfiguration configuration)
        {
            var user = await _userRepository.GetUserByEmail(userModel.Email);

            if (user != null && BC.Verify(userModel.Password, user.Password))
            {
                user.Password = null;
                user.Token = TokenCreationHelper.GenerateJwtToken(user, configuration);

                return user;
            }
            return null;
        }

        public async Task<UserModel> Registration(UserModel userModel, IConfiguration configuration)
        {
            var user = await _userRepository.GetUserByEmail(userModel.Email);

            if (user == null)
            {
                userModel.Password = BC.HashPassword(userModel.Password);
                await _userRepository.AddUser(userModel);

                var newUser = await _userRepository.GetUserByEmail(userModel.Email);
                newUser.Token = TokenCreationHelper.GenerateJwtToken(newUser, configuration);
                newUser.Password = null;

                return newUser;
            }
            else if (user.StatusId == (int)StatusEnumModel.Deleted)
            {
                return null;
            }
            return null;
        }

        public async Task<int> UserInformationRegistration(UserInformationModel userInformationModel, IConfiguration configuration)
        {
            await _userRepository.AddUserInformation(userInformationModel);

            if (userInformationModel.UserTypeEnum == UserTypeEnum.Investor)
            {
                await _investorRepository.AddInvestor(userInformationModel);
            }

            return userInformationModel.Id;
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
            if (string.IsNullOrEmpty(usersGroupModel.GroupName))
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

        public async Task DeleteGroup(int groupId)
        {
            if (groupId <= 0)
            {
                throw new ArgumentException("Group not found");
            }

            await _userRepository.DeleteGroup(groupId);
        }

        public async Task<bool> AddUserToGroup(int userId, int groupId)
        {
            if (userId <= 0 || groupId <= 0)
            {
                return false;
            }
            var user = await _userRepository.AddUserToGroup(userId, groupId);

            return user;
        }
    }
}
