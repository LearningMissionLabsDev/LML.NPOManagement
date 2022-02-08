
namespace LML.NPOManagement.Bll.Model
{
    public class BeneficiaryRoleModel
    {
        public BeneficiaryRoleModel()
        {
            Beneficiaries = new HashSet<BeneficiaryModel>();
        }

        public int Id { get; set; }
        public string BeneficiaryRoleType { get; set; } = null!;

        public virtual ICollection<BeneficiaryModel> Beneficiaries { get; set; }
    }
}
