using LML.NPOManagement.Bll.Model;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IBeneficiaryInventoryService
    {
        public IEnumerable<BeneficiaryInventoryModel> GetAllBeneficiaryInventories();
        public BeneficiaryInventoryModel GetBeneficiaryInventoryById(int id);
        public int AddBeneficiaryInventory(BeneficiaryInventoryModel beneficiaryInventoryModel);
        public int ModifyBeneficiaryInventory(BeneficiaryInventoryModel beneficiaryInventoryModel, int id);
        public void DeleteBeneficiaryInventory(int id);
    }
}
