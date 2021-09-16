using DefaultAPI.Application.Interfaces;
using DefaultAPI.Domain.Enums;
using DefaultAPI.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DefaultAPI.Application.Services
{
    public class LoggerService<T> : ILoggerService<T> where T : class
    {
        private List<LogMessage> _loggerMessages;
        private readonly ILogger<T> _logger;

        public LoggerService(ILogger<T> logger)
        {
            _loggerMessages = new List<LogMessage>();
            _logger = logger;
        }

        public void Handle(LogMessage message)
        {
            _loggerMessages.Add(message);
        }

        public List<LogMessage> GetLoggerMessages()
        {
            _loggerMessages.ForEach(item =>
            {
                SaveLogMessageInKissLog(item);
            });

            return _loggerMessages;
        }

        public bool HaveLogMessage()
        {
            return _loggerMessages.Any();
        }

        private void SaveLogMessageInKissLog(LogMessage logMessage)
        {
            switch (logMessage.Type)
            {
                case EnumLogger.LogInformation:
                    _logger.LogInformation(logMessage.Message);
                    break;
                case EnumLogger.LogTrace:
                    _logger.LogTrace(logMessage.Message);
                    break;
                case EnumLogger.LogDebug:
                    _logger.LogDebug(logMessage.Message);
                    break;
                case EnumLogger.LogWarning:
                    _logger.LogWarning(logMessage.Message);
                    break;
                case EnumLogger.LogError:
                    _logger.LogError(logMessage.Message);
                    break;
                case EnumLogger.LogCritical:
                    _logger.LogCritical(logMessage.Message);
                    break;
            }
        }
    }
}
