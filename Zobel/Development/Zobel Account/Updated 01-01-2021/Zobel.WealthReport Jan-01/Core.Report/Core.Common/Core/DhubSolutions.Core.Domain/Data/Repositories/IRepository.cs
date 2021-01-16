using System.Collections.Generic;

namespace DhubSolutions.Core.Domain.Data.Repositories
{
    public interface IRepository
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
    public interface IRepository<TEntity> : IRepository, IReadOnlyRepository<TEntity> where TEntity : class, new()
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        TEntity Create();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Add(TEntity entity);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        void AddRange(IEnumerable<TEntity> entities);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        void UpdateRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        void Remove(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Remove(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        void RemoveRange(IEnumerable<TEntity> entities);





    }
}
