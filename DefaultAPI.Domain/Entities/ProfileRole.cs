using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Entities
{
    public class ProfileRole
    {
        public long IdProfile { get; set; }

        public virtual Profile Profile { get; set; }

        public long IdRole { get; set; }

        public virtual Role Role { get; set; }
    }
}
