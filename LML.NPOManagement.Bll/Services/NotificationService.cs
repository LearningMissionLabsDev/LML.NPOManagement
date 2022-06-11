using AutoMapper;
using Grpc.Core;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal;
using LML.NPOManagement.Dal.Models;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace LML.NPOManagement.Bll.Services
{
    public class NotificationService : INotificationService
    {
        private IMapper _mapper;
        private readonly INPOManagementContext _dbContext;
        public string AppRootPath { get; set; }
        public NotificationService(INPOManagementContext context)
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
            _dbContext = context;
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
            TemplateService templateService = new TemplateService(AppRootPath,_dbContext);
            string subject = templateService.HtmlSubject();

            foreach (var userModel in userModels)
            {
                var body = templateService.HtmlBodyNotification(userModel ,notificationModel);
                SendNotification(body, subject, userModel.Email);
            }           
        }

        public void SendNotificationUser(UserModel userModel, NotificationModel notificationModel)
        {
            TemplateService templateService = new TemplateService(AppRootPath, _dbContext);
            notificationModel.NotificationTypeEnum = NotificationTypeEnum.ByRegistration;
            string subject = templateService.HtmlSubject();
            var body = templateService.HtmlBodyNotification(userModel,notificationModel);
            SendNotification(body, subject, userModel.Email);
        }

        public void SendNotificationInvestor(DonationModel donationModel, NotificationModel notificationModel)
        {      
            var investor = _dbContext.InvestorInformations.Where(inv => inv.Id == donationModel.InvestorId).FirstOrDefault();
            var user = _dbContext.Users.Where(us => us.Id == investor.UserId).FirstOrDefault();
            var userModel = _mapper.Map<User, UserModel>(user);

            TemplateService templateService = new TemplateService(AppRootPath, _dbContext);
            notificationModel.NotificationTypeEnum = NotificationTypeEnum.ByDonation;
            string subject = templateService.HtmlSubject();
            var body = templateService.HtmlBodyNotification(userModel, notificationModel);
            body = body.Replace("@amount", Convert.ToString(donationModel.Amount));
            body = body.Replace("@dateTime", Convert.ToString(donationModel.DateOfCharity));
            SendNotification(body, subject, userModel.Email);
 
        }
        
        private void SendNotification(string body, string subject, string email)
        {           
            String FROM = "learningmissionarmenia@gmail.com";
            String FROMNAME = "Learning Mission";                
            String TO = email;               
            String SMTP_USERNAME = "AKIAWNCW772FYFDWSEEG";
            String SMTP_PASSWORD = "BO2xx+FoiCvTo8jCSTyjijpHuF5gmT4eQcuJAQD3EwrE";       
            String HOST = "email-smtp.eu-west-1.amazonaws.com";                
            int PORT = 587;            
            String SUBJECT = subject;       
            String BODY = body;
            MailMessage message = new MailMessage();
            message.IsBodyHtml = true;
            message.From = new MailAddress(FROM, FROMNAME);
            message.To.Add(new MailAddress(TO));
            message.Subject = SUBJECT;
            message.Body = BODY;
            using (var client = new SmtpClient(HOST, PORT))
            {      
                client.Credentials = new NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);                
                client.EnableSsl = true;
                try
                {
                    Console.WriteLine("Attempting to send email...");
                    client.Send(message);
                    Console.WriteLine("Email sent!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("The email was not sent.");
                    Console.WriteLine("Error message: " + ex.Message);
                }
            } 
        }

        public void CheckingEmail(UserModel userModel, NotificationModel notificationModel, IConfiguration configuration)
        {
            TemplateService templateService = new TemplateService(AppRootPath, _dbContext);
            string subject = templateService.HtmlSubject();
            var body = templateService.HtmlBodyNotificationVerify(userModel, notificationModel, configuration);
            SendNotification(body, subject, userModel.Email);
        }
    }
}
