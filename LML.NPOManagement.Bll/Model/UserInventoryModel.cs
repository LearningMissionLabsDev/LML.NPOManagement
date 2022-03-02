using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Model
{
    public class UserInventoryModel
    {
        public UserInventoryModel(UserInventory userInventory)
        {
            userInventory.Id = Id;
            userInventory.InventoryTypeId = InventoryTypeId;
            userInventory.UserId = UserId;
            userInventory.Date = Date;
            userInventory.Metadate = Metadate;
            userInventory.Description = Description;
        }
        public int Id { get; set; }
        public int InventoryTypeId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Metadate { get; set; }
        public string Description { get; set; }

        public virtual InventoryTypeModel InventoryType { get; set; } 
        public virtual UserModel User { get; set; }
    }
}
