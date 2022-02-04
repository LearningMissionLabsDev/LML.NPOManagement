using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Model
{
    public class BeneficiaryRoleModel
    {
        public BeneficiaryRoleModel()
        {
            Beneficiaries = new HashSet<Beneficiary>();
        }

        public int Id { get; set; }
        public string BeneficiaryRoleType { get; set; } = null!;

        public virtual ICollection<Beneficiary> Beneficiaries { get; set; }
    }
}
