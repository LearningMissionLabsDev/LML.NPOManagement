
namespace LML.NPOManagement.Bll.Model
{
    public class AccountManagerInventoryModel
    {
        public int Id { get; set; }
        public int InventoryTypeId { get; set; }
        public int AccountManagerInfoId { get; set; }
        public DateTime Date { get; set; }
        public string Metadate { get; set; } = null!;
        public string Description { get; set; }

        public virtual AccountManagerInfoModel AccountManagerInfo { get; set; }
        public virtual InventoryTypeModel InventoryType { get; set; } = null!;
    }
}
