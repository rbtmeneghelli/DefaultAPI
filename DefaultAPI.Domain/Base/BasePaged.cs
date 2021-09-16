using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Base
{
    public abstract class BasePaged
    {
        public int? NextPage { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int PageCount { get; set; }
    }
}
