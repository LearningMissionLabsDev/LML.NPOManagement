using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal.Models;
using LML.NPOManagement.Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LML.NPOManagement.Bll.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserModel> ActivationUser(string token, IConfiguration configuration)
        {
            var user = await _userRepository.ActivationUser(token, configuration);

            return user;
        }
        public async Task<UserTypeModel> AddUserType(UserInformationModel userInformationModel)
        {
            var userType = await _userRepository.AddUserType(userInformationModel);

            return userType;
        }  
        public void DeleteUser(int id)
        {
            _userRepository.DeleteUser(id);
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

        public async Task<UserModel> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return null;
            }
            return user;
        }
        public async Task<List<UserInformationModel>> GetUserByName(UserInformationModel searchUser)
        {
            var userModel = await _userRepository.GetUserByName(searchUser);

            if (userModel == null)
            {
                return null;
            }
            return userModel;
        }
        public async Task<List<UserModel>> GetUsersByAccount(int id)
        {
            var usersByAccount = await _userRepository.GetUsersByAccount(id);
            if (usersByAccount == null)
            {
                return null;
            }
            return usersByAccount;
        }

        public async Task<List<UserModel>> GetUsersByInvestorTier(int id)
        {
            var users = await _userRepository.GetUsersByInvestorTier(id);
            if (users == null)
            {
                return null;
            }
            return users;
        }
        public async Task<List<UserModel>> GetUsersByRole(int id)
        {
            var users = await _userRepository.GetUsersByRole(id);
            if (users.Count > 0)
            {
                return users;
            }
            return null;
        }

        public async Task<bool> ModifyUser(UserModel userModel, int id)
        {
            var user = await _userRepository.ModifyUser(userModel, id);

            return user;
        }
        public async Task<bool> ModifyUserInfo(UserInformationModel userInformationModel, int id)
        {
            var user = await _userRepository.ModifyUserInfo(userInformationModel, id);

            return user;
        }
    }
}
