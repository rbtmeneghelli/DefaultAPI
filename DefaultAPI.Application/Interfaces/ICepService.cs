﻿using DefaultAPI.Domain.Entities;
using DefaultAPI.Domain.Filters;
using DefaultAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DefaultAPI.Application.Interfaces
{
    public interface ICepService : IDisposable
    {
        Task RefreshCep(string cep, Ceps modelCep, Ceps modelCepAPI);
        Task<Ceps> GetByCep(string cep);
        Task<bool> UpdateStatusById(long id);
        Task<List<Ceps>> GetAllWithLike(string parametro);
        Task<PagedResult<Ceps>> GetAllWithPaginate(CepFilter filter);
    }
}
