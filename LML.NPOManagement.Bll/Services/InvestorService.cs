using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
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
                cfg.CreateMap<InvestorInformation, InvestorInformationModel>();
                cfg.CreateMap<UserInformation, UserInformationModel>();
                cfg.CreateMap<UserType, UserTypeModel>();
                cfg.CreateMap<DonationModel, Donation>();
                cfg.CreateMap<InvestorInformationModel, InvestorInformation>();
                cfg.CreateMap<UserInformationModel, UserInformation>();
                cfg.CreateMap<UserInventoryModel, UserInventory>();
                cfg.CreateMap<UserTypeModel, UserType>();
            });
            _mapper = config.CreateMapper();
        }

        public int AddDonation(DonationModel donationModel)
        {
            using (var dbContext = new NPOManagementContext())
            {
                var donation = _mapper.Map<DonationModel, Donation>(donationModel);
                dbContext.Donations.Add(donation);
                dbContext.SaveChanges();
                return donation.Id;
            }
        }

        public void DeleteDonation(int id)
        {
            using (var dbContext = new NPOManagementContext())
            {
                var donation = dbContext.Donations.Where(d => d.Id == id).FirstOrDefault();
                donation.Investor.User.Status = Convert.ToString(StatusEnumModel.Closed);
                dbContext.SaveChanges();
            }
        }

        public IEnumerable<InvestorInformationModel> GetAllInvestorInformations()
        {
            using (var dbContext = new NPOManagementContext())
            {
                var investors = dbContext.InvestorInformations.ToList();
                foreach (var investor in investors)
                {
                    var InvestorModel = _mapper.Map<InvestorInformation, InvestorInformationModel>(investor);
                    yield return InvestorModel;
                }
            }
        }

        public InvestorInformationModel GetInvestorInformationById(int id)
        {
            using (var dbContext = new NPOManagementContext())
            {
                var investor = dbContext.InvestorInformations.Where(d => d.Id == id).FirstOrDefault();
                if (investor != null)
                {
                    return _mapper.Map<InvestorInformation, InvestorInformationModel>(investor);
                }
                return null;
            }
        }

        public IEnumerable<DonationModel> GetAllDonation()
        {
            using (var dbContext = new NPOManagementContext())
            {
                var donations = dbContext.Donations.ToList();
                foreach (var donation in donations)
                {
                    var donationModel = _mapper.Map<Donation, DonationModel>(donation);
                    yield return donationModel;
                }
            }
        }

        public DonationModel GetDonationById(int id)
        {
            using (var dbContext = new NPOManagementContext())
            {
                var donation = dbContext.Donations.Where(d => d.Id == id).FirstOrDefault();
                if (donation != null)
                {
                    return _mapper.Map<Donation, DonationModel>(donation);
                }
                return null;
            }
        }
        public DonationModel ModifyDonation(DonationModel donationModel, int id)
        {
            using (var dbContext = new NPOManagementContext())
            {
                var donation = dbContext.Donations.Where(d => d.Id == id).FirstOrDefault();
                if (donation != null)
                {
                    var modifyDonation = _mapper.Map<DonationModel, Donation>(donationModel);
                    dbContext.SaveChanges();
                    var newDonation = _mapper.Map<Donation, DonationModel>(modifyDonation);
                    return newDonation;
                }
                return null;
            }
        }
    }
}
