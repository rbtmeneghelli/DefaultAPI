﻿using DefaultAPI.Application.Factory;
using DefaultAPI.Application.Interfaces;
using DefaultAPI.Domain.Dto;
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
    public class LogService : ILogService
    {
        public readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<Log> GetById(long id)
        {
            return await Task.FromResult(_logRepository.GetById(id));
        }

        public async Task<List<Log>> GetAllWithLike(string parametro) => await _logRepository.GetAllWithLike(parametro);

        public async Task<PagedResult<LogReturnedDto>> GetAllWithPaginate(LogFilter filter)
        {
            try
            {
                var query = await GetAllWithFilter(filter);
                var queryCount = await GetCount(filter);

                var queryResult = from x in query.AsQueryable()
                                  orderby x.UpdateTime descending
                                  select new LogReturnedDto()
                                  {
                                      Id = x.Id,
                                      Class = x.Class,
                                      Method = x.Method,
                                      Object = x.Object,
                                      MessageError = x.MessageError,
                                      UpdateTime = x.UpdateTime
                                  };

                return PagedFactory.GetPaged(queryResult, filter.pageIndex, filter.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        private async Task<IQueryable<Log>> GetAllWithFilter(LogFilter filter)
        {
            return await Task.FromResult(_logRepository.GetAll().Where(GetPredicate(filter)).AsQueryable());
        }

        private async Task<int> GetCount(LogFilter filter)
        {
            return await _logRepository.GetAll().CountAsync(GetPredicate(filter));
        }

        private Expression<Func<Log, bool>> GetPredicate(LogFilter filter)
        {
            return p =>
            (string.IsNullOrWhiteSpace(filter.Class) || p.Class.Trim().ToUpper().Contains(filter.Class.Trim().ToUpper()));
        }

        public void Dispose()
        {
            _logRepository?.Dispose();
        }
    }
}
