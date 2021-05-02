using DefaultAPI.Domain.Base;
using DefaultAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Filters
{
    public class RegionPagedReturned : BasePaged
    {
        public List<Region> Regions { get; set; }
    }
}
