using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Base
{
    public abstract class BaseFilter
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}
