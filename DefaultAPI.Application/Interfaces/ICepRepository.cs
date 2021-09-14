using DefaultAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DefaultAPI.Application.Interfaces
{
    public interface ICepRepository : IRepository<Ceps>
    {
        Task<List<Ceps>> GetAllWithLike(string parametro);
    }
}
