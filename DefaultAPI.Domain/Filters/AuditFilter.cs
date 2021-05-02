using DefaultAPI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Filters
{
    public class AuditFilter : BaseFilter
    {
        public string TableName { get; set; }
    }
}
