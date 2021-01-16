using DhubSolutions.Common.Domain.Entities.Admin;
using System.Collections.Generic;

namespace DhubSolutions.WealthReport.Domain.Repositories
{
    public interface IWealthReportRepository<TEntity> : IWealthReportReadOnlyRepository<TEntity>
        where TEntity : class, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        TEntity Create();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="entity"></param>
        void Add(Organization organization, TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="entities"></param>
        void AddRange(Organization organization, IEnumerable<TEntity> entities);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="entity"></param>
        void Remove(Organization organization, TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="entities"></param>
        void RemoveRange(Organization organization, IEnumerable<TEntity> entities);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="entity"></param>
        void Update(Organization organization, TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="entities"></param>
        void UpdateRange(Organization organization, IEnumerable<TEntity> entities);

    }
}
