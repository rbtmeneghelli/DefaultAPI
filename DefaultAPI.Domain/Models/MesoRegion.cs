using DefaultAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Models
{
    public class MesoRegion
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public States UF { get; set; }
    }
}
