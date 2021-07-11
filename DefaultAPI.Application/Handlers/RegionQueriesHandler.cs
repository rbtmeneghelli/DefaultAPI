using DefaultAPI.Application.Interfaces;
using DefaultAPI.Application.Queries;
using DefaultAPI.Domain.Entities;
using DefaultAPI.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DefaultAPI.Application.Handlers
{
    public class RegionQueriesHandler : IRequestHandler<RegionQueryById, Region>, IRequestHandler<RegionQueryAll, IEnumerable<Region>>, IRequestHandler<RegionQueryPage, PagedResult<Region>>
    {
        private IRegionService _regionService;

        public RegionQueriesHandler(IRegionService regionService)
        {
            _regionService = regionService;
        }

        public async Task<Region> Handle(RegionQueryById request, CancellationToken cancellationToken)
        {
            var list = await _regionService.GetAllRegion();
            if (list != null)
            {
                return list[0];
            }
            return null;
        }

        public async Task<IEnumerable<Region>> Handle(RegionQueryAll request, CancellationToken cancellationToken)
        {
            var list = await _regionService.GetAllRegion();
            if (list != null)
            {
                return list;
            }
            return null;
        }

        public async Task<PagedResult<Region>> Handle(RegionQueryPage request, CancellationToken cancellationToken)
        {
            var list = await _regionService.GetAllWithPaginate(request);
            if (list != null)
            {
                return list;
            }
            return null;
        }
    }
}