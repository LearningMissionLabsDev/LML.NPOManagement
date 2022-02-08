namespace LML.NPOManagement.Request
{
    public class BeneficiaryInventoryRequest
    {
        public int InventoryTypeId { get; set; }
        public int BeneficiaryId { get; set; }
        public DateTime Date { get; set; }
    }
}
