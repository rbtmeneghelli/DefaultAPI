using DefaultAPI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Filters
{
    public class LogFilter : BaseFilter
    {
        public string Class { get; set; }
        public string Method { get; set; }
    }
}
