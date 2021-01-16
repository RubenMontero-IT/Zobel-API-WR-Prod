using DhubSolutions.Core.Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace DhubSolutions.Core.Infrastructure.Data
{
    /// <summary>
    /// The UnitOfWork contract for EF implementation
    /// <remarks>
    /// This contract extend IUnitOfWork for use with EF code
    /// </remarks>
    /// </summary>
    public interface IEntityFrameworkUnitOfWork<TContext> : IEntityFrameworkUnitOfWork
        where TContext : DbContext
    {
        TContext Context { get; }
    }

    /// <summary>
    /// The UnitOfWork contract for EF implementation
    /// <remarks>
    /// This contract extend IUnitOfWork for use with EF code
    /// </remarks>
    /// </summary>
    public interface IEntityFrameworkUnitOfWork : IUnitOfWork, ISql
    {
        /// <summary>
        /// Attach this item into "ObjectStateManager"
        /// </summary>
        /// <typeparam name="TEntity">The type of entity</typeparam>
        /// <param name="entity">The item <</param>
        void Attach<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Set object as modified
        /// </summary>
        /// <typeparam name="TEntity">The type of entity</typeparam>
        /// <param name="entity">The entity item to set as modifed</param>
        void SetModified<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Detach this item into "ObjectStateManager"
        /// </summary>
        /// <typeparam name="TEntity">The type of entity</typeparam>
        /// <param name="entity">The item <</param>
        void Detach<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Apply current values in <paramref name="original"/>
        /// </summary>
        /// <typeparam name="TEntity">The type of entity</typeparam>
        /// <param name="original">The original entity</param>
        /// <param name="current">The current entity</param>
        void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class;

    }
}
