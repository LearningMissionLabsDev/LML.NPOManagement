namespace LML.NPOManagement.Response
{
    public class AccountResponse
    {
        public string AccountName { get; set; } = null!;
        public string AccountDescription { get; set; }

        public virtual ICollection<AccountManagerInfoResponse> AccountManagerInfosRes { get; set; }
        public virtual ICollection<BeneficiaryResponse> BeneficiariesRes { get; set; }
    }
}