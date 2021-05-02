using DefaultAPI.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DefaultAPI.Application;

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
                throw new Exception(ex.Message, ex.InnerException);
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
                throw new Exception(ex.Message, ex.InnerException);
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
                throw new Exception(ex.Message, ex.InnerException);
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
                throw new Exception(ex.Message, ex.InnerException);
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
                throw new Exception(ex.Message, ex.InnerException);
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
                throw new Exception(ex.Message, ex.InnerException);
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
                throw new Exception(ex.Message, ex.InnerException);
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
                throw new Exception(ex.Message, ex.InnerException);
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
                string[] values = listaEntity.GetType().GetProperties().Select(x => x.Name + ": " + x.GetValue(listaEntity, null)).ToArray();
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate) => DbSet.AsNoTracking().Where(predicate);

        public bool Exist(Expression<Func<TEntity, bool>> predicate) => DbSet.Any(predicate);
    }
}