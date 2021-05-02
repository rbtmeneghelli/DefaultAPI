using DefaultAPI.Domain.Entities;
using DefaultAPI.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DefaultAPI.Application.Interfaces
{
    public interface ILogService
    {
        Task<Log> GetById(long id);
        List<Log> GetAllWithLike(string parametro);
        Task<LogPagedReturned> GetAllWithPaginate(LogFilter filter);
    }
}
