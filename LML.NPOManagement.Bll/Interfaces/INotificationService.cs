﻿using LML.NPOManagement.Bll.Model;
using Microsoft.Extensions.Configuration;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface INotificationService
    {
        public string AppRootPath { get; set; }
        public IEnumerable<NotificationModel> GetAllNotifications();
        public NotificationModel GetNotificationById(int id);
        public int AddNotification(NotificationModel notificationModel);
        Task <bool> ModifyNotification(NotificationModel notificationModel, int id);
        public void DeleteNotification(int id);
        public void SendNotifications (List<UserModel> userModels, NotificationModel notificationModel);
        public void SendNotificationUser(UserModel userModel, NotificationModel notificationModel);
        public void SendNotificationInvestor(DonationModel donationModel, NotificationModel notificationModel);
        public void CheckingEmail(UserModel userModel, NotificationModel notificationModel, IConfiguration configuration);
    }
}
