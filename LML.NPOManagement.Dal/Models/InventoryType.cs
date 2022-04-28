namespace LML.NPOManagement.Dal.Models
{
    public partial class InventoryType
    {
        public InventoryType()
        {
            UserInventories = new HashSet<UserInventory>();
        }

        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public virtual ICollection<UserInventory> UserInventories { get; set; }
    }
}
