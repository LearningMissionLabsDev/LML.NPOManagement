using LML.NPOManagement.Bll.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface INotificationTypeService
    {
        public IEnumerable<NotificationTypeModel> GetAllNotificationTypes();
        public NotificationTypeModel GetNotificationTypeById(int id);
        public int AddNotificationType(NotificationTypeModel notificationTypeModel);
        public int ModifyNotificationType(NotificationTypeModel notificationTypeModel, int id);
        public void DeleteNotificationType(int id);
    }
}
