using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Base
{
    public class BasePaged
    {
        public int? NextPage { get; set; }
        public int Page { get; set; }
        public int Total { get; set; }

        public int TotalRecords { get; set; }
    }
}
