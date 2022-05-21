using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal.Models;
using Microsoft.Extensions.Configuration;

namespace LML.NPOManagement.Bll.Services
{
    public class TemplateService : ITemplateService
    {
        private string _notificationTemplateRootPath;
        public TemplateService()
        {

        }

        public TemplateService(string appRootPath )
        {
            _notificationTemplateRootPath = Path.Combine(appRootPath + "NotificationTemplates");
        }

        public string HtmlBodyNotification(UserModel userModel, NotificationModel notificationModel)
        {
            using (var dbContext = new NPOManagementContext())
            {
                var html = string.Empty; 
                switch (notificationModel.NotificationTypeEnum)
                {
                    case NotificationTypeEnum.ByRoles:
                        html = Path.Combine(_notificationTemplateRootPath);
                        break;
                    case NotificationTypeEnum.ByAccounts:
                        html = Path.Combine(_notificationTemplateRootPath);
                        break;
                    case NotificationTypeEnum.ByInvestors:
                        html = Path.Combine(_notificationTemplateRootPath);
                        break;
                    case NotificationTypeEnum.ByIndividuals:
                        html = Path.Combine(_notificationTemplateRootPath);
                        break;
                    case NotificationTypeEnum.ByRegistration:
                        html = Path.Combine(_notificationTemplateRootPath + "/RegistracionNotification.html");
                        break;
                    case NotificationTypeEnum.ByDonation:
                        html = Path.Combine(_notificationTemplateRootPath + "/DonationNotification.html");
                        break;
                    default:
                        return null;
                }

                var user = dbContext.UserInformations.Where(us => us.UserId == userModel.Id).FirstOrDefault();               
                                
                var body = File.ReadAllText(html);
                body = body.Replace("@firstName", user.FirstName);
                body = body.Replace("@lastName", user.LastName);
                return body.ToString();
            }
        }
        public string HtmlBodyNotificationVerify(UserModel userModel, NotificationModel notificationModel, IConfiguration configuration,string body)
        {  

            string token = TokenCreationHelper.GenerateJwtToken(userModel, configuration);
            string clientVerificationURL = configuration.GetSection("AppSettings:ClientVerificationURL").Value;
            var uri =  $"{clientVerificationURL}?token={token}";
            body = body.Replace("@verifiyCode", uri);
            return body.ToString();  
        }

        public string HtmlSubject()
        {
            return " Learning Mission ARMENIA ";
        }
    }
}
