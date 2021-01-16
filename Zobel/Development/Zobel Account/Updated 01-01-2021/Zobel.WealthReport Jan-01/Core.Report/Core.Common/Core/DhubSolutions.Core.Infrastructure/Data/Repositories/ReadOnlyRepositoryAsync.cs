using DhubSolutions.Core.Domain.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DhubSolutions.Core.Infrastructure.Data.Repositories
{
    public abstract class ReadOnlyRepositoryAsync<TEntity> : IReadOnlyRepositoryAsync<TEntity>
         where TEntity : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        protected ReadOnlyRepositoryAsync(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region ReadOnlyRepositoryAsync

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityKeyValues"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FindAsync(params object[] entityKeyValues)
        {
            // local instance of entity
            TEntity entity = null;

            if (entityKeyValues != null && entityKeyValues.Length > 0)
                // gets entity
                entity = await _dbSet.FindAsync(entityKeyValues);

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
        public async Task<TEntity> GetAsync(
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

            else
                query = _dbContext.Model.FindEntityType(typeof(TEntity)).GetNavigations()
                    .Aggregate(query, (current, navigation) => current.Include(navigation.Name));

            if (asNoTracking)
                query = query.AsNoTracking();

            return await query.SingleOrDefaultAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="noTracking"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>>
            filter = null, bool noTracking = true,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            if (includes != null)
                query = includes.Aggregate(query, (current, includeEntity) => current.Include(includeEntity));

            else
                query = _dbContext.Model.FindEntityType(typeof(TEntity)).GetNavigations()
                    .Aggregate(query, (current, navigation) => current.Include(navigation.Name));

            if (noTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();
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
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
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

            else
                query = _dbContext.Model.FindEntityType(typeof(TEntity)).GetNavigations()
                    .Aggregate(query, (current, navigation) => current.Include(navigation.Name));

            if (noTracking)
                query = query.AsNoTracking();

            return await query.Skip(pageCount * pageIndex)
                              .Take(pageCount)
                              .ToListAsync();
        }


        #endregion       


    }
}
