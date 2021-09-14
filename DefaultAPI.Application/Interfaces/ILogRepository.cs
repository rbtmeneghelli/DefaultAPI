using DefaultAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DefaultAPI.Application.Interfaces
{
    public interface ILogRepository : IRepository<Log>
    {
        Task<List<Log>> GetAllWithLike(string parametro);
    }
}
