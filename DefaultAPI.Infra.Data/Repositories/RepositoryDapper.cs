using Dapper;
using DefaultAPI.Application;
using DefaultAPI.Domain.Models;
using DefaultAPI.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace DefaultAPI.Infra.Data.Repositories
{
    public class RepositoryDapper<TEntity> : IRepositoryDapper<TEntity> where TEntity : class
    {
        protected readonly DefaultAPIContext _context;

        public RepositoryDapper(DefaultAPIContext context)
        {
            _context = context;
        }

        public async Task<List<TEntity>> QueryToGetAll(string sqlQuery)
        {
            var list = new List<TEntity>();
            using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                connection.Open();
                list = (await connection.QueryAsync<TEntity>(sql: sqlQuery)).AsList();
                return list;
            }
        }

        public async Task<TEntity> QueryToGetFirstOrDefault(string sqlQuery)
        {
            using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                connection.Open();
                TEntity entity = await connection.QueryFirstOrDefaultAsync<TEntity>(sql: sqlQuery);
                return entity;
            }
        }

        public async Task<QueryResult<TEntity>> QueryMultiple(string sqlQuery)
        {
            using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                connection.Open();
                var reader = await connection.QueryMultipleAsync(sql: sqlQuery);
                return new QueryResult<TEntity>
                {
                    Count = reader.Read<int>().FirstOrDefault(),
                    Result = reader.Read<TEntity>().ToList()
                };
            }
        }

        public async Task ExecuteQuery(string sqlQuery)
        {
            using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                connection.Open();
                await connection.ExecuteAsync(sql: sqlQuery);
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
