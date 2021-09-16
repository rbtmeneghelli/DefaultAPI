using DefaultAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Application.Interfaces
{
    public interface ILoggerService<T> where T : class
    {
        void Handle(LogMessage message);
        List<LogMessage> GetLoggerMessages();
        bool HaveLogMessage();
    }
}
