using LML.NPOManagement.Common;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface INotificationService
    {
        Task<List<NotificationModel>> GetAllNotifications();
        Task<NotificationModel> GetNotificationById(int id);
        Task<NotificationModel> AddNotification(NotificationModel notificationModel);
        Task<NotificationModel> ModifyNotification(NotificationModel notificationModel, int id);
        public void DeleteNotification(int id);
        public void SendNotifications(List<UserModel> userModels, NotificationModel notificationModel, string body);
        public Task<bool> SendNotificationUserAsync(UserModel userModel, NotificationModel notificationModel, string body);
        public void SendNotificationInvestor(DonationModel donationModel, NotificationModel notificationModel, string body);
        public void PasswordRecoverRequest(UserModel user);
        public void EmailVerificationRequest(UserModel user);
        public void EmailVerificationConfirmation(UserModel user);


    }
}
