using LML.NPOManagement.Bll.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IUserService
    {
        public IEnumerable<UserModel> GetAllUsers();
        public UserModel GetUserById(int id);
        public int AddUser(UserModel userModel);
        public int ModifyUser(UserModel userModel, int id);
        public void DeleteUser(int id);
        Task<UserModel> Login(UserModel userModel, IConfiguration configuration);
        Task<bool> Registration(UserModel userModel, IConfiguration configuration);
        Task<int> UserInformationRegistration(UserInformationModel userInformationModel);
    }
}
