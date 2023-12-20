using LML.NPOManagement.Bll.Model;
using Microsoft.Extensions.Configuration;


namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IUserService
    {
        Task<List<UserModel>> GetAllUsers();
        Task<List<UserModel>> GetUsersByRole(int id);
        Task<List<UserModel>> GetUsersByInvestorTier(int id);
        Task<List<UserModel>> GetUsersByAccount(int id);
        Task<UserModel> GetUserById(int id);
        Task<List<UserInformationModel>> GetUserByName(UserInformationModel name);
        Task<bool> ModifyUser(UserModel userModel, int id);
        Task<bool> ModifyUserInfo(UserInformationModel userInformationModel, int id);
        public void DeleteUser(int id);
        Task<UserTypeModel> AddUserType(UserInformationModel userInformationModel);
        Task<UserModel> ActivationUser(string token, IConfiguration configuration);
    }
}
