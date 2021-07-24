using DefaultAPI.Domain.Base;
using DefaultAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Entities
{
    public class Profile : BaseEntity
    {
        public string Description { get; set; }
        public EnumProfileType ProfileType { get; set; }
        public EnumAccessGroup AccessGroup { get; set; }
        public EnumLoginType LoginType { get; set; }
        public List<User> Users { get; set; }
        public List<ProfileOperation> ProfileOperations { get; set; }
    }
}
