using DhubSolutions.Core.Domain.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DhubSolutions.Core.Infrastructure.Data.Repositories
{
    public abstract class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity>
        where TEntity : class
    {
        protected readonly DbSet<TEntity> _dbSet;
        protected readonly DbContext _dbContext;

        public ReadOnlyRepository(DbContext dbContenxt)
        {
            _dbContext = dbContenxt;
            _dbSet = dbContenxt.Set<TEntity>();
        }

        #region ReadOnlyRepository

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityKeyValues"></param>
        /// <returns></returns>
        public virtual TEntity Find(params object[] entityKeyValues)
        {
            // local instance of entity
            TEntity entity = null;

            if (entityKeyValues != null && entityKeyValues.Length > 0)
                // gets entity
                entity = _dbSet.Find(entityKeyValues);

            // return located entity or null
            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="asNoTracking"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public virtual TEntity Get(
            Expression<Func<TEntity, bool>> predicate,
            bool asNoTracking = true,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();

            if (predicate == null)
                throw new NullReferenceException(nameof(predicate));

            query = query.Where(predicate);

            if (includes != null)
                query = includes.Aggregate(query, (current, includeEntity) => current.Include(includeEntity));

            if (asNoTracking)
                query = query.AsNoTracking();

            return query.SingleOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="noTracking"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> filter = null,
            bool noTracking = true,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            if (noTracking)
                query = query.AsNoTracking();

            if (includes != null)
                query = includes.Aggregate(query, (current, includeEntity) => current.Include(includeEntity));

            if (filter != null)
                query = query.Where(filter);

            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="filter"></param>
        /// <param name="noTracking"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetAll(
            int pageIndex,
            int pageCount,
            Expression<Func<TEntity, bool>> filter = null,
            bool noTracking = true,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            if (includes != null)
                query = includes.Aggregate(query, (current, includeEntity) => current.Include(includeEntity));

            if (noTracking)
                query = query.AsNoTracking();

            return query.Skip(pageCount * pageIndex)
                        .Take(pageCount);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Query(string sql, params object[] parameters)
        {
            return _dbSet.FromSql(sql, parameters: parameters);
        }

        #endregion

    }
}
