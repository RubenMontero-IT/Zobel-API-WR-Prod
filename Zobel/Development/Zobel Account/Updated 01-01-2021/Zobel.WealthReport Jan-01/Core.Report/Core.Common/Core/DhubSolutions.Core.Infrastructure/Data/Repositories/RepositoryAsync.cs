using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Core.Domain.Data.Repositories;
using DhubSolutions.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DhubSolutions.Core.Infrastructure.Data.Repositories
{
    public abstract class RepositoryAsync<TEntity> :
        ReadOnlyRepositoryAsync<TEntity>,
        IRepositoryAsync<TEntity> where TEntity : class, IEntity, new()
    {
        protected RepositoryAsync(DbContext dbContext, IUnitOfWork unitOfWork) : base(dbContext)
        {
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; }

        public TEntity CreateAsync()
        {
            throw new NotImplementedException();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            if (entity == null)
                throw new NullReferenceException(nameof(entity));

            await _dbSet.AddAsync(entity);
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new NullReferenceException(nameof(entities));

            await _dbSet.AddRangeAsync(entities);

        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            await Task.Factory.StartNew(() =>
             {
                 _dbContext.Attach(entity);
                 _dbContext.Entry(entity).State = EntityState.Modified;
             });

        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            await Task.Factory.StartNew(() =>
            {
                if (entities == null)
                    throw new NullReferenceException(nameof(entities));

                _dbSet.UpdateRange(entities); throw new NotImplementedException();
            });
        }
    }
}
