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
    public class TemplateTypeService : ITemplateTypeService
    {
        private IMapper _mapper;
        public TemplateTypeService()
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
                cfg.CreateMap<InvestorTier, InvestorTierModel>();
                cfg.CreateMap<MeetingSchedule, MeetingScheduleModel>();
                cfg.CreateMap<Notification, NotificationModel>();
                cfg.CreateMap<NotificationType, NotificationTypeModel>();
                cfg.CreateMap<Template, TemplateModel>();
                cfg.CreateMap<TemplateType, TemplateTypeModel>();
                cfg.CreateMap<UserInformation, UserInformationModel>();
                cfg.CreateMap<UserInventory, UserInventoryModel>();
                cfg.CreateMap<User, UserModel>();
                cfg.CreateMap<UserType, UserTypeModel>();
                cfg.CreateMap<WeeklySchedule, WeeklyScheduleModel>();
                cfg.CreateMap<AccountProgressModel, AccountProgress>();
                cfg.CreateMap<AttachmentModel, Attachment>();
                cfg.CreateMap<DailyScheduleModel, DailySchedule>();
                cfg.CreateMap<DonationModel, Donation>();
                cfg.CreateMap<AccountModel, Account>();
                cfg.CreateMap<InvestorInformationModel, InvestorInformation>();
                cfg.CreateMap<InventoryTypeModel, InventoryType>();
                cfg.CreateMap<InvestorTierModel, InvestorTier>();
                cfg.CreateMap<MeetingScheduleModel, MeetingSchedule>();
                cfg.CreateMap<NotificationModel, Notification>();
                cfg.CreateMap<NotificationTypeModel, NotificationType>();
                cfg.CreateMap<TemplateModel, Template>();
                cfg.CreateMap<TemplateTypeModel, TemplateType>();
                cfg.CreateMap<UserInformationModel, UserInformation>();
                cfg.CreateMap<UserInventoryModel, UserInventory>();
                cfg.CreateMap<UserModel, User>();
                cfg.CreateMap<UserTypeModel, UserType>();
                cfg.CreateMap<WeeklyScheduleModel, WeeklySchedule>();
            });
            _mapper = config.CreateMapper();
        }

        public int AddTemplateType(TemplateTypeModel templateTypeModel)
        {
            throw new NotImplementedException();
        }

        public void DeleteTemplateType(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TemplateTypeModel> GetAllTemplateTypes()
        {
            throw new NotImplementedException();
        }

        public TemplateTypeModel GetTemplateTypeById(int id)
        {
            throw new NotImplementedException();
        }

        public int ModifyTemplateType(TemplateTypeModel templateTypeModel, int id)
        {
            throw new NotImplementedException();
        }
    }
}
