using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BC = BCrypt.Net.BCrypt;

namespace LML.NPOManagement.Bll.Services
{
    public class UserService : IUserService
    {
        private IMapper _mapper;
        public UserService()
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
                cfg.CreateMap<User, UserModel>();
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
                cfg.CreateMap<UserModel, User>();
                cfg.CreateMap<WeeklyScheduleModel, WeeklySchedule>();
            });
            _mapper = config.CreateMapper();
        }

        public int AddUser(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public UserModel GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserModel> Login(UserModel userModel, IConfiguration configuration)
        {
            using (var dbContext = new NPOManagementContext())
            {
                string encPass = BC.HashPassword(userModel.Password);

                var user = await dbContext.Users.FirstOrDefaultAsync(m => m.Email == userModel.Email);
                if (user != null && BC.Verify(userModel.Password, user.Password))
                {
                    var userModelMapper = _mapper.Map<User, UserModel>(user);
                    userModelMapper.Password = null;
                    userModelMapper.Token = TokenCreationHelper.GenerateJwtToken(userModelMapper, configuration);
                    return userModelMapper;
                }
            }
            return null;
        }

        public int ModifyUser(UserModel userModel, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Registration(UserModel userModel, IConfiguration configuration)
        {
            using (var dbContext = new NPOManagementContext())
            {
                string encPass = BC.HashPassword(userModel.Password);

                var user = await dbContext.Users.FirstOrDefaultAsync(m => m.Email == userModel.Email);
                if (user == null )
                {
                    return false;
                }
            }
            return true;
        }
        public async Task<int> UserInformationRegistration(UserInformationModel userInformationModel)
        {
            using(var dbContext = new NPOManagementContext())
            {
                var userInfo = _mapper.Map<UserInformationModel, UserInformation>(userInformationModel);
                dbContext.UserInformations.Add(userInfo);
                dbContext.SaveChanges();
                return userInfo.Id;
            }
        }
    }
}
