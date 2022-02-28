﻿using LML.NPOManagement.Bll.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface INotificationService
    {
        public IEnumerable<NotificationModel> GetAllNotifications();
        public NotificationModel GetNotificationById(int id);
        public int AddNotification(NotificationModel notificationModel);
        public int ModifyNotification(NotificationModel notificationModel, int id);
        public void DeleteNotification(int id);
    }
}