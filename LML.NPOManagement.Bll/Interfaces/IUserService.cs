using LML.NPOManagement.Bll.Model;
using Microsoft.Extensions.Configuration;


namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IUserService
    {
        public IEnumerable<UserModel> GetAllUsers();
        public IEnumerable<UserTypeModel> GetAllUserTypes();
        Task<List<UserModel>> GetUsersByRole(int id);
        Task<List<UserModel>> GetUsersByInvestorTier(int id);
        Task<List<UserModel>> GetUsersByAccount(int id);
        public UserModel GetUserById(int id);
        public bool ModifyUser(UserModel userModel, int id);
        public void DeleteUser(int id);
        Task<UserModel> Login(UserModel userModel, IConfiguration configuration);
        Task<UserModel> Registration(UserModel userModel, IConfiguration configuration);
        Task<int> UserInformationRegistration(UserInformationModel userInformationModel, IConfiguration configuration);
        public void AddUserType(UserInformationModel userInformationModel);
        Task<UserModel> ActivationUser(string token, IConfiguration configuration);
    }
}
