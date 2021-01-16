using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Infrastructure.Data.Context;
using DhubSolutions.WealthReport.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Repositories
{
    public class WealthReportReadOnlyRepository<TEntity> : IWealthReportReadOnlyRepository<TEntity>
        where TEntity : class
    {
        protected readonly IEntityFrameworkContextAccessor _contextAccessor;
        protected readonly IConnectionByOrganizationRepository _repository;

        public WealthReportReadOnlyRepository(IEntityFrameworkContextAccessor contextAccessor, IConnectionByOrganizationRepository repository)
        {
            _contextAccessor = contextAccessor;
            _repository = repository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="entityKeyValues"></param>
        /// <returns></returns>
        public virtual TEntity Find(Organization organization, params object[] entityKeyValues)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="filter"></param>
        /// <param name="asNoTracking"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public TEntity Get(Organization organization,
            Expression<Func<TEntity, bool>> filter,
            bool asNoTracking = true,
            params Expression<Func<TEntity, object>>[] includes)
        {
            DbContext context = GetContext(organization);

            IQueryable<TEntity> query = context.Set<TEntity>();

            if (filter != null)
                query = query.Where(filter);

            if (includes != null)
                query = includes.Aggregate(query, (current, includeEntity) => current.Include(includeEntity));

            if (asNoTracking)
                query = query.AsNoTracking();

            return query.SingleOrDefault();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="filter"></param>
        /// <param name="asNoTracking"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetAll(Organization organization,
            Expression<Func<TEntity, bool>> filter = null,
            bool asNoTracking = true,
            params Expression<Func<TEntity, object>>[] includes)
        {
            DbContext context = GetContext(organization);

            IQueryable<TEntity> query = context.Set<TEntity>();

            if (filter != null)
                query = query.Where(filter);

            if (includes != null)
                query = includes.Aggregate(query, (current, includeEntity) => current.Include(includeEntity));

            if (asNoTracking)
                query = query.AsNoTracking();

            return query;

        }

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
        public IEnumerable<TEntity> GetAll(Organization organization,
            int pageIndex,
            int pageCount,
            Expression<Func<TEntity, bool>> filter = null,
            bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        protected DbContext GetContext(Organization organization)
        {
            var connectionId = _repository.GetConnectionId(organization);

            return _contextAccessor.GetContext(connectionId);


        }
    }
}
