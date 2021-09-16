using DefaultAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Models
{
    public class LogMessage
    {
        public string Message { get; }
        public EnumLogger Type { get; }

        public LogMessage(string message, EnumLogger type)
        {
            Message = message;
            Type = type;
        }
    }
}
