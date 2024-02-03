namespace LML.NPOManagement.Common
{
    public class InventoryTypeModel
    {
        public InventoryTypeModel()
        {
            UserInventories = new HashSet<UserInventoryModel>();
        }
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public virtual ICollection<UserInventoryModel> UserInventories { get; } = new List<UserInventoryModel>();
    }
}
