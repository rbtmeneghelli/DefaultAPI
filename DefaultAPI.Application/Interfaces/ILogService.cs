using DefaultAPI.Domain.Dto;
using DefaultAPI.Domain.Entities;
using DefaultAPI.Domain.Filters;
using DefaultAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DefaultAPI.Application.Interfaces
{
    public interface ILogService : IDisposable
    {
        Task<Log> GetById(long id);
        Task<List<Log>> GetAllWithLike(string parametro);
        Task<PagedResult<LogReturnedDto>> GetAllPaginate(LogFilter filter);
        Task<bool> ExistById(long id);
    }
}
