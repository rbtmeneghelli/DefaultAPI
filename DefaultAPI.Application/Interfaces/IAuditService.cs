using DefaultAPI.Domain.Entities;
using DefaultAPI.Domain.Filters;
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
        Task<AuditPagedReturned> GetAllWithPaginate(AuditFilter filter);
    }
}
