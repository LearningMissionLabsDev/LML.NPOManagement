using LML.NPOManagement.Common;

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
        Task<string> GetAllInventoryTypes(string type, DateTime dateTimeStart, DateTime dateTimeFinsh);
        Task<List<UserInventoryModel>> GetInventoryByUser(int id);
        Task<List<UserInventoryModel>> GetInventoryByYear(DateTime dateTimeStart, DateTime dateTimeFinish);
        Task<List<UserInventoryModel>> GetInventoryUserByYear(DateTime dateTimeStart, DateTime dateTimeFinish, int id);
        Task<UserInventoryModel> DeleteInventory(int id);
    }
}
