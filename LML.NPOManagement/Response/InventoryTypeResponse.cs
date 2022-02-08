namespace LML.NPOManagement.Response
{
    public class InventoryTypeResponse
    {
        public string Description { get; set; } = null!;

        public virtual ICollection<AccountManagerInventoryResponse> AccountManagerInventoriesRes { get; set; }
        public virtual ICollection<BeneficiaryInventoryResponse> BeneficiaryInventoriesRes { get; set; }
    }
}
