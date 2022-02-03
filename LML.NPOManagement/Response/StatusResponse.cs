namespace LML.NPOManagement.Response
{
    public class StatusResponse
    {
        public string StatusType { get; set; } = null!;

        public virtual ICollection<AccountManagerInfoResponse> AccountManagerInfosRes { get; set; }
        public virtual ICollection<BeneficiaryResponse> BeneficiariesRes { get; set; }
    }
}
