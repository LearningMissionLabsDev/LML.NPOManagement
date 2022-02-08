
namespace LML.NPOManagement.Dal.Models
{
    public partial class InventoryType
    {
        public InventoryType()
        {
            AccountManagerInventories = new HashSet<AccountManagerInventory>();
            BeneficiaryInventories = new HashSet<BeneficiaryInventory>();
        }

        public int Id { get; set; }
        public string Description { get; set; } = null!;

        public virtual ICollection<AccountManagerInventory> AccountManagerInventories { get; set; }
        public virtual ICollection<BeneficiaryInventory> BeneficiaryInventories { get; set; }
    }
}
