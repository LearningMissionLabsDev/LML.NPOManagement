using AutoMapper;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Bll.Independencies;
using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Services
{
    public class InvestorService : IInvestorService
    {
        private IMapper _mapper;
        public InvestorService()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Donation, DonationModel>();
                cfg.CreateMap<Investor, InvestorModel>();
                cfg.CreateMap<DonationModel, Donation>();
                cfg.CreateMap<InvestorModel, Investor>();

            });
            _mapper = config.CreateMapper();
        }

        public int AddInvestor(InvestorModel investorModel)
        {
            throw new NotImplementedException();
        }

        public void DeleteInvestor(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<InvestorModel> GetAllInvestors()
        {
            using (var investorContext = new NPOManagementContext())
            {
                var investors = investorContext.Investors.ToList();
                foreach (var investor in investors)
                {
                    var investorModel = _mapper.Map<Investor, InvestorModel>(investor);
                    yield return investorModel;
                }
            }

        }

        public InvestorModel GetInvestorById(int id)
        {
            using (var dbContext = new NPOManagementContext())
            {
                var investor = dbContext.Investors.Where(investor => investor.Id == id).FirstOrDefault();
                if (investor != null)
                {
                    var investorModel = _mapper.Map<Investor, InvestorModel>(investor);
                    return investorModel;
                }
                return null;
            }
        }

        public void Delete(int id)
        {
            using (var dbcontext = new NPOManagementContext())
            {
                var investor = dbcontext.Investors.Where(investor => investor.Id == id).FirstOrDefault();
                if (investor != null)
                {
                    dbcontext.Investors.Remove(investor);
                    dbcontext.SaveChanges();
                }
            }

        }

        public void ModifyInvestor(InvestorModel investorModel)
        {
            throw new NotImplementedException();
        }
    }
}
