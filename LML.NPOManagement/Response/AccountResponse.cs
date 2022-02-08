namespace LML.NPOManagement.Response
{
    public class AccountResponse
    {
        public string AccountCategory { get; set; } = null!;

        public virtual ICollection<AccountManagerInfoResponse> AccountManagerInfosRes { get; set; }
        public virtual ICollection<BeneficiaryResponse> BeneficiariesRes { get; set; }
    }
}