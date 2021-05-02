using DefaultAPI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Dto
{
    public class LogReturnedDto : BaseDto
    {
        public string Class { get; set; }
        public string Method { get; set; }
        public string MessageError { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Object { get; set; }
        public override string ToString() => $"Classe: {Class}";
    }

    public class LogSendDto : BaseDto
    {
        public string Class { get; set; }
        public string Method { get; set; }
        public string MessageError { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Object { get; set; }
        public override string ToString() => $"Estado: {Class}";
    }
}
