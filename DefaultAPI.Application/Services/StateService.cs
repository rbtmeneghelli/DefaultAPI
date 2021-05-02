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
    public class StatesService : IStatesService
    {
        public readonly IRepository<States> _stateRepository;

        public StatesService(IRepository<States> stateRepository)
        {
            _stateRepository = stateRepository;
        }

        public Task AddStates(List<States> list)
        {
            _stateRepository.AddRange(list);
            _stateRepository.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<long> GetStateByInitials(string initials)
        {
            var state = await _stateRepository.GetAll().FirstOrDefaultAsync(x => x.Sigla == initials);
            return state is not null ? state.Id.Value : 0;
        }

        public async Task<List<States>> GetAllStates()
        {
            return await _stateRepository.GetAll().ToListAsync();
        }

        public async Task RefreshStates(List<States> listStates, List<States> listStatesAPI, List<Region> listRegion)
        {
            try
            {
                foreach (var item in listStatesAPI)
                {
                    States state = listStates.FirstOrDefault(x => x.Sigla == item.Sigla && x.IsActive == true);
                    if (state != null)
                    {
                        state.UpdateTime = DateTime.Now;
                        state.Nome = item.Nome;
                        state.Sigla = item.Sigla;
                        _stateRepository.Update(state);
                    }
                    else
                    {
                        state = new States();
                        state.IsActive = true;
                        state.CreatedTime = DateTime.Now;
                        state.Nome = item.Nome;
                        state.Sigla = item.Sigla;
                        state.IdRegiao = listRegion.FirstOrDefault(x => x.Sigla == item.Regiao.Sigla).Id ?? 0;
                        _stateRepository.Add(state);
                    }
                    _stateRepository.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public async Task<bool> UpdateStatusById(long id)
        {
            try
            {
                States record = await Task.FromResult(_stateRepository.GetById(id));
                if (record != null)
                {
                    record.IsActive = record.IsActive == true ? false : true;
                    _stateRepository.Update(record);
                    _stateRepository.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<States> GetAllWithLike(string parametro) => _stateRepository.GetAll().Where(x => EF.Functions.Like(x.Nome, $"%{parametro}%")).ToList();

        public async Task<StatePagedReturned> GetAllWithPaginate(StateFilter filter)
        {
            try
            {
                var query = await GetAllWithFilter(filter);
                var queryCount = await GetCount(filter);

                var queryResult = from x in query.AsQueryable()
                                  orderby x.Nome ascending
                                  select new States
                                  {
                                      Id = x.Id,
                                      Nome = x.Nome,
                                      Sigla = x.Sigla,
                                      IsActive = x.IsActive
                                  };

                filter.pageIndex++;
                return new StatePagedReturned
                {
                    States = queryResult.Skip((filter.pageIndex - 1) * filter.pageSize).Take(filter.pageSize).ToList(),
                    NextPage = (filter.pageSize * filter.pageIndex) >= queryCount ? null : (int?)filter.pageIndex + 1,
                    Page = filter.pageIndex,
                    Total = (int)Math.Ceiling((decimal)queryCount / filter.pageSize),
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        private async Task<IQueryable<States>> GetAllWithFilter(StateFilter filter)
        {
            return await Task.FromResult(_stateRepository.GetAll().Where(GetPredicate(filter)).AsQueryable());
        }

        private async Task<int> GetCount(StateFilter filter)
        {
            return await _stateRepository.GetAll().CountAsync(GetPredicate(filter));
        }

        private Expression<Func<States, bool>> GetPredicate(StateFilter filter)
        {
            return p =>
                   (string.IsNullOrWhiteSpace(filter.Nome) || p.Nome.Trim().ToUpper().Contains(filter.Nome.Trim().ToUpper()));
        }
    }
}
