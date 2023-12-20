using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LML.NPOManagement.Dal.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetAllUsers();
        Task<List<UserModel>> GetUsersByRole(int id);
        Task<List<UserModel>> GetUsersByInvestorTier(int id);
        Task<List<UserModel>> GetUsersByAccount(int id);
        Task<UserModel> GetUserById(int id);
        Task<List<UserInformationModel>> GetUserByName(UserInformationModel name);
        Task<bool> ModifyUser(UserModel userModel, int id);
        Task<bool> ModifyUserInfo(UserInformationModel userInformation, int id);
        public void DeleteUser(int id);
        Task<UserModel> ActivationUser(string token, IConfiguration configuration);
        Task<UserTypeModel> AddUserType(UserInformationModel userInformationModel);
        Task<UserModel> GetUserByEmail(string email);
        Task AddUser(UserModel userModel);
        Task AddUserInformation(UserInformationModel userInformationModel);
    }
}
