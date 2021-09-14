using DefaultAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DefaultAPI.Application.Interfaces
{
    public interface IAuditRepository : IRepository<Audit>
    {
        Task<List<Audit>> GetAllWithLike(string parametro);
    }
}
