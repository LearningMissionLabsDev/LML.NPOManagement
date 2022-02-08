using LML.NPOManagement.Bll.Model;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IAccountManagerInventoryService
    {
        public IEnumerable<AccountManagerInventoryModel> GetAllAccountManagerInventories();
        public AccountManagerInventoryModel GetAccountManagerInventoryById(int id);
        public int AddAccountManagerInventory(AccountManagerInventoryModel accountManagerInventoryModel);
        public int ModifyAccountManagerInventory(AccountManagerInventoryModel accountManagerInventoryModel, int id);
        public void DeleteAccountManagerInventory(int id);
    }
}
