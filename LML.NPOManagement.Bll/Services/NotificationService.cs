using AutoMapper;
using Grpc.Core;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal.Models;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace LML.NPOManagement.Bll.Services
{
    public class NotificationService : INotificationService
    {
        private IMapper _mapper;
        public string AppRootPath { get; set; }
        public NotificationService()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountProgress, AccountProgressModel>();
                //cfg.CreateMap<Attachment, AttachmentModel>();
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
                cfg.CreateMap<WeeklySchedule, WeeklyScheduleModel>();
                cfg.CreateMap<AccountProgressModel, AccountProgress>();
                //cfg.CreateMap<AttachmentModel, Attachment>();
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
                cfg.CreateMap<WeeklyScheduleModel, WeeklySchedule>();
                cfg.CreateMap<User, UserModel>();
            });
            _mapper = config.CreateMapper();
        }

        public int AddNotification(NotificationModel notificationModel)
        {
            throw new NotImplementedException();
        }

        public void DeleteNotification(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NotificationModel> GetAllNotifications()
        {
            throw new NotImplementedException();
        }

        public NotificationModel GetNotificationById(int id)
        {
            throw new NotImplementedException();
        }

        public int ModifyNotification(NotificationModel notificationModel, int id)
        {
            throw new NotImplementedException();
        }

        public void SendNotifications (List<UserModel> userModels, NotificationModel notificationModel)
        {
            TemplateService templateService = new TemplateService(AppRootPath);
            string subject = templateService.HtmlSubject();

            foreach (var userModel in userModels)
            {
                string body = templateService.HtmlBodyNotification(userModel ,notificationModel);
                SendNotification(body, subject, userModel.Email);
            }           
        }

        public void SendNotificationUser(UserModel userModel, NotificationModel notificationModel)
        {
            TemplateService templateService = new TemplateService(AppRootPath);
            notificationModel.NotificationTypeEnum = NotificationTypeEnum.ByRegistration;
            string subject = templateService.HtmlSubject();
            string body = templateService.HtmlBodyNotification(userModel,notificationModel);
            SendNotification(body, subject, userModel.Email);
        }

        public void SendNotificationInvestor(DonationModel donationModel, NotificationModel notificationModel)
        {
            using(var dbContext = new NPOManagementContext())
            {
                var investor = dbContext.InvestorInformations.Where(inv => inv.Id == donationModel.InvestorId).FirstOrDefault();
                var user = dbContext.Users.Where(us => us.Id == investor.UserId).FirstOrDefault();
                var userModel = _mapper.Map<User, UserModel>(user);

                TemplateService templateService = new TemplateService(AppRootPath);
                notificationModel.NotificationTypeEnum = NotificationTypeEnum.ByDonation;
                string subject = templateService.HtmlSubject();
                string body = templateService.HtmlBodyNotification(userModel, notificationModel);
                body = body.Replace("@amount", Convert.ToString(donationModel.Amount));
                body = body.Replace("@dateTime", Convert.ToString(donationModel.DateOfCharity));
                SendNotification(body, subject, userModel.Email);
            }
        }
        
        private void SendNotification(string body, string subject, string email)
        {
            using (MailMessage EmailMsg = new MailMessage())
            {            
                EmailMsg.From = new MailAddress("learningmissionarmenia@gmail.com", "Learning Mission");
                EmailMsg.To.Add(new MailAddress(email, email));
                EmailMsg.Subject = subject;
                EmailMsg.Body = body;
                EmailMsg.IsBodyHtml = true;
                EmailMsg.Priority = MailPriority.Normal;
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    //Port = 465,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("learningmissionarmenia@gmail.com", "H@ghteluEnk21!")
                };
                smtp.Send(EmailMsg);
            }
        }

        public void CheckingEmail(UserModel userModel, NotificationModel notificationModel, IConfiguration configuration)
        {
            TemplateService templateService = new TemplateService(AppRootPath);
            string subject = templateService.HtmlSubject();
            string body = templateService.HtmlBodyNotificationVerify(userModel, notificationModel, configuration);
            SendNotification(body, subject, userModel.Email);
        }
    }
}
