using DefaultAPI.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DefaultAPI.Application;
using DefaultAPI.Domain;
using DefaultAPI.Domain.Entities;

namespace DefaultAPI.Infra.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DefaultAPIContext _context;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(DefaultAPIContext context)
        {
            _context = context;
            DbSet = _context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            try
            {
                DbSet.Add(entity);
            }
            catch (Exception ex)
            {
                SaveLogError(GetValuesFromEntity(entity), entity.GetType().Name, "Add", ex.Message);
            }
        }

        public void AddRange(IList<TEntity> listaEntity)
        {
            try
            {
                DbSet.AddRange(listaEntity);
            }
            catch (Exception ex)
            {
                SaveLogError(GetValuesFromEntity(listaEntity), listaEntity.GetType().Name, "AddRange", ex.Message);
            }
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
            GC.SuppressFinalize(this);
        }

        public IQueryable<TEntity> GetAll()
        {
            return DbSet.AsNoTracking();
        }

        public IQueryable<TEntity> GetAllTracking()
        {
            return DbSet;
        }

        public long GetCount()
        {
            return DbSet.AsNoTracking().Count();
        }

        public virtual TEntity GetById(params object[] keyvalues)
        {
            var result = DbSet.Find(keyvalues);
            _context.Entry(result).State = EntityState.Detached;
            return result;
        }

        public void Remove(TEntity entity)
        {
            try
            {
                DbSet.Remove(entity);
            }
            catch (Exception ex)
            {
                SaveLogError(GetValuesFromEntity(entity), entity.GetType().Name, "Remove", ex.Message);
            }
        }

        public void RemoveRange(IList<TEntity> listaEntity)
        {
            try
            {
                DbSet.RemoveRange(listaEntity);
            }
            catch (Exception ex)
            {
                SaveLogError(GetValuesFromEntity(listaEntity), listaEntity.GetType().Name, "RemoveRange", ex.Message);
            }
        }

        public long SaveChanges()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                foreach (var entry in _context.ChangeTracker.Entries())
                {
                    if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                        continue;
                    string[] values = entry.Entity.GetType().GetProperties().Select(x => x.Name + ": " + x.GetValue(entry.Entity, null)).ToArray();
                    SaveLogError(values, entry.Metadata.GetTableName(), "SaveChanges", ex.Message);
                }
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public void Update(TEntity entity)
        {
            try
            {
                DbSet.Update(entity);
            }
            catch (Exception ex)
            {
                SaveLogError(GetValuesFromEntity(entity), entity.GetType().Name, "Update", ex.Message);
            }
        }

        public bool ExecuteSql(string sql, params object[] parameters)
        {
            int count = 0;

            try
            {
                count = _context.Database.ExecuteSqlRaw(sql, parameters);
            }
            catch (Exception ex)
            {
                SaveLogErrorSql(sql, "Script", "ExecuteSql", ex.Message);
            }
            return count > 0 ? true : false;
        }

        public List<dynamic> ExecuteDynamicSQL(string sql, Dictionary<string, object> parameters = null)
        {
            List<dynamic> list = new List<dynamic>();
            try
            {
                if (parameters == null)
                    parameters = new Dictionary<string, object>();

                list = _context.CollectionFromSql(sql, parameters).ToList();
            }
            catch (Exception ex)
            {
                SaveLogErrorSql(sql, "Script", "ExecuteDynamicSQL", ex.Message);
            }

            return list;
        }

        public void SetCommandTimeout(int timeout)
        {
            _context.Database.SetCommandTimeout(timeout);
        }

        public bool ExecuteProcedureSql(string sql)
        {
            int count = 0;
            try
            {
                count = _context.Database.ExecuteSqlRaw(sql);
            }
            catch (Exception ex)
            {
                SaveLogErrorSql(sql, "Script", "ExecuteProcedureSql", ex.Message);
            }
            return count > 0 ? true : false;
        }

        public void UpdateRange(List<TEntity> listaEntity)
        {
            try
            {
                DbSet.UpdateRange(listaEntity);
            }
            catch (Exception ex)
            {
                SaveLogError(GetValuesFromEntity(listaEntity), listaEntity.GetType().Name, "UpdateRange", ex.Message);
            }
        }

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate) => DbSet.AsNoTracking().Where(predicate);

        public bool Exist(Expression<Func<TEntity, bool>> predicate) => DbSet.Any(predicate);
  
        private string[] GetValuesFromEntity(IList<TEntity> entity)
        {
            return entity.GetType().GetProperties().Select(x => x.Name + ": " + x.GetValue(entity, null)).ToArray();
        }

        private string[] GetValuesFromEntity(TEntity entity)
        {
            return entity.GetType().GetProperties().Select(x => x.Name + ": " + x.GetValue(entity, null)).ToArray();
        }

        private void SaveLogError(string[] values, string entity, string method, string messageError)
        {
            _context.Database.ExecuteSqlRaw(string.Format(Constants.saveLog, entity, method, messageError, DateTime.Now.ToString("yyyy-MM-dd"), string.Join(",", values)));
        }

        private void SaveLogErrorSql(string sql, string entity, string method, string messageError)
        {
            _context.Database.ExecuteSqlRaw(string.Format(Constants.saveLog, entity, method, messageError, DateTime.Now.ToString("yyyy-MM-dd"), sql));
        }
    }
}