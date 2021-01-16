using DhubSolutions.Core.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DhubSolutions.Core.Domain.Services
{
    public interface IServiceMapperAsync<TEntity> : IServiceMapper where TEntity : IEntity
    {
        /// <summary>
        ///     Add item into repository.
        /// </summary>
        /// <param name="dto">Item to add to repository.</param>
        Task<TEntity> AddAsync<Dto>(Dto dto) where Dto : class;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        Task AddRangeAsync<Dto>(IEnumerable<Dto> dtos);

        /// <summary>
        ///     Get an element of type <typeparamref name="TEntity" /> in repository.
        /// </summary>
        /// <param name="filter">Filters the elements in database BEFORE materialize the query.</param>
        /// <param name="asNoTracking">Indicates that the resulting objects are not tracked by EF.</param>
        /// <param name="joinedEntities">
        ///     Include these entities in the query result, otherwise these navigation fields will be
        ///     null.
        /// </param>
        /// <returns>The element found, otherwise null</returns>
        Task<Dto> GetAsync<Dto>(
            Expression<Func<TEntity, bool>> filter,
            bool asNoTracking = true,
            params Expression<Func<TEntity, object>>[] joinedEntities) where Dto : class;

        /// <summary>
        ///     Get all elements of type <typeparamref name="TEntity" /> in repository.
        /// </summary>
        /// <param name="filter">Filters the elements in database BEFORE materialize the query.</param>
        /// <param name="noTracking">Indicates that the resulting objects are not tracked by EF.</param>
        /// <param name="joinedEntities">
        ///     Include these entities in the query result, otherwise these navigation fields will be
        ///     null.
        /// </param>
        /// <returns>List of selected elements.</returns>
        Task<IEnumerable<Dto>> GetAllAsync<Dto>(
            Expression<Func<TEntity, bool>> filter = null,
            bool noTracking = true,
            params Expression<Func<TEntity, object>>[] joinedEntities) where Dto : class;

        /// <summary>
        /// Get all elements of type Dto 
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageCount">Number of elements in each page</param>
        /// <param name="filter">Filters the elements in database BEFORE materialize the query.</param>
        /// <param name="noTracking">Indicates that the resulting objects are not tracked by EF.</param>
        /// <param name="includes">
        ///     Include these entities in the query result, otherwise these navigation fields will be
        ///     null.
        /// </param>
        /// <returns>List of selected elements.</returns>
        /// <returns>List of selected elements</returns>
        Task<IEnumerable<Dto>> GetAllAsync<Dto>(
            int pageIndex,
            int pageCount,
             Expression<Func<TEntity, bool>> filter = null,
            bool noTracking = true,
            params Expression<Func<TEntity, object>>[] includes) where Dto : class;

        /// <summary>
        ///     Gets an element by it's entity key.
        /// </summary>
        /// <param name="entityKeyValues">Entity key values, the order the are same of order in mapping.</param>
        /// <returns>The element found, otherwise null.</returns>
        Task<Dto> FindAsync<Dto>(params object[] entityKeyValues) where Dto : class;

    }
}
