using DefaultAPI.Application;
using DefaultAPI.Domain;
using DefaultAPI.Domain.Entities;
using DefaultAPI.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace DefaultAPI.Infra.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DefaultAPIContext _context;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(DefaultAPIContext context)
        {
            _context = context;
            DbSet = _context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet.AsNoTracking();
        }

        public virtual IQueryable<TEntity> GetAllTracking()
        {
            return DbSet;
        }

        public virtual IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate);
        }

        public virtual IQueryable<TEntity> FindByTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public virtual TEntity GetById(long id)
        {
            var result = DbSet.Find(id);
            _context.Entry(result).State = EntityState.Detached;
            return result;
        }

        public virtual long GetCount()
        {
            return DbSet.AsNoTracking().Count();
        }

        public virtual void Add(TEntity entity)
        {
            try
            {
                DbSet.Add(entity);
                SaveChanges();
            }
            catch (Exception ex)
            {
                SaveLogError(GetValuesFromEntity(entity), entity.GetType().Name, "Add", ex.Message);
            }
        }

        public virtual void AddRange(IEnumerable<TEntity> listaEntity)
        {
            try
            {
                DbSet.AddRange(listaEntity);
                SaveChanges();
            }
            catch (Exception ex)
            {
                SaveLogError(GetValuesFromEntity(listaEntity), listaEntity.GetType().Name, "AddRange", ex.Message);
            }
        }

        public virtual void Update(TEntity entity)
        {
            try
            {
                DbSet.Update(entity);
                SaveChanges();
            }
            catch (Exception ex)
            {
                SaveLogError(GetValuesFromEntity(entity), entity.GetType().Name, "Update", ex.Message);
            }
        }

        public virtual void UpdateRange(IEnumerable<TEntity> listaEntity)
        {
            try
            {
                DbSet.UpdateRange(listaEntity);
                SaveChanges();
            }
            catch (Exception ex)
            {
                SaveLogError(GetValuesFromEntity(listaEntity), listaEntity.GetType().Name, "UpdateRange", ex.Message);
            }
        }

        public virtual void Remove(TEntity entity)
        {
            try
            {
                DbSet.Remove(entity);
                SaveChanges();
            }
            catch (Exception ex)
            {
                SaveLogError(GetValuesFromEntity(entity), entity.GetType().Name, "Remove", ex.Message);
            }
        }

        public virtual void RemoveRange(IEnumerable<TEntity> listaEntity)
        {
            try
            {
                DbSet.RemoveRange(listaEntity);
                SaveChanges();
            }
            catch (Exception ex)
            {
                SaveLogError(GetValuesFromEntity(listaEntity), listaEntity.GetType().Name, "RemoveRange", ex.Message);
            }

        }

        public virtual long SaveChanges()
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

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }

        public virtual bool ExecuteSql(string sql, params object[] parameters)
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

        public virtual List<dynamic> ExecuteDynamicSQL(string sql, Dictionary<string, object> parameters = null)
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

        public virtual void SetCommandTimeout(int timeout)
        {
            _context.Database.SetCommandTimeout(timeout);
        }

        public virtual bool ExecuteProcedureSql(string sql)
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

        public virtual bool Exist(Expression<Func<TEntity, bool>> predicate) => DbSet.Any(predicate);

        public virtual T RunStoredProcedureWithReturn<T>(string procedureName = "[dbo].[FelizAnoNovo]") where T : class
        {
            var parameterReturn = new SqlParameter
            {
                ParameterName = "ReturnValue",
                SqlDbType = GetSqlDbType(typeof(T)),
                Direction = System.Data.ParameterDirection.Output,
            };

            try
            {
                var result = _context.Database.ExecuteSqlRaw($"EXEC @returnValue = {procedureName}", parameterReturn);
                var returnValue = (T)parameterReturn.Value;
                return returnValue;
            }

            catch (Exception ex)
            {
                SaveLogErrorSql(procedureName, "Script", "RunStoredProcedureWithReturn", ex.Message);
            }

            return null;
        }

        public void BulkInsert(IEnumerable<TEntity> lista)
        {
            _context.BulkInsert(lista);
        }

        #region Private Methods

        private string[] GetValuesFromEntity(IEnumerable<TEntity> entity)
        {
            return entity.GetType().GetProperties().Select(x => x.Name + ": " + x.GetValue(entity, null)).ToArray();
        }

        private string[] GetValuesFromEntity(TEntity entity)
        {
            return entity.GetType().GetProperties().Select(x => x.Name + ": " + x.GetValue(entity, null)).ToArray();
        }

        private void SaveLogError(string[] values, string entity, string method, string messageError)
        {
            _context.Database.ExecuteSqlRaw(string.Format(Constants.SaveLog, entity, method, messageError, DateTime.Now.ToString("yyyy-MM-dd"), string.Join(",", values)));
        }

        private void SaveLogErrorSql(string sql, string entity, string method, string messageError)
        {
            _context.Database.ExecuteSqlRaw(string.Format(Constants.SaveLog, entity, method, messageError, DateTime.Now.ToString("yyyy-MM-dd"), sql));
        }

        private SqlDbType GetSqlDbType(Type type)
        {
            Dictionary<Type, SqlDbType> dictionary = new Dictionary<Type, SqlDbType>();
            dictionary.Add(typeof(int), SqlDbType.Int);
            dictionary.Add(typeof(string), SqlDbType.VarChar);
            dictionary.Add(typeof(DateTime), SqlDbType.DateTime);
            return dictionary[type];
        }

        #endregion
    }
}