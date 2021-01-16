using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Core.Domain.Data.Repositories;
using DhubSolutions.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DhubSolutions.Core.Infrastructure.Data.Repositories
{
    public abstract class Repository<TEntity> :
        ReadOnlyRepository<TEntity>,
        IRepository<TEntity> where TEntity : class, IEntity, new()
    {

        protected Repository(DbContext dbContext, IUnitOfWork unitOfWork) : base(dbContext)
        {
            UnitOfWork = unitOfWork;
        }


        public IUnitOfWork UnitOfWork { get; }

        public TEntity Create()
        {
            return new TEntity();
        }

        public virtual void Add(TEntity entity)
        {
            if (entity == null)
                throw new NullReferenceException(nameof(entity));

            _dbSet.Add(entity);

        }


        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new NullReferenceException(nameof(entities));

            _dbSet.AddRange(entities);
        }



        public virtual void Remove(string id)
        {
            TypeInfo typeInfo = typeof(TEntity).GetTypeInfo();
            IProperty key = _dbContext.Model.FindEntityType(typeInfo).FindPrimaryKey().Properties.FirstOrDefault();
            PropertyInfo property = typeInfo.GetProperty(key?.Name);
            if (property != null)
            {
                TEntity entity = Activator.CreateInstance<TEntity>();
                property.SetValue(entity, id);
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }
            else
            {
                TEntity entity = _dbSet.Find(id);
                if (entity != null)
                    _dbSet.Remove(entity);
            }
        }

        public virtual void Remove(TEntity entity)
        {
            TEntity foundEntity = _dbSet.Find(entity.Id);
            if (foundEntity != null)
                _dbSet.Remove(foundEntity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new NullReferenceException(nameof(entities));

            _dbSet.RemoveRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            _dbContext.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new NullReferenceException(nameof(entities));

            _dbSet.UpdateRange(entities);
        }


    }
}
