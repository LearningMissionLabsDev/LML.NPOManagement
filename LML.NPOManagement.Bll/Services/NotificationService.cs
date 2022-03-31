using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Mail;
using System.Text;

namespace LML.NPOManagement.Bll.Services
{
    public class NotificationService : INotificationService
    {
        private IMapper _mapper;
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
                cfg.CreateMap<NotificationType, NotificationTypeModel>();
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
           
            foreach (var userModel in userModels)
            {
                //userModel.Email = "lalazaryan91@inbox.ru";
                //MailMessage message = new MailMessage(from, userModel.Email);


                //string mailbody = notificationModel.Body;
                //message.Subject = notificationModel.Subject;
                //message.Body = mailbody;
                //message.BodyEncoding = Encoding.UTF8;
                //message.IsBodyHtml = true;
                //SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                //System.Net.NetworkCredential credential = new
                //System.Net.NetworkCredential("garush.mkhitaryan@gmail.com", "sat25111988");
                //client.EnableSsl = true;
                //client.UseDefaultCredentials = false;
                //client.Credentials = credential;
                //try
                //{
                //    client.Send(message);
                //}

                //catch (Exception ex)
                //{
                //    throw ex;
                //}
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("garush.mkhitaryan@gmail.com");
                mail.Sender = new MailAddress("garush.mkhitaryan@gmail.com");
                mail.To.Add(userModel.Email);
                mail.IsBodyHtml = true;
                mail.Subject = notificationModel.Subject;
                mail.Body = notificationModel.Body;


               
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.UseDefaultCredentials = false;

                smtp.Credentials = new System.Net.NetworkCredential("garush.mkhitaryan@gmail.com", "sat25111988");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = true;

                smtp.Timeout = 30000;
                try
                {

                    smtp.Send(mail);
                }
                catch (SmtpException ex)
                {
                    throw ex;
                }
            }
           
        }
      
    }
}
