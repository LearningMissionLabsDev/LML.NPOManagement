using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal;
using LML.NPOManagement.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace LML.NPOManagement.Bll.Services
{
    public class AccountService : IAccountService
    {
        private IMapper _mapper;
        private readonly INPOManagementContext _dbContext;
       
        public AccountService(INPOManagementContext context)
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
                cfg.CreateMap<Template, TemplateModel>();
                cfg.CreateMap<TemplateType, TemplateTypeModel>();
                cfg.CreateMap<UserInformation, UserInformationModel>();
                cfg.CreateMap<UserInventory, UserInventoryModel>();
                cfg.CreateMap<UserType, UserTypeModel>();
                cfg.CreateMap<AccountProgressModel, AccountProgress>();
                cfg.CreateMap<AttachmentModel, Attachment>();
                cfg.CreateMap<DailyScheduleModel, DailySchedule>();
                cfg.CreateMap<DonationModel, Donation>();
                cfg.CreateMap<AccountModel, Account>();
                cfg.CreateMap<InvestorInformationModel, InvestorInformation>();
                cfg.CreateMap<InventoryTypeModel, InventoryType>();
                cfg.CreateMap<MeetingScheduleModel, MeetingSchedule>();
                cfg.CreateMap<NotificationModel, Notification>();
                cfg.CreateMap<TemplateModel, Template>();
                cfg.CreateMap<TemplateTypeModel, TemplateType>();
                cfg.CreateMap<UserInformationModel, UserInformation>();
                cfg.CreateMap<UserInventoryModel, UserInventory>();
                cfg.CreateMap<UserTypeModel, UserType>();
                cfg.CreateMap<UserIdeaModel, UserIdea>();
                cfg.CreateMap<UserIdea, UserIdeaModel>();
            });
            _mapper = config.CreateMapper();
            _dbContext = context;
        }

        public int AddAccount(AccountModel accountModel)
        {
            throw new NotImplementedException();
        }

        public UserIdeaModel AddUserIdea(UserIdeaModel userIdeaModel)
        {
            //using(var dbContext = new NPOManagementContext())
            //{
                var idea =_mapper.Map<UserIdeaModel,UserIdea>(userIdeaModel);
                _dbContext.UserIdeas.Add(idea);
                _dbContext.SaveChanges();
                return userIdeaModel;
            //}
        }
        public List<UserIdeaModel> GetAllIdea()
        {
            //using (var dbContext = new NPOManagementContext())
            //{
                var ideas = _dbContext.UserIdeas.ToList();
                if(ideas.Count == 0)
                {
                    return null;
                }
                List<UserIdeaModel> userIdeaModels = new List<UserIdeaModel>();
                foreach (var idea in ideas)
                {
                    var ideaModel = _mapper.Map<UserIdea, UserIdeaModel>(idea);
                    userIdeaModels.Add(ideaModel);
                }
                return userIdeaModels;
            //}
        }

        public void DeleteAccount(int id)
        {
            //using (var dbContext = new NPOManagementContext())
            //{
                var delatAccount = _dbContext.Accounts.Where(da => da.Id == id).FirstOrDefault();
                if (delatAccount != null)
                {
                    _dbContext.SaveChanges();
                }
            //}
        }

        public AccountModel GetAccountById(int id)
        {
           // using (var dbContext = new NPOManagementContext())
           // {
                var account = _dbContext.Accounts.Where(acc => acc.Id == id).FirstOrDefault();
                if (account != null)
                {
                    var accountModel = _mapper.Map<Account, AccountModel>(account);
                    return accountModel;
                }
                return null;
           // }
        }

        public IEnumerable<AccountModel> GetAllAccounts()
        {
            //using (var dbContex = new NPOManagementContext())
            //{
                var accounts = _dbContext.Accounts.ToList();

                foreach (var account in accounts)
                {
                    var accountModel = _mapper.Map<Account, AccountModel>(account);
                    yield return accountModel;
                }
            //}
        }

        public int ModifyAccount(AccountModel accountModel, int id)
        {
            throw new NotImplementedException();
        }
    }
}
