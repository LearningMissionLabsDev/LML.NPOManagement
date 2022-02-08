
namespace LML.NPOManagement.Bll.Model
{
    public class InventoryTypeModel
    {
        public InventoryTypeModel()
        {
            AccountManagerInventories = new HashSet<AccountManagerInventoryModel>();
            BeneficiaryInventories = new HashSet<BeneficiaryInventoryModel>();
        }

        public int Id { get; set; }
        public string Description { get; set; } = null!;

        public virtual ICollection<AccountManagerInventoryModel> AccountManagerInventories { get; set; }
        public virtual ICollection<BeneficiaryInventoryModel> BeneficiaryInventories { get; set; }
    }
}
