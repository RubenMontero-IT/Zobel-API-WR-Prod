using DhubSolutions.Common.Domain.Entities.Admin;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DhubSolutions.WealthReport.Domain.Repositories
{
    public interface IWealthReportReadOnlyRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="entityKeyValues"></param>
        /// <returns></returns>
        TEntity Find(Organization organization, params object[] entityKeyValues);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="filter"></param>
        /// <param name="asNoTracking"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        TEntity Get(
           Organization organization,
           Expression<Func<TEntity, bool>> filter,
           bool asNoTracking = true,
           params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="filter"></param>
        /// <param name="asNoTracking"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll(
            Organization organization,
            Expression<Func<TEntity, bool>> filter = null,
            bool asNoTracking = true,
            params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="filter"></param>
        /// <param name="noTracking"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll(
            Organization organization,
            int pageIndex,
            int pageCount,
            Expression<Func<TEntity, bool>> filter = null,
            bool noTracking = true,
            params Expression<Func<TEntity, object>>[] includes);

    }
}
