using LML.NPOManagement.Bll.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IUserInventoryService
    {
        public IEnumerable<UserInventoryModel> GetAllUserInventories();
        public UserInventoryModel GetUserInventoryById(int id);
        public int AddUserInventory(UserInventoryModel userInventoryModel);
        public int ModifyUserInventory(UserInventoryModel userInventoryModel, int id);
        public void DeleteUserInventory(int id);
    }
}
