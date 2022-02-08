
namespace LML.NPOManagement.Bll.Model
{
    public class BeneficiaryInventoryModel
    {
        public int Id { get; set; }
        public int InventoryTypeId { get; set; }
        public int BeneficiaryId { get; set; }
        public DateTime Date { get; set; }
        public string Metadate { get; set; } = null!;
        public string Description { get; set; }

        public virtual BeneficiaryModel Beneficiary { get; set; }
        public virtual InventoryTypeModel InventoryType { get; set; } = null!;
    }
}
