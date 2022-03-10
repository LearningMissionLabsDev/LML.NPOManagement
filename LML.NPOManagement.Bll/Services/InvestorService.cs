using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Services
{
    public class InvestorService : IInvestorService
    {
        private IMapper _mapper;
        public InvestorService()
        {
            var config = new MapperConfiguration(cfg =>
            {             
                cfg.CreateMap<InvestorInformation, InvestorInformationModel>();
                cfg.CreateMap<InvestorInformationModel, InvestorInformation>();
            });
            _mapper = config.CreateMapper();
        }

        public int AddInvestorInformation(InvestorInformationModel investorInformationModel)
        {
            throw new NotImplementedException();
        }

        public void DeleteInvestorInformation(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<InvestorInformationModel> GetAllInvestorInformations()
        {
            throw new NotImplementedException();
        }

        public InvestorInformationModel GetInvestorInformationById(int id)
        {
            throw new NotImplementedException();
        }

        public int ModifyInvestorInformation(InvestorInformationModel investorInformationModel, int id)
        {
            throw new NotImplementedException();
        }
    }
}
