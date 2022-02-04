using LML.NPOManagement.Bll.Model;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IBeneficiaryService
    {
        public IEnumerable<BeneficiaryModel> GetAllBeneficiaries();
        public BeneficiaryModel GetBeneficiaryById(int id);
        public int AddBeneficiary(BeneficiaryModel beneficiaryModel);
        public int ModifyBeneficiary(BeneficiaryModel beneficiaryModel, int id);
        public void DeleteBeneficiary(int id);
    }
}
