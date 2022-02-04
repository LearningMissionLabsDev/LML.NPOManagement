using LML.NPOManagement.Bll.Model;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IBeneficiaryRoleService
    {
        public IEnumerable<BeneficiaryRoleModel> GetAllBeneficiaryRoles();
        public BeneficiaryRoleModel GetBeneficiaryRoleById(int id);
       
    }
}
