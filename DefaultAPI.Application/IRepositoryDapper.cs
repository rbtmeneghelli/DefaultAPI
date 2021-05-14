using DefaultAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace DefaultAPI.Application
{
    public interface IRepositoryDapper<TEntity> : IDisposable where TEntity : class
    {
        Task<List<TEntity>> QueryToGetAll(string sqlQuery);
        Task<TEntity> QueryToGetFirstOrDefault(string sqlQuery);
        Task<QueryResult<TEntity>> QueryMultiple(string sqlQuery);
        Task ExecuteQuery(string sqlQuery);
    }
}