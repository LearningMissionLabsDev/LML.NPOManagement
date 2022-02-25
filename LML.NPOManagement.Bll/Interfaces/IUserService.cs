using LML.NPOManagement.Bll.Model;
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
    }
}
