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
        Task<List<UserInventoryModel>> GetAllUserInventories();
        Task<UserInventoryModel> GetUserInventoryById(int id);
        Task<UserInventoryModel> AddUserInventory(UserInventoryModel userInventoryModel);
        Task<UserInventoryModel> ModifyUserInventory(UserInventoryModel userInventoryModel, int id);
        Task<InventoryTypeModel> GetUserInventoryTypeById(int id);
        Task<InventoryTypeModel> AddInventoryType(InventoryTypeModel inventoryTypeModel);
        Task<List<InventoryTypeModel>> GetAllInventoryTypes();
        Task<List<UserInventoryModel>> GetInventoryByUser(int id);
    }
}
