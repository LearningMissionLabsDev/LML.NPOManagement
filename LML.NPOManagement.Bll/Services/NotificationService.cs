using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Services
{
    public class NotificationService : INotificationService
    {
        private IMapper _mapper;
        public NotificationService()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Notification, NotificationModel>();
                cfg.CreateMap<NotificationModel, Notification>();
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
    }
}
