using LML.NPOManagement.Bll.Model;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IInventoryTypeService
    {
        public IEnumerable<InventoryTypeModel> GetAllInventoryTypes();
        public InventoryTypeModel GetInventoryTypeById(int id);
        public int AddInventoryType(InventoryTypeModel inventoryTypeModel);
        public int ModifyInventoryType(InventoryTypeModel inventoryTypeModel, int id);
        public void DeleteInventoryType(int id);
    }
}
