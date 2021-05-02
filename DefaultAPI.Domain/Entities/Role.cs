using DefaultAPI.Domain.Base;
using System.Collections.Generic;

namespace DefaultAPI.Domain.Entities
{
    public class Role : BaseEntity
    {
        public string Description { get; set; }
        public string RoleTag { get; set; }
        public List<ProfileRole> ProfileRoles { get; set; }
    }
}
