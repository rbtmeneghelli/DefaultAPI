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
    public class CepRepository : Repository<Ceps>, ICepRepository
    {
        public CepRepository(DefaultAPIContext context) : base(context) { }

        public async Task<List<Ceps>> GetAllWithLike(string parametro) => await _context.Cep.AsNoTracking().Where(x => EF.Functions.Like(x.Cep, $"%{parametro}%")).ToListAsync();
    }
}
