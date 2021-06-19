using DefaultAPI.Domain.Entities;
using DefaultAPI.Domain.Filters;
using DefaultAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultAPI.Application.Interfaces
{
    public interface IRegionService
    {
        Task AddRegions(List<Region> list);

        Task<List<Region>> GetAllRegion();

        Task RefreshRegion(List<States> listStatesAPI);

        Task<bool> UpdateStatusById(long id);

        List<Region> GetAllWithLike(string parametro);

        Task<PagedResult<Region>> GetAllWithPaginate(RegionFilter filter);
    }
}
