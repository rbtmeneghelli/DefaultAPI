using DefaultAPI.Domain.Base;
using DefaultAPI.Domain.Enums;
using System.Collections.Generic;

namespace DefaultAPI.Domain.Entities
{
    public class Role : BaseEntity
    {
        public string Description { get; set; }
        public string RoleTag { get; set; }
        public EnumActions Action { get; set; }
        public long? IdFuncionalidade { get; set; }
        public virtual Operation Operation { get; set; }
    }
}