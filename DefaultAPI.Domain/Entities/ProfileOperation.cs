using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Entities
{
    public class ProfileOperation
    {
        public long IdProfile { get; set; }
        public virtual Profile Profile { get; set; }
        public long IdOperation { get; set; }
        public virtual Operation Operation { get; set; }
        public bool CanCreate { get; set; }
        public bool CanResearch { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanExport { get; set; }
        public bool CanImport { get; set; }
    }
}
