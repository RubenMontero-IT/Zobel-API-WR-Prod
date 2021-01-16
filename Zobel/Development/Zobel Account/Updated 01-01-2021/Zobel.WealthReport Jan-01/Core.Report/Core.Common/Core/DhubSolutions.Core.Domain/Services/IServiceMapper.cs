using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Core.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DhubSolutions.Core.Domain.Services
{
    public interface IServiceMapper
    {
        /// <summary>
        ///     Gets the unit of work in this service.
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        ///     Adapter for converting entity to dto and viceversa
        /// </summary>
        ITypeAdapter TypeAdapter { get; }

    }

    public interface IServiceMapper<TEntity> : IServiceMapper
        where TEntity : IEntity
    {

        /// <summary>
        ///     Add item into repository.
        /// </summary>
        /// <param name="dto">Item to add to repository.</param>
        TEntity Add<Dto>(Dto dto) where Dto : class;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtos"></param>
        void AddRange<Dto>(IEnumerable<Dto> dtos) where Dto : class;


        /// <summary>
        ///     Delete item.
        /// </summary>
        /// <param name="dto">Item to delete.</param>
        TEntity Remove<Dto>(Dto dto) where Dto : class;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtos"></param>
        void RemoveRange<Dto>(IEnumerable<Dto> dtos) where Dto : class;


        /// <summary>
        ///     Sets modified entity into the repository. When calling Commit() method in UnitOfWork these changes will be saved
        ///     into the storage.
        ///     <remarks>
        ///         Internally this method always calls Repository.Attach().
        ///     </remarks>
        /// </summary>
        /// <param name="dto">Item with changes.</param>
        TEntity Update<Dto>(Dto dto) where Dto : class;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtos"></param>
        void UpdateRange<Dto>(IEnumerable<Dto> dtos) where Dto : class;

        /// <summary>
        ///     Get an element of type <typeparamref name="TEntity" /> in repository.
        /// </summary>
        /// <param name="filter">Filters the elements in database BEFORE materialize the query.</param>
        /// <param name="asNoTracking">Indicates that the resulting objects are not tracked by EF.</param>
        /// <param name="includes">
        ///     Include these entities in the query result, otherwise these navigation fields will be
        ///     null.
        /// </param>
        /// <returns>The element found, otherwise null</returns>
        Dto Get<Dto>(
            Expression<Func<TEntity, bool>> filter,
            bool asNoTracking = true,
            params Expression<Func<TEntity, object>>[] includes) where Dto : class;


        /// <summary>
        ///     Get all elements of type <typeparamref name="TEntity" /> in repository.
        /// </summary>
        /// <param name="filter">Filters the elements in database BEFORE materialize the query.</param>
        /// <param name="asNoTracking">Indicates that the resulting objects are not tracked by EF.</param>
        /// <param name="includes">
        ///     Include these entities in the query result, otherwise these navigation fields will be
        ///     null.
        /// </param>
        /// <returns>List of selected elements.</returns>
        IEnumerable<Dto> GetAll<Dto>(
            Expression<Func<TEntity, bool>> filter = null,
            bool asNoTracking = true,
            params Expression<Func<TEntity, object>>[] includes) where Dto : class;


        /// <summary>
        /// Get all elements of type TViewModel
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
        IEnumerable<Dto> GetAll<Dto>(
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
        Dto Find<Dto>(params object[] entityKeyValues) where Dto : class;

        /// <summary>
        ///     Creates a new entity object.
        /// </summary>
        /// <returns>The new object with default values.</returns>
        Dto Create<Dto>() where Dto : class;
    }
}
