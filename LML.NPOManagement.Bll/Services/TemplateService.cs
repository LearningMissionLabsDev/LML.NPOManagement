using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal.Models;

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
            _notificationTemplateRootPath = Path.Combine(appRootPath + "/NotificationTemplates");
        }

        public string HtmlBodyNorification(UserModel userModel, NotificationModel notificationModel)
        {
            using (var dbContext = new NPOManagementContext())
            {
                var html = "/RegistracionNotification.html";

                if (notificationModel != null)
                {
                    var notificationType = dbContext.NotificationTypes.Where(nt => nt.Id == notificationModel.Id).FirstOrDefault();
                    html = ReadHtmlFile(notificationType.Description);

                   
                }  
                var investor = dbContext.InvestorInformations.Where(inv => inv.UserId == userModel.Id).FirstOrDefault();
                if(investor != null)
                {
                    html = ".html";
                }

                var user = dbContext.UserInformations.Where(us => us.UserId == userModel.Id).FirstOrDefault();               

                string path = Path.Combine(_notificationTemplateRootPath + html);
                var body = System.IO.File.ReadAllText(path);
                body = body.Replace("@firstName", user.FirstName);
                body = body.Replace("@lastName", user.LastName);
                return body.ToString();

            }
            
        }

        public string HtmlSubject()
        {
            return " Learning Mission ARMENIA ";
        }

        private string ReadHtmlFile(string type)
        {          
            return type;
        }
    }
}
