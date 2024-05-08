using AutoMapper;
using Grpc.Core;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Common;
using LML.NPOManagement.Common.Model;
using LML.NPOManagement.Dal;
using LML.NPOManagement.Dal.Models;
using LML.NPOManagement.Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace LML.NPOManagement.Bll.Services
{
    public class NotificationService : INotificationService
    {
        private IMapper _mapper;
        //private readonly INotificationRepository _notificationRepository;
        private readonly NpomanagementContext _dbContext;

        public NotificationService(/*INotificationRepository notificationRepository*/)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Dal.Models.Attachment, AttachmentModel>();
                cfg.CreateMap<Donation, DonationModel>();
                cfg.CreateMap<Account, AccountModel>();
                cfg.CreateMap<InvestorInformation, InvestorInformationModel>();
                cfg.CreateMap<InventoryType, InventoryTypeModel>();
                cfg.CreateMap<Notification, NotificationModel>();
                cfg.CreateMap<UserInformation, UserInformationModel>();
                cfg.CreateMap<UserInventory, UserInventoryModel>();
                //cfg.CreateMap<UserType, UserTypeModel>();
                cfg.CreateMap<AttachmentModel, Dal.Models.Attachment>();
                cfg.CreateMap<DonationModel, Donation>();
                cfg.CreateMap<AccountModel, Account>();
                cfg.CreateMap<InvestorInformationModel, InvestorInformation>();
                cfg.CreateMap<InventoryTypeModel, InventoryType>();
                cfg.CreateMap<NotificationModel, Notification>();
                cfg.CreateMap<UserInformationModel, UserInformation>();
                cfg.CreateMap<UserInventoryModel, UserInventory>();
                //cfg.CreateMap<UserTypeModel, UserType>();
                cfg.CreateMap<User, UserModel>();
            });
            _mapper = config.CreateMapper();
            //_notificationRepository = notificationRepository;
        }

        public async Task<NotificationModel> AddNotification(NotificationModel notificationModel)
        {
            var notification = _mapper.Map<NotificationModel,Notification>(notificationModel);
            await _dbContext.Notifications.AddAsync(notification);
            await _dbContext.SaveChangesAsync();
            return notificationModel;
        }

        public void DeleteNotification(int id)
        {
            var notification = _dbContext.Notifications.Where(n => n.Id == id).FirstOrDefault();
            _dbContext.Notifications.Remove(notification);
            _dbContext.SaveChanges();
        }

        public async Task<List<NotificationModel>> GetAllNotifications()
        {
            List<NotificationModel> notificationModels = new List<NotificationModel>();
            var notifications = await _dbContext.Notifications.ToListAsync();
            if (notifications != null)
            {
                foreach (var notification in notifications)
                {
                    var notificationModel = _mapper.Map<Notification, NotificationModel>(notification);
                    notificationModels.Add(notificationModel);
                }
                return notificationModels;
            }
            return null;
        }

        public async Task<NotificationModel> GetNotificationById(int id)
        {
            var notification = await _dbContext.Notifications.Where(n => n.Id == id).FirstOrDefaultAsync();
            if (notification != null)
            {
                var notificationModel = _mapper.Map<Notification, NotificationModel>(notification);
                return notificationModel;
            }
            return null;
        }

        public async Task<NotificationModel> ModifyNotification(NotificationModel notificationModel, int id)
        {
            var notification = await _dbContext.Notifications.Where(n => n.Id == id).FirstOrDefaultAsync();
            if (notification == null)
            {
                return null;
            }
            notification.Id = id;
            notification.Subject = notificationModel.Subject;
            notification.AttachmentId = notificationModel.AttachmentId;
            notification.Body = notificationModel.Body;
            notification.Reminder = notificationModel.Reminder;
            notification.Metadata = notificationModel.Metadata;
            notification.MeetingSchedule = notificationModel.MeetingSchedule;
            notification.NotificationType.Description = notificationModel.NotificationTypeEnum.ToString();
            await _dbContext.SaveChangesAsync();
            var newNotificationModel =_mapper.Map<Notification,NotificationModel>(notification);
            return newNotificationModel;
        }

        public async void SendNotifications (List<UserModel> userModels, NotificationModel notificationModel, string body)
        {
            if(notificationModel.Subject == null)
            {
                notificationModel.Subject = HtmlSubject();
            }
            
            foreach (var userModel in userModels)
            {
                var userInfo = await _dbContext.UserInformations.Where(usi => usi.UserId == userModel.Id).FirstOrDefaultAsync();
                body = body.Replace("@firstName", userInfo.FirstName);
                body = body.Replace("@lastName", userInfo.LastName);
                SendNotification(body, notificationModel.Subject, userModel.Email);
            }           
        }

        public async void SendNotificationUserAsync(UserModel userModel, NotificationModel notificationModel, string body)
        {
            notificationModel.NotificationTypeEnum = NotificationTypeEnum.ByRegistration;
            if (notificationModel.Subject == null)
            {
                notificationModel.Subject = HtmlSubject();
            }
            var userInfo = await _dbContext.UserInformations.Where(usi => usi.UserId == userModel.Id).FirstOrDefaultAsync();
            body = body.Replace("@firstName", userInfo.FirstName);
            body = body.Replace("@lastName", userInfo.LastName);

            SendNotification(body, notificationModel.Subject, userModel.Email);
        }

        public async void SendNotificationInvestor(DonationModel donationModel, NotificationModel notificationModel, string body)
        {      
            var investor = await _dbContext.InvestorInformations.Where(inv => inv.Id == donationModel.InvestorId).FirstOrDefaultAsync();
            var user = await _dbContext.Users.Where(us => us.Id == investor.UserId).FirstOrDefaultAsync();
            var userModel = _mapper.Map<User, UserModel>(user);

            notificationModel.NotificationTypeEnum = NotificationTypeEnum.ByDonation;
            if(notificationModel.Subject == null)
            {
                notificationModel.Subject = HtmlSubject();
            }
            body = body.Replace("@amount", Convert.ToString(donationModel.Amount));
            body = body.Replace("@dateTime", Convert.ToString(donationModel.DateOfCharity));
            SendNotification(body, notificationModel.Subject, userModel.Email);
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

        public async void CheckingEmail(UserModel userModel, NotificationModel notificationModel, IConfiguration configuration, string body)
        {
            if (notificationModel.Subject == null)
            {
                notificationModel.Subject = HtmlSubject();
            }
            //string token = await TokenCreationHelper.GenerateJwtToken(userModel, configuration);
            //string clientVerificationURL = configuration.GetSection("AppSettings:ClientVerificationURL").Value;
            //var uri = $"{clientVerificationURL}?token={token}";
            //body = body.Replace("@verifiyCode", uri);
            //SendNotification(body, notificationModel.Subject, userModel.Email);
        }

        private string HtmlSubject()
        {
            return " Learning Mission ARMENIA ";
        }

    }
}
