namespace LML.NPOManagement.Response
{ 
    public class UserInventoryResponse
    {
        public int InventoryTypeId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Metadate { get; set; }
        public string Description { get; set; }
    }
}
