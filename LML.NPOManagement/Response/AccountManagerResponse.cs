namespace LML.NPOManagement.Response
{
    public class AccountManagerResponse
    {
        public string AccountManagerCategory { get; set; } = null!;
        public string NarrowProfessional { get; set; }

        public virtual ICollection<AccountManagerInfoResponse> AccountManagerInfosRes { get; set; }
    }
}
