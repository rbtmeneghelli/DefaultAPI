using DefaultAPI.Application.Interfaces;
using DefaultAPI.Domain.Dto;
using DefaultAPI.Domain.Entities;
using DefaultAPI.Domain.Filters;
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
        public readonly IRepository<Log> _logRepository;

        public LogService(IRepository<Log> logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<Log> GetById(long id)
        {
            return await Task.FromResult(_logRepository.GetById(id));
        }

        public List<Log> GetAllWithLike(string parametro) => _logRepository.GetAll().Where(x => EF.Functions.Like(x.Class, $"%{parametro}%")).ToList();

        public async Task<LogPagedReturned> GetAllWithPaginate(LogFilter filter)
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

                filter.pageIndex++;
                return new LogPagedReturned
                {
                    Logs = queryResult.Skip((filter.pageIndex - 1) * filter.pageSize).Take(filter.pageSize).ToList(),
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
    }
}
