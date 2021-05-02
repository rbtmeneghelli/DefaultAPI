using DefaultAPI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }

        public long? IBGE { get; set; }

        public long IdState { get; set; }

        public virtual States States { get; set; }
    }
}
