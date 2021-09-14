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
    public class AuditService : BaseService, IAuditService
    {
        public readonly IRepository<Audit> _auditRepository;
        public readonly IRepositoryDapper<Audit> _auditDapper;

        public AuditService(IRepository<Audit> auditRepository, IRepositoryDapper<Audit> auditDapper, INotificationMessageService notificationMessageService): base(notificationMessageService)
        {
            _auditRepository = auditRepository;
            _auditDapper = auditDapper;
        }

        public async Task<Audit> GetById(long id)
        {
            return await Task.FromResult(_auditRepository.GetById(id));
        }

        public List<Audit> GetAllWithLike(string parametro) => _auditRepository.GetAll().Where(x => EF.Functions.Like(x.TableName, $"%{parametro}%")).ToList();

        public async Task<PagedResult<AuditReturnedDto>> GetAllDapper(AuditFilter filter)
        {
            string sql = @"select count(*) from audits " +
            @"select Id = Id, TableName = Table_Name, ActionName = Action_Name from audits where (Table_Name = '" + filter.TableName + "')";
            var reader = await _auditDapper.QueryMultiple(sql);

            var queryResult = from x in reader.Result.AsQueryable()
                              orderby x.UpdateTime descending
                              select new AuditReturnedDto()
                              {
                                  Id = x.Id,
                                  TableName = x.TableName,
                                  ActionName = x.ActionName,
                                  UpdateTime = x.UpdateTime,
                                  KeyValues = x.KeyValues,
                                  OldValues = x.OldValues,
                                  NewValues = x.NewValues
                              };

            return PagedFactory.GetPaged(queryResult, filter.pageIndex, filter.pageSize);
        }

        public async Task<PagedResult<AuditReturnedDto>> GetAllWithPaginate(AuditFilter filter)
        {
            try
            {
                var query = await GetAllWithFilter(filter);
                var queryCount = await GetCount(filter);

                var queryResult = from x in query.AsQueryable()
                                  orderby x.UpdateTime descending
                                  select new AuditReturnedDto()
                                  {
                                      Id = x.Id,
                                      TableName = x.TableName,
                                      ActionName = x.ActionName,
                                      UpdateTime = x.UpdateTime,
                                      KeyValues = x.KeyValues,
                                      OldValues = x.OldValues,
                                      NewValues = x.NewValues
                                  };

                return PagedFactory.GetPaged(queryResult, filter.pageIndex, filter.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        private async Task<IQueryable<Audit>> GetAllWithFilter(AuditFilter filter)
        {
            return await Task.FromResult(_auditRepository.GetAll().Where(GetPredicate(filter)).AsQueryable());
        }

        private async Task<int> GetCount(AuditFilter filter)
        {
            return await _auditRepository.GetAll().CountAsync(GetPredicate(filter));
        }

        private Expression<Func<Audit, bool>> GetPredicate(AuditFilter filter)
        {
            return p =>
            (string.IsNullOrWhiteSpace(filter.TableName) || p.TableName.Trim().ToUpper().Contains(filter.TableName.Trim().ToUpper()));
        }
    }
}