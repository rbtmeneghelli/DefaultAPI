using DefaultAPI.Application.Interfaces;
using DefaultAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DefaultAPI.Application.Services
{
    public class NotificationMessageService : INotificationMessageService
    {
        private List<NotificationMessage> _notifications;

        public NotificationMessageService()
        {
            _notifications = new List<NotificationMessage>();
        }

        public void Handle(NotificationMessage notification)
        {
            _notifications.Add(notification);
        }

        public List<NotificationMessage> GetNotifications()
        {
            return _notifications;
        }

        public bool HaveNotification()
        {
            return _notifications.Any();
        }
    }
}
