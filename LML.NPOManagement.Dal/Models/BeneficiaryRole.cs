
namespace LML.NPOManagement.Dal.Models
{
    public partial class BeneficiaryRole
    {
        public BeneficiaryRole()
        {
            Beneficiaries = new HashSet<Beneficiary>();
        }

        public int Id { get; set; }
        public string BeneficiaryRoleType { get; set; } = null!;

        public virtual ICollection<Beneficiary> Beneficiaries { get; set; }
    }
}
