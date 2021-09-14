using DefaultAPI.Application.Interfaces;
using DefaultAPI.Domain.Entities;
using DefaultAPI.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultAPI.Infra.Data.Repositories
{
    public class StateRepository : Repository<States>, IStatesRepository
    {
        public StateRepository(DefaultAPIContext context) : base(context) { }

        public async Task<List<States>> GetAllWithLike(string parametro) => await _context.State.AsNoTracking().Where(x => EF.Functions.Like(x.Nome, $"%{parametro}%")).ToListAsync();
    }
}
