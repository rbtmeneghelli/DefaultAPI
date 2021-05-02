using DefaultAPI.Domain.Dto;
using DefaultAPI.Domain.Entities;
using DefaultAPI.Domain.Filters;
using DefaultAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DefaultAPI.Application.Interfaces
{
    public interface ICityService
    {
        Task<CityPagedReturned> GetAllFromUf(int IdState = 25, int? page = 1, int? limit = int.MaxValue);
        Task<List<CityReturnedDto>> GetAllEntity();
        Task<CityReturnedDto> GetById(int id);
        Task Add(City city);
        Task Remove(long id);
        Task Update(City city);
        Task<ResultReturned> AddOrUpdateCity(List<City> lista);
        Task<bool> CheckCityExist(string city, long IdState);
        Task<List<long>> GetIdStates();
    }
}
