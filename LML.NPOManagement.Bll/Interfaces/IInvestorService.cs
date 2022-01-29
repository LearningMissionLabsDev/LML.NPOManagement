using LML.NPOManagement.Bll.Model;

namespace LML.NPOManagement.Bll.Independencies
{
    public interface IInvestorService
    {
        public IEnumerable<InvestorModel> GetAllInvestors();
        public InvestorModel GetInvestorById(int id);
        public int AddInvestor(InvestorModel investorModel);
        public int ModifyInvestor(InvestorModel investorModel, int id);
        public void DeleteInvestor(int id);
    }
}
