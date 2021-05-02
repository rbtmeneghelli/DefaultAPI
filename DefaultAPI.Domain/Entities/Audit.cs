using DefaultAPI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Entities
{
    public class Audit : BaseEntity
    {
        public string TableName { get; set; }
        public string ActionName { get; set; }
        public string KeyValues { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
    }
}
