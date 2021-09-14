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
    public class AuditRepository : Repository<Audit>, IAuditRepository
    {
        public AuditRepository(DefaultAPIContext context) : base(context) { }

        public async Task<List<Audit>> GetAllWithLike(string parametro) => await _context.Audit.AsNoTracking().Where(x => EF.Functions.Like(x.TableName, $"%{parametro}%")).ToListAsync();
    }
}
