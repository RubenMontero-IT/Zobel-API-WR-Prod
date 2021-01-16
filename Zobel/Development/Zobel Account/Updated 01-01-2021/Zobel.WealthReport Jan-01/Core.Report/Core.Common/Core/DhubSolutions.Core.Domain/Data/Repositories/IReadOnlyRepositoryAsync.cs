using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DhubSolutions.Core.Domain.Data.Repositories
{
    public interface IReadOnlyRepositoryAsync
    {
    }

    public interface IReadOnlyRepositoryAsync<TEntity> : IReadOnlyRepository where TEntity : class
    {
        /// <summary>
        ///     Get all elements of type <typeparamref name="TEntity" /> in repository.
        /// </summary>
        /// <param name="filter">Filters the elements in database BEFORE materialize the query.</param>
        /// <param name="noTracking">Indicates that the resulting objects are not tracked by EF.</param>
        /// <param name="includes">
        ///     Include these entities in the query result, otherwise these navigation fields will be
        ///     null.
        /// </param>
        /// <returns>List of selected elements.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null,
            bool noTracking = true,
            params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Get all elements of type TEntity in repository
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageCount">Number of elements in each page</param>
        /// <param name="filter">Filters the elements in database BEFORE materialize the query.</param>
        /// <param name="asNoTracking">Indicates that the resulting objects are not tracked by EF.</param>
        /// <param name="includes">
        ///     Include these entities in the query result, otherwise these navigation fields will be
        ///     null.
        /// </param>
        /// <returns>List of selected elements.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync(
             int pageIndex,
             int pageCount,
              Expression<Func<TEntity, bool>> filter = null,
             bool asNoTracking = true,
             params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="asNoTracking"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate,
                                      bool asNoTracking = true,
                                      params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        ///     Gets an element by it's entity key.
        /// </summary>
        /// <param name="entityKeyValues">Entity key values, the order the are same of order in mapping.</param>
        /// <returns>The element found, otherwise null.</returns>
        Task<TEntity> FindAsync(params object[] entityKeyValues);
    }
}
