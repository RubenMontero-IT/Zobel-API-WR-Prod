using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Infrastructure.Data.Context;
using DhubSolutions.WealthReport.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Repositories
{
    public class WealthReportRepository<TEntity> :
        WealthReportReadOnlyRepository<TEntity>,
        IWealthReportRepository<TEntity> where TEntity : class, new()
    {
        public WealthReportRepository(IEntityFrameworkContextAccessor contextAccessor, IConnectionByOrganizationRepository repository)
            : base(contextAccessor, repository)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="entity"></param>
        public void Add(Organization organization, TEntity entity)
        {
            if (entity == null)
                throw new NullReferenceException(nameof(entity));

            DbContext context = GetContext(organization);

            context.Set<TEntity>().Add(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="entities"></param>
        public void AddRange(Organization organization, IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new NullReferenceException(nameof(entities));

            DbContext context = GetContext(organization);

            context.Set<TEntity>().AddRange(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TEntity Create() => new TEntity();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="entity"></param>
        public virtual void Remove(Organization organization, TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="entities"></param>
        public void RemoveRange(Organization organization, IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new NullReferenceException(nameof(entities));

            DbContext context = GetContext(organization);

            context.Set<TEntity>().RemoveRange(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="entity"></param>
        public void Update(Organization organization, TEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            DbContext dbContext = GetContext(organization);

            dbContext.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="entities"></param>
        public void UpdateRange(Organization organization, IEnumerable<TEntity> entities)
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));

            DbContext context = GetContext(organization);

            context.Set<TEntity>().UpdateRange(entities);
        }
    }
}
