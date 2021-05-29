using DefaultAPI.Application.Interfaces;
using DefaultAPI.Domain;
using DefaultAPI.Domain.Dto;
using DefaultAPI.Domain.Entities;
using DefaultAPI.Domain.Filters;
using DefaultAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DefaultAPI.Application.Services
{
    public class CityService : ICityService
    {
        public readonly IRepository<City> _cityRepository;

        public CityService(IRepository<City> cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<List<long>> GetIdStates()
        {
            var list = await GetAllEntity();
            return (from x in list select x.IdState.Value).Distinct().ToList();
        }

        public async Task<CityPagedReturned> GetAllFromUf(int IdState = 25, int? page = 1, int? limit = int.MaxValue)
        {
            page = page ?? 1;
            limit = limit ?? 10;

            var set = await _cityRepository.GetAll()
                .Where(x => x.IdState == IdState)
                .OrderBy(a => a.Name)
                .Skip((page.Value - 1) * limit.Value)
                .Take(limit.Value)
                .Select(a => new CityReturnedDto
                {
                    Id = a.Id,
                    Name = a.Name
                }).ToListAsync();

            var total = set.Count();

            return new CityPagedReturned
            {
                Cities = set,
                NextPage = (page.Value * limit.Value) >= total ? null : (int?)page.Value + 1,
                Page = page.Value,
                Total = (int)Math.Ceiling((decimal)total / limit.Value),
                TotalRecords = total
            };
        }

        public async Task<List<CityReturnedDto>> GetAllEntity()
        {
            return await (from p in _cityRepository.FindBy(x => true).AsQueryable()
                          orderby p.Name ascending
                          select new CityReturnedDto
                          {
                              Id = p.Id,
                              Name = p.Name,
                              IBGE = p.IBGE,
                              StateDesc = p.States.Sigla,
                              IsActiveDesc = p.IsActive ? "Ativo" : "Inativo",
                              IdState = p.IdState
                          }).ToListAsync();
        }

        public async Task<CityReturnedDto> GetById(int id)
        {
            return await (from p in _cityRepository.FindBy(x => x.Id == id).AsQueryable()
                          orderby p.Name ascending
                          select new CityReturnedDto
                          {
                              Id = p.Id,
                              Name = p.Name,
                              IBGE = p.IBGE,
                              StateDesc = p.States.Sigla,
                              IsActiveDesc = p.IsActive ? "Ativo" : "Inativo",
                              IdState = p.IdState
                          }).FirstOrDefaultAsync();
        }

        public Task Add(City city)
        {
            city.CreatedTime = DateTime.Now;
            _cityRepository.Add(city);
            var added = _cityRepository.SaveChanges();
            return Task.CompletedTask;
        }

        public Task Remove(long id)
        {
            City city = _cityRepository.GetById(id);

            if (city is not null)
                _cityRepository.Remove(city);

            return Task.CompletedTask;
        }

        public Task Update(City city)
        {
            City entityBase = _cityRepository.FindBy(a => a.Id == city.Id).FirstOrDefault();

            if (entityBase != null)
            {
                entityBase.Name = city.Name;
                entityBase.IBGE = city.IBGE;
                entityBase.IdState = city.IdState;
                _cityRepository.Update(entityBase);
                _cityRepository.SaveChanges();
            }

            _cityRepository.Update(entityBase);
            var updated = _cityRepository.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<ResultReturned> AddOrUpdateCity(List<City> lista)
        {
            City entityBase = new City();
            try
            {
                List<City> listaEntityBase = await _cityRepository.GetAllTracking().ToListAsync();

                foreach (City city in lista)
                {
                    string stIBGE = city.IBGE.HasValue ? city.IBGE.Value.ToString() : "00000000";

                    if (listaEntityBase.Any(x => x.IBGE == city.IBGE && x.IdState == city.IdState))
                    {
                        entityBase = listaEntityBase.FirstOrDefault(x => x.IBGE == city.IBGE);
                        entityBase.Name = city.Name;
                        entityBase.IBGE = long.Parse(stIBGE);
                        entityBase.IdState = city.IdState;
                        entityBase.UpdateTime = DateTime.Now;
                        _cityRepository.Update(entityBase);
                        _cityRepository.SaveChanges();
                    }
                    else
                    {
                        entityBase = new City();
                        entityBase.Name = city.Name;
                        entityBase.IBGE = long.Parse(stIBGE);
                        entityBase.IdState = city.IdState;
                        entityBase.CreatedTime = DateTime.Now;
                        _cityRepository.Add(entityBase);
                        _cityRepository.SaveChanges();
                    }
                }

                return new ResultReturned(true, Constants.SuccessInAddCity);
            }
            catch (Exception ex)
            {
                return new ResultReturned(true, Constants.ErrorInAddCity);
            }
        }

        public async Task<bool> CheckCityExist(string city, long IdState)
        {
            return await Task.FromResult(_cityRepository.Exist(a => a.Name == city && a.IdState == IdState));
        }
    }
}