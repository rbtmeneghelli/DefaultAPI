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
        private ResultReturned _resultReturned;

        public NotificationMessageService()
        {
            _notifications = new List<NotificationMessage>();
            _resultReturned = new ResultReturned();
        }

        public void Handle(NotificationMessage notification)
        {
            _notifications.Add(notification);
        }

        public void Handle(ResultReturned resultReturned)
        {
            _resultReturned = resultReturned;
        }

        public List<NotificationMessage> GetNotifications()
        {
            return _notifications;
        }

        public ResultReturned GetResultReturned()
        {
            return _resultReturned;
        }

        public bool HaveNotification()
        {
            return _notifications.Any();
        }
    }
}
