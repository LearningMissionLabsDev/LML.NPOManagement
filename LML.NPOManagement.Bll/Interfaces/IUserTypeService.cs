using LML.NPOManagement.Bll.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IUserTypeService
    {
        public IEnumerable<UserTypeModel> GetAllUserTypes();
        public UserTypeModel GetUserTypeById(int id);
        public int AddUserType(UserTypeModel userTypeModel);
        public int ModifyUserType(UserTypeModel userTypeModel, int id);
        public void DeleteUserType(int id);
    }
}
