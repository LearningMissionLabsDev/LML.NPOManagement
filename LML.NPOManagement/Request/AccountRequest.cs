namespace LML.NPOManagement.Request
{
    public class AccountRequest
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Status { get; set; }
    }
}
