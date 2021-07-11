using DefaultAPI.Domain.Entities;
using DefaultAPI.Domain.Filters;
using DefaultAPI.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Application.Queries
{
    public class RegionQueryById : IRequest<Region>
    {
        public long? Id { get; set; }
    }

    public class RegionQueryAll : IRequest<IEnumerable<Region>>
    {
        public long? Id { get; set; }
    }

    public class RegionQueryPage : RegionFilter, IRequest<PagedResult<Region>>
    {
        public RegionQueryPage(RegionFilter regionFilter)
        {
            pageIndex = regionFilter.pageIndex;
            pageSize = regionFilter.pageSize;
        }
    }
}
