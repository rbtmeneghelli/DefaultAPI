using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Base
{
    public abstract class BaseEntity
    {
        public long? Id { get; set; }

        public DateTime? CreatedTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public bool IsActive { get; set; }
    }
}
