using System.Collections.Generic;
using System.Threading.Tasks;

namespace DhubSolutions.Core.Domain.Data.Repositories
{
    public interface IRepositoryAsync
    {
        /// <summary>
        /// 
        /// </summary>new
        IUnitOfWork UnitOfWork { get; }

    }
    /// <summary>
    ///     Base interface for implement a "Repository Pattern".
    /// </summary>
    /// <remarks>
    ///     Indeed, one might think that IObjectSet is already a generic repository and therefore  would not need this item.
    ///     Using this interface allows us to ensure PI(Persistence Ignorant) principle within our domain model.
    /// </remarks>
    /// <typeparam name="TEntity">Type of entity for this repository.</typeparam>
    public interface IRepositoryAsync<TEntity> : IRepositoryAsync, IReadOnlyRepositoryAsync<TEntity> where TEntity : class, new()
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        TEntity CreateAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);

    }
}
