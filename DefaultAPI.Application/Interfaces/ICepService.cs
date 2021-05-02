using DefaultAPI.Domain.Entities;
using DefaultAPI.Domain.Filters;
using DefaultAPI.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DefaultAPI.Application.Interfaces
{
    public interface ICepService
    {
        Task RefreshCep(string cep, Ceps modelCep, Ceps modelCepAPI);
        Task<Ceps> GetByCep(string cep);
        Task<bool> UpdateStatusById(long id);
        List<Ceps> GetAllWithLike(string parametro);
        Task<CepPagedReturned> GetAllWithPaginate(CepFilter filter);
    }
}
