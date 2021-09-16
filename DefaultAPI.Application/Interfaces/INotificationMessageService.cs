using DefaultAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Application.Interfaces
{
    public interface INotificationMessageService
    {
        void Handle(NotificationMessage notificacao);
        List<NotificationMessage> GetNotifications();
        bool HaveNotification();
    }
}
