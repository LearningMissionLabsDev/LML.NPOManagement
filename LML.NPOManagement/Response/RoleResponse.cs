namespace LML.NPOManagement.Response
{
    public class RoleResponse
    {
        public string Role1 { get; set; } = null!;

        public virtual ICollection<AccountManagerInfoResponse> AccountManagerInfosRes { get; set; }
        public virtual ICollection<BeneficiaryResponse> BeneficiariesRes { get; set; }
    }
}
