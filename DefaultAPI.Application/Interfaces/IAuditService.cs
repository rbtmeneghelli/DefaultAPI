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
    public interface IAuditService
    {
        Task<Audit> GetById(long id);
        List<Audit> GetAllWithLike(string parametro);
        Task<PagedResult<AuditReturnedDto>> GetAllWithPaginate(AuditFilter filter);
    }
}
