using LML.NPOManagement.Bll.Model;
using Microsoft.Extensions.Configuration;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IRegistrationService
    {
        Task<UserModel> Login(UserModel userModel, IConfiguration configuration);
        Task<UserModel> Registration(UserModel userModel, IConfiguration configuration);
        Task<int> UserInformationRegistration(UserInformationModel userInformationModel, IConfiguration configuration);
    }
}
