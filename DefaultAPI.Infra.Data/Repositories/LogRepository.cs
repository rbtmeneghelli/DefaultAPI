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
    public class LogRepository : Repository<Log>, ILogRepository
    {
        public LogRepository(DefaultAPIContext context) : base(context) { }

        public async Task<List<Log>> GetAllWithLike(string parametro) => await _context.Log.AsNoTracking().Where(x => EF.Functions.Like(x.Class, $"%{parametro}%")).ToListAsync();
    }
}
