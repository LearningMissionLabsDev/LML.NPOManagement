
namespace LML.NPOManagement.Dal.Models
{
    public partial class AccountManagerInventory
    {
        public int Id { get; set; }
        public int InventoryTypeId { get; set; }
        public int? AccountManagerInfoId { get; set; }
        public DateTime Date { get; set; }
        public string Metadate { get; set; } = null!;
        public string? Description { get; set; }

        public virtual AccountManagerInfo? AccountManagerInfo { get; set; }
        public virtual InventoryType InventoryType { get; set; } = null!;
    }
}
