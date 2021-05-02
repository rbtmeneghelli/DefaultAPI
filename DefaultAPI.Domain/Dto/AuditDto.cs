using DefaultAPI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Dto
{
    public class AuditReturnedDto : BaseDto
    {
        public string TableName { get; set; }
        public string ActionName { get; set; }
        public string KeyValues { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public DateTime? UpdateTime { get; set; }
        public override string ToString() => $"Tabela: {TableName}";
    }

    public class AuditSendDto : BaseDto
    {
        public string TableName { get; set; }
        public string ActionName { get; set; }
        public string KeyValues { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public DateTime? UpdateTime { get; set; }
        public override string ToString() => $"Tabela: {TableName}";
    }
}

