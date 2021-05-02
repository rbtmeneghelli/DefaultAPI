using DefaultAPI.Domain.Base;
using DefaultAPI.Domain.Dto;
using DefaultAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Filters
{
    public class AuditPagedReturned : BasePaged
    {
        public List<AuditReturnedDto> Audits { get; set; }
    }
}
