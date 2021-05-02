using DefaultAPI.Domain.Base;
using DefaultAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Filters
{
    public class CepPagedReturned : BasePaged
    {
        public List<Ceps> Ceps { get; set; }
    }
}
