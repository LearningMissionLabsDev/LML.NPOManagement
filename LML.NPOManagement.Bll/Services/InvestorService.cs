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
                cfg.CreateMap<AccountProgress, AccountProgressModel>();
                cfg.CreateMap<Attachment, AttachmentModel>();
                cfg.CreateMap<DailySchedule, DailyScheduleModel>();
                cfg.CreateMap<Donation, DonationModel>();
                cfg.CreateMap<Account, AccountModel>();
                cfg.CreateMap<InvestorInformation, InvestorInformationModel>();
                cfg.CreateMap<InventoryType, InventoryTypeModel>();
                cfg.CreateMap<MeetingSchedule, MeetingScheduleModel>();
                cfg.CreateMap<Notification, NotificationModel>();
                cfg.CreateMap<NotificationType, NotificationTypeModel>();
                cfg.CreateMap<Template, TemplateModel>();
                cfg.CreateMap<TemplateType, TemplateTypeModel>();
                cfg.CreateMap<UserInformation, UserInformationModel>();
                cfg.CreateMap<UserInventory, UserInventoryModel>();
                cfg.CreateMap<UserType, UserTypeModel>();
                cfg.CreateMap<WeeklySchedule, WeeklyScheduleModel>();
                cfg.CreateMap<AccountProgressModel, AccountProgress>();
                cfg.CreateMap<AttachmentModel, Attachment>();
                cfg.CreateMap<DailyScheduleModel, DailySchedule>();
                cfg.CreateMap<DonationModel, Donation>();
                cfg.CreateMap<AccountModel, Account>();
                cfg.CreateMap<InvestorInformationModel, InvestorInformation>();
                cfg.CreateMap<InventoryTypeModel, InventoryType>();
                cfg.CreateMap<MeetingScheduleModel, MeetingSchedule>();
                cfg.CreateMap<NotificationModel, Notification>();
                cfg.CreateMap<NotificationTypeModel, NotificationType>();
                cfg.CreateMap<TemplateModel, Template>();
                cfg.CreateMap<TemplateTypeModel, TemplateType>();
                cfg.CreateMap<UserInformationModel, UserInformation>();
                cfg.CreateMap<UserInventoryModel, UserInventory>();
                cfg.CreateMap<UserTypeModel, UserType>();
                cfg.CreateMap<WeeklyScheduleModel, WeeklySchedule>();
            });
            _mapper = config.CreateMapper();
        }

        public int AddInvestorInformation(InvestorInformationModel investorInformationModel)
        {
            throw new NotImplementedException();
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
            using(var dbContext = new NPOManagementContext())
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
            using(var dbContext = new NPOManagementContext())
            {
                var donation = dbContext.Donations.Where(d => d.Id == id).FirstOrDefault();
                if(donation != null)
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
