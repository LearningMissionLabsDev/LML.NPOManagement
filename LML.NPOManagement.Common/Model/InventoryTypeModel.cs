namespace LML.NPOManagement.Common.Model
{
    public class InventoryTypeModel
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public virtual ICollection<UserInventoryModel> UserInventories { get; } = new List<UserInventoryModel>();
    }
}
