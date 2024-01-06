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
        Task <UserModel> GetUserById(int id);
        Task <bool> ModifyUser(UserModel userModel, int id);
        Task<bool> ModifyUserInfo(UserInformationModel userInformationModel,int id);
        public void DeleteUser(int id);
        Task<UserModel> Login(UserModel userModel, IConfiguration configuration);
        Task<UserModel> Registration(UserModel userModel, IConfiguration configuration);
        Task<int> UserInformationRegistration(UserInformationModel userInformationModel, IConfiguration configuration);
        Task<UserTypeModel> AddUserType(UserInformationModel userInformationModel);
        Task<UserModel> ActivationUser(string token, IConfiguration configuration);
        Task<List<SearchModel>> GetSearchResult(string firstChars, bool includeGroups);
    }
}
