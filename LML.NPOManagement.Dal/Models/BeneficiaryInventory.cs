
namespace LML.NPOManagement.Dal.Models
{
    public partial class BeneficiaryInventory
    {
        public int Id { get; set; }
        public int InventoryTypeId { get; set; }
        public int? BeneficiaryId { get; set; }
        public DateTime Date { get; set; }
        public string Metadate { get; set; } = null!;
        public string? Description { get; set; }

        public virtual Beneficiary? Beneficiary { get; set; }
        public virtual InventoryType InventoryType { get; set; } = null!;
    }
}
