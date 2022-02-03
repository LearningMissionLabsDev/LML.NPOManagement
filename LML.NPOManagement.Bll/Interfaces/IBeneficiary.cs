using LML.NPOManagement.Bll.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IBeneficiary
    {
        public IEnumerable<BeneficiaryModel> GetAllBeneficiary();
        public BeneficiaryModel GetBeneficiaryById(int id);
        public int AddBeneficiary(BeneficiaryModel beneficiaryModel);
        public int ModifyBeneficiary(BeneficiaryModel beneficiaryModel, int id);
        public void DeleteBeneficiary(int id);
    }
}
