namespace LML.NPOManagement.Response
{
    public class BeneficiaryInventoryResponse
    {
        public int InventoryTypeId { get; set; }
        public int BeneficiaryId { get; set; }
        public DateTime Date { get; set; }

        public virtual BeneficiaryResponse BeneficiaryRes{ get; set; }
        public virtual InventoryTypeResponse InventoryTypeRes { get; set; } = null!;
    }
}
