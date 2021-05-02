using DefaultAPI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Entities
{
    public class Region : BaseEntity
    {
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public List<States> Estados { get; set; }
    }
}
