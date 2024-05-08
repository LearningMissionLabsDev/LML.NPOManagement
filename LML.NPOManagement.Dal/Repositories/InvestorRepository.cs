using AutoMapper;
using LML.NPOManagement.Common;
using LML.NPOManagement.Common.Model;
using LML.NPOManagement.Dal.Models;
using LML.NPOManagement.Dal.Repositories.Interfaces;

namespace LML.NPOManagement.Dal.Repositories
{
    public class InvestorRepository : IInvestorRepository
    {
        IMapper _mapper;
        private readonly NpomanagementContext _dbContext;
        public InvestorRepository(NpomanagementContext context)
        {
            var config = new MapperConfiguration(cfg =>
            {

            });
            _mapper = config.CreateMapper();
            _dbContext = context;
        }

        public async Task AddInvestor(UserInformationModel userInformationModel)
        {
           await _dbContext.InvestorInformations.AddAsync(new InvestorInformation()
            {
                UserId = userInformationModel.UserId,
                InvestorTierId = Convert.ToInt16(InvestorTierEnum.Basic)
            });
            await _dbContext.SaveChangesAsync();
        }
    }
}
