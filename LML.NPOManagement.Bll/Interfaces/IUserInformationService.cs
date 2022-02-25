using LML.NPOManagement.Bll.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IUserInformationService
    {
        public IEnumerable<UserInformationModel> GetAllUserInformations();
        public UserInformationModel GetUserInformationById(int id);
        public int AddUserInformation(UserInformationModel userInformationModel);
        public int ModifyUserInformation(UserInformationModel userInformationModel, int id);
        public void DeleteUserInformation(int id);
    }
}
