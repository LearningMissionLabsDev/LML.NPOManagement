namespace LML.NPOManagement.Response
{
    public class BeneficiaryRoleResponse
    {
        public string BeneficiaryRoleType { get; set; } = null!;

        public virtual ICollection<AccountManagerInfoResponse> AccountManagerInfosRes { get; set; }
        public virtual ICollection<BeneficiaryResponse> BeneficiariesRes { get; set; }
    }
}
