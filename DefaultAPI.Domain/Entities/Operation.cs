using DefaultAPI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Entities
{
    public class Operation : BaseEntity
    {
        public string Description { get; set; }
        public virtual List<ProfileOperation> ProfileOperations { get; set; }
        public virtual List<Role> Roles { get; set; }
    }
}
