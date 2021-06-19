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
    public class RegionService : IRegionService
    {
        private readonly IRepository<Region> _regionRepository;

        public RegionService(IRepository<Region> regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public async Task<List<Region>> GetAllRegion()
        {
            return await _regionRepository.GetAll().ToListAsync();
        }

        public Task AddRegions(List<Region> list)
        {
            _regionRepository.AddRange(list);
            _regionRepository.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task RefreshRegion(List<States> listStatesAPI)
        {
            try
            {
                List<Region> listRegion = await _regionRepository.GetAll().ToListAsync();
                List<Region> tmpRegion = listStatesAPI.Select(x => new Region()
                {
                    Nome = x.Regiao.Nome,
                    Sigla = x.Regiao.Sigla,
                    IsActive = true,
                    CreatedTime = DateTime.Now
                }).ToList();

                List<Region> listaRegiaoAPI = tmpRegion.GroupBy(x => new { x.Nome, x.Sigla }).Select(g => g.First()).ToList();

                foreach (var item in listaRegiaoAPI)
                {

                    Region region = listRegion.FirstOrDefault(x => x.Sigla == item.Sigla && x.IsActive == true);
                    if (region != null)
                    {
                        region.UpdateTime = DateTime.Now;
                        region.Nome = item.Nome;
                        region.Sigla = item.Sigla;
                        _regionRepository.Update(region);
                    }
                    else
                    {
                        _regionRepository.Add(item);
                    }
                    _regionRepository.SaveChanges();
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
                Region record = await Task.FromResult(_regionRepository.GetById(id));
                if (record != null)
                {
                    record.IsActive = record.IsActive == true ? false : true;
                    _regionRepository.Update(record);
                    _regionRepository.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Region> GetAllWithLike(string parametro) => _regionRepository.GetAll().Where(x => EF.Functions.Like(x.Nome, $"%{parametro}%")).ToList();

        public async Task<PagedResult<Region>> GetAllWithPaginate(RegionFilter filter)
        {
            try
            {
                var query = await GetAllWithFilter(filter);
                var queryCount = await GetCount(filter);

                var queryResult = from x in query.AsQueryable()
                                  orderby x.Nome ascending
                                  select new Region
                                  {
                                      Id = x.Id,
                                      Nome = x.Nome,
                                      Sigla = x.Sigla,
                                      IsActive = x.IsActive
                                  };

                return PagedFactory.GetPaged(queryResult, filter.pageIndex, filter.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        private async Task<IQueryable<Region>> GetAllWithFilter(RegionFilter filter)
        {
            return await Task.FromResult(_regionRepository.GetAll().Where(GetPredicate(filter)).AsQueryable());
        }

        private async Task<int> GetCount(RegionFilter filter)
        {
            return await _regionRepository.GetAll().CountAsync(GetPredicate(filter));
        }

        private Expression<Func<Region, bool>> GetPredicate(RegionFilter filter)
        {
            return p =>
                   (string.IsNullOrWhiteSpace(filter.Nome) || p.Nome.Trim().ToUpper().Contains(filter.Nome.Trim().ToUpper()));
        }
    }
}
