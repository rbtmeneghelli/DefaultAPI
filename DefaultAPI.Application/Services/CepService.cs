using DefaultAPI.Application.Factory;
using DefaultAPI.Application.Interfaces;
using DefaultAPI.Domain.Entities;
using DefaultAPI.Domain.Filters;
using DefaultAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DefaultAPI.Application.Services
{
    public class CepService : BaseService, ICepService
    {
        public readonly ICepRepository _cepRepository;

        public CepService(ICepRepository cepRepository, INotificationMessageService notificationMessageService): base(notificationMessageService)
        {
            _cepRepository = cepRepository;
        }

        public async Task RefreshCep(string cep, Ceps modelCep, Ceps modelCepAPI)
        {
            try
            {
                if (modelCep != null)
                {

                    modelCep = new Ceps(modelCep.Id.Value, cep, modelCepAPI, modelCep.IdEstado, modelCep.CreatedTime);
                    _cepRepository.Update(modelCep);
                    _cepRepository.SaveChanges();
                }
                else
                {
                    modelCep = new Ceps(cep, modelCepAPI);
                    _cepRepository.Add(modelCep);
                    _cepRepository.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                await Task.CompletedTask;
            }
        }

        public async Task<Ceps> GetByCep(string cep)
        {
            return await _cepRepository.FindBy(x => x.Cep == cep).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateStatusById(long id)
        {
            try
            {
                Ceps record = await Task.FromResult(_cepRepository.GetById(id));
                if (record != null)
                {
                    record.IsActive = record.IsActive == true ? false : true;
                    _cepRepository.Update(record);
                    _cepRepository.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Ceps>> GetAllWithLike(string parametro) => await _cepRepository.GetAllWithLike(parametro);

        public async Task<PagedResult<Ceps>> GetAllWithPaginate(CepFilter filter)
        {
            try
            {
                var query = await GetAllWithFilter(filter);
                var queryCount = await GetCount(filter);

                var queryResult = from x in query.AsQueryable()
                                  orderby x.Logradouro ascending
                                  select new Ceps
                                  {
                                      Id = x.Id,
                                      Cep = x.Cep,
                                      Logradouro = x.Logradouro,
                                      Bairro = x.Bairro,
                                      Complemento = x.Complemento,
                                      Ddd = x.Ddd,
                                      Uf = x.Uf,
                                      Gia = x.Gia,
                                      Ibge = x.Ibge,
                                      Localidade = x.Localidade,
                                      Siafi = x.Siafi,
                                      IsActive = x.IsActive
                                  };

                return PagedFactory.GetPaged(queryResult, filter.pageIndex, filter.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        private async Task<IQueryable<Ceps>> GetAllWithFilter(CepFilter filter)
        {
            return await Task.FromResult(_cepRepository.GetAll().Where(GetPredicate(filter)).AsQueryable());
        }

        private async Task<int> GetCount(CepFilter filter)
        {
            return await _cepRepository.GetAll().CountAsync(GetPredicate(filter));
        }

        private Expression<Func<Ceps, bool>> GetPredicate(CepFilter filter)
        {
            return p =>
                   (string.IsNullOrWhiteSpace(filter.Cep) || p.Cep.Trim().ToUpper().Contains(filter.Cep.Trim().ToUpper()));
        }

        public void Dispose()
        {
            _cepRepository?.Dispose();
        }
    }
}
