namespace LML.NPOManagement.Response
{
    public class AccountManagerInventoryResponse
    {
        public int InventoryTypeId { get; set; }
        public int AccountManagerInfoId { get; set; }
        public DateTime Date { get; set; }

        public virtual AccountManagerInfoResponse AccountManagerInfoRes { get; set; }
        public virtual InventoryTypeResponse InventoryTypeRes { get; set; } = null!;
    }
}
