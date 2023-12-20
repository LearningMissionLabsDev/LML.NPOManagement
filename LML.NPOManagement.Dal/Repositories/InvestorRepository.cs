using AutoMapper;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal.Models;
using LML.NPOManagement.Dal.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Dal.Repositories
{
    public class InvestorRepository : IInvestorRepository
    {
        IMapper _mapper;
        private readonly INPOManagementContext _dbContext;
        public InvestorRepository(INPOManagementContext context)
        {
            var config = new MapperConfiguration(cfg =>
            {

            });
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
