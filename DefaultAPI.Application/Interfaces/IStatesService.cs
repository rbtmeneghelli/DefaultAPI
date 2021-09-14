using DefaultAPI.Domain.Entities;
using DefaultAPI.Domain.Filters;
using DefaultAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DefaultAPI.Application.Interfaces
{
    public interface IStatesService : IDisposable
    {
        Task AddStates(List<States> list);
        Task<long> GetStateByInitials(string initials);
        Task<List<States>> GetAllStates();
        Task RefreshStates(List<States> listState, List<States> ListStateAPI, List<Region> listRegion);
        Task<bool> UpdateStatusById(long id);
        Task<List<States>> GetAllWithLike(string param);
        Task<PagedResult<States>> GetAllWithPaginate(StateFilter filter);
    }
}
