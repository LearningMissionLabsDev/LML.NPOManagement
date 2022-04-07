using LML.NPOManagement.Bll.Model;


namespace LML.NPOManagement.Bll.Interfaces
{
    public interface ITemplateService
    {
        public string HtmlBodyNorification(UserModel userModel,NotificationModel notificationModel);
        public string HtmlSubject();
    }
}
