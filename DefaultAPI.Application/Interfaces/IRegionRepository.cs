using DefaultAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DefaultAPI.Application.Interfaces
{
    public interface IRegionRepository : IRepository<Region>
    {
        Task<List<Region>> GetAllWithLike(string parametro);
    }
}
