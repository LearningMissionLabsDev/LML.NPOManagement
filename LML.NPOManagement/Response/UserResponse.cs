namespace LML.NPOManagement.Response
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int? StatusId { get; set; }
        public ICollection<AccountMappingResponse> UserAccounts { get; set; }
    }
}
