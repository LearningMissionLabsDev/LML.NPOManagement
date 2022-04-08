using LML.NPOManagement.Bll.Model;


namespace LML.NPOManagement.Bll.Interfaces
{
    public interface ITemplateService
    {
        public string HtmlBodyNotification(UserModel userModel,NotificationModel notificationModel);
        public string HtmlSubject();
    }
}
