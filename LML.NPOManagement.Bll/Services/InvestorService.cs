using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal;
using LML.NPOManagement.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace LML.NPOManagement.Bll.Services
{
    public class InvestorService : IInvestorService
    {
        private IMapper _mapper;
        private readonly INPOManagementContext _dbContext;
        public InvestorService(INPOManagementContext context)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountProgress, AccountProgressModel>();
                cfg.CreateMap<Attachment, AttachmentModel>();
                cfg.CreateMap<Donation, DonationModel>();
                cfg.CreateMap<Account, AccountModel>();
                cfg.CreateMap<InvestorInformation, InvestorInformationModel>();
                cfg.CreateMap<InventoryType, InventoryTypeModel>();
                cfg.CreateMap<Notification, NotificationModel>();
                cfg.CreateMap<UserInformation, UserInformationModel>();
                cfg.CreateMap<UserInventory, UserInventoryModel>();
                cfg.CreateMap<UserType, UserTypeModel>();
                cfg.CreateMap<AccountProgressModel, AccountProgress>();
                cfg.CreateMap<AttachmentModel, Attachment>();
                cfg.CreateMap<DonationModel, Donation>();
                cfg.CreateMap<AccountModel, Account>();
                cfg.CreateMap<InvestorInformationModel, InvestorInformation>();
                cfg.CreateMap<InventoryTypeModel, InventoryType>();
                cfg.CreateMap<NotificationModel, Notification>();
                cfg.CreateMap<UserInformationModel, UserInformation>();
                cfg.CreateMap<UserInventoryModel, UserInventory>();
                cfg.CreateMap<UserTypeModel, UserType>();
            });
            _mapper = config.CreateMapper();
            _dbContext = context;
        }

        public async Task<int> AddDonation(DonationModel donationModel)
        {
            var donation =  _mapper.Map<DonationModel, Donation>(donationModel);
            _dbContext.Donations.Add(donation);
            _dbContext.SaveChanges();
            return donation.Id;
        }

        public void DeleteDonation(int id)
        {
            var donation = _dbContext.Donations.Where(d => d.Id == id).FirstOrDefault();
            donation.Investor.User.Status = Convert.ToString(StatusEnumModel.Closed);
            _dbContext.SaveChanges();
        }

        public async Task<List<InvestorInformationModel>> GetAllInvestorInformations()
        {
            List<InvestorInformationModel> investorInformationModels = new List<InvestorInformationModel>();
            var investors = await _dbContext.InvestorInformations.ToListAsync();
            foreach (var investor in investors)
            {
                var InvestorModel = _mapper.Map<InvestorInformation, InvestorInformationModel>(investor);
                investorInformationModels.Add(InvestorModel);

            }
            return investorInformationModels;
        }

        public async Task<InvestorInformationModel> GetInvestorInformationById(int id)
        {
            var investor = await _dbContext.InvestorInformations.Where(d => d.Id == id).FirstOrDefaultAsync();
            if (investor != null)
            {
                return _mapper.Map<InvestorInformation, InvestorInformationModel>(investor);
            }
            return null;
        }

        public async Task<List<DonationModel>> GetAllDonation()
        {
            List<DonationModel> donationModels = new List<DonationModel>();
            var donations = await _dbContext.Donations.ToListAsync();
            foreach (var donation in donations)
            {
                var donationModel = _mapper.Map<Donation, DonationModel>(donation);
                donationModels.Add(donationModel);
            }
            return donationModels;
        }

        public async Task<DonationModel> GetDonationById(int id)
        {
            var donation = await _dbContext.Donations.Where(d => d.Id == id).FirstOrDefaultAsync();
            if (donation != null)
            {
                return _mapper.Map<Donation, DonationModel>(donation);
            }
            return null;
        }
        public async Task<DonationModel> ModifyDonation(DonationModel donationModel, int id)
        {
            var donation = await _dbContext.Donations.Where(d => d.Id == id).FirstOrDefaultAsync();
            if (donation != null)
            {
                var modifyDonation = _mapper.Map<DonationModel, Donation>(donationModel);
                _dbContext.SaveChanges();
                var newDonation = _mapper.Map<Donation, DonationModel>(modifyDonation);
                return newDonation;
            }
            return null;
        }

        public async Task<List<DonationModel>> GetDonationByYear(DateTime dateTimeStart,DateTime dateTimeFinish)
        {
            if ((dateTimeStart >= DateTime.UtcNow || dateTimeFinish >= DateTime.UtcNow) && dateTimeStart >= dateTimeFinish )
            {
                return null;
            }
            var donations = await _dbContext.Donations.Where(don => (don.DateOfCharity == dateTimeStart) &&
            (don.DateOfCharity == dateTimeFinish)).ToListAsync();
            if (donations.Count == 0)
            {
                return null;
            }
            List<DonationModel> DonationModels = new List<DonationModel>();
            foreach (var donation in donations)
            {
                var donationModel = _mapper.Map<Donation, DonationModel>(donation);
                DonationModels.Add(donationModel);
            }
            return DonationModels;
        }
    }
}
