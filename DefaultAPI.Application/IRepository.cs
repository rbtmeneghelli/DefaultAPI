using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DefaultAPI.Application
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity entity);
        TEntity GetById(params object[] keyvalues);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAllTracking();
        long GetCount();
        void Update(TEntity entity);
        void UpdateRange(List<TEntity> listaEntity);
        void Remove(TEntity entity);
        long SaveChanges();
        void RemoveRange(IList<TEntity> listaEntity);
        void AddRange(IList<TEntity> listaEntity);
        bool ExecuteSql(string sql, params object[] parameters);
        List<dynamic> ExecuteDynamicSQL(string sql, Dictionary<string, object> parameters = null);
        void SetCommandTimeout(int timeout);
        bool ExecuteProcedureSql(string sql);
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        bool Exist(Expression<Func<TEntity, bool>> predicate);
    }
}
