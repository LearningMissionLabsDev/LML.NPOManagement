using LML.NPOManagement.Common.Model;

namespace LML.NPOManagement.Dal.Repositories.Interfaces
{
    public interface IInvestorRepository
    {
        Task AddInvestor(UserInformationModel userInformationModel);
    }
}
