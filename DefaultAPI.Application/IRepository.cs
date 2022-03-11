using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DefaultAPI.Application
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAllTracking();
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> FindByTracking(Expression<Func<TEntity, bool>> predicate);
        TEntity GetById(long id);
        long GetCount();
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> listaEntity);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> listaEntity);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> listaEntity);
        long SaveChanges();
        bool ExecuteSql(string sql, params object[] parameters);
        List<dynamic> ExecuteDynamicSQL(string sql, Dictionary<string, object> parameters = null);
        void SetCommandTimeout(int timeout);
        bool ExecuteProcedureSql(string sql);
        bool Exist(Expression<Func<TEntity, bool>> predicate);
        void BulkInsert(IEnumerable<TEntity> lista);
    }
}
