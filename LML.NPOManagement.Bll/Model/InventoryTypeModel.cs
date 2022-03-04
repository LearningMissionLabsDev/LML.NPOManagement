using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Model
{
    public class InventoryTypeModel
    {
        public InventoryTypeModel()
        {
            UserInventories = new HashSet<UserInventoryModel>();
        }
        public int Id { get; set; }
        public string Description { get; set; } 

        public virtual ICollection<UserInventoryModel> UserInventories { get; set; }
    }
}
