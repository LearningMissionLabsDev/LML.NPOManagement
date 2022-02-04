namespace LML.NPOManagement.Response
{
    public class AccountManagerRoleResponse
    {
        public string AccountManagerRoleType { get; set; } = null!;

        public virtual ICollection<AccountManagerInfoResponse> AccountManagerInfosRes { get; set; }
    }
}
