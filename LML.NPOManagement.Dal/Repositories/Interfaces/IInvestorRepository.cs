using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace LML.NPOManagement.Dal.Repositories.Interfaces
{
    public interface IInvestorRepository
    {
        Task AddInvestor(UserInformationModel userInformationModel);
    }
}
