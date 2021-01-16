using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Core.Domain.Data.Repositories;
using DhubSolutions.Core.Domain.Entity;
using DhubSolutions.Core.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DhubSolutions.Core.Application.Services
{
    public abstract class ServiceMapper<TEntity> : IServiceMapper<TEntity>
        where TEntity : class, IEntity, new()
    {
        protected readonly IRepository<TEntity> _repository;

        public ServiceMapper(IUnitOfWork unitOfWork, ITypeAdapter typeAdapter, IRepository<TEntity> repository)
        {
            UnitOfWork = unitOfWork;
            TypeAdapter = typeAdapter;
            _repository = repository;
        }

        /// <summary>
        ///     Gets the unit of work in this repository.
        /// </summary>
        public IUnitOfWork UnitOfWork { get; }

        ///
        public ITypeAdapter TypeAdapter { get; }

        /// <summary>
        ///     Add item into repository.
        /// </summary>
        /// <param name="dto">Item to add to repository.</param>
        public virtual TEntity Add<Dto>(Dto dto) where Dto : class
        {
            // load data from DTO to the entity
            TEntity entity = TypeAdapter.Adapt<Dto, TEntity>(dto);

            // add to entity repository
            _repository.Add(entity);

            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="dtos"></param>
        public virtual void AddRange<Dto>(IEnumerable<Dto> dtos) where Dto : class
        {
            var entities = TypeAdapter.Adapt<IEnumerable<Dto>, IEnumerable<TEntity>>(dtos);

            _repository.AddRange(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <returns></returns>
        public virtual Dto Create<Dto>() where Dto : class
        {
            return TypeAdapter.Adapt<TEntity, Dto>(_repository.Create());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        public virtual TEntity Remove<Dto>(Dto dto) where Dto : class
        {
            TEntity entity = TypeAdapter.Adapt<Dto, TEntity>(dto);

            _repository.Remove(entity);

            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="dtos"></param>
        public virtual void RemoveRange<Dto>(IEnumerable<Dto> dtos) where Dto : class
        {
            IEnumerable<TEntity> entities = TypeAdapter.Adapt<IEnumerable<Dto>, IEnumerable<TEntity>>(dtos);

            _repository.RemoveRange(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        public virtual TEntity Update<Dto>(Dto dto) where Dto : class
        {
            TEntity entity = TypeAdapter.Adapt<Dto, TEntity>(dto);

            _repository.Update(entity);

            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="dtos"></param>
        public virtual void UpdateRange<Dto>(IEnumerable<Dto> dtos) where Dto : class
        {
            IEnumerable<TEntity> entities = TypeAdapter.Adapt<IEnumerable<Dto>, IEnumerable<TEntity>>(dtos);

            _repository.UpdateRange(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="entityKeyValues"></param>
        /// <returns></returns>
        public virtual Dto Find<Dto>(params object[] entityKeyValues) where Dto : class
        {
            TEntity entity = _repository.Find(entityKeyValues);
            if (entity != null)
                return TypeAdapter.Adapt<TEntity, Dto>(entity);

            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="filter"></param>
        /// <param name="asNoTracking"></param>
        /// <param name="joinedEntities"></param>
        /// <returns></returns>
        public virtual Dto Get<Dto>(
            Expression<Func<TEntity, bool>> filter,
            bool asNoTracking = false,
            params Expression<Func<TEntity, object>>[] joinedEntities) where Dto : class
        {
            TEntity entity = _repository.Get(filter, asNoTracking, joinedEntities);

            return TypeAdapter.Adapt<TEntity, Dto>(entity);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="filter"></param>
        /// <param name="asNoTracking"></param>
        /// <param name="joinedEntities"></param>
        /// <returns></returns>
        public virtual IEnumerable<Dto> GetAll<Dto>(
            Expression<Func<TEntity, bool>> filter = null,
            bool asNoTracking = false,
            params Expression<Func<TEntity, object>>[] joinedEntities) where Dto : class
        {
            IEnumerable<TEntity> entities = _repository.GetAll(filter, asNoTracking, joinedEntities);

            return TypeAdapter.Adapt<IEnumerable<TEntity>, IEnumerable<Dto>>(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="filter"></param>
        /// <param name="noTracking"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public virtual IEnumerable<Dto> GetAll<Dto>(
            int pageIndex,
            int pageCount,
            Expression<Func<TEntity, bool>> filter = null,
            bool noTracking = false,
            params Expression<Func<TEntity, object>>[] includes) where Dto : class
        {
            IEnumerable<TEntity> entities = _repository.GetAll(pageIndex, pageCount, filter, noTracking, includes);

            return TypeAdapter.Adapt<IEnumerable<TEntity>, IEnumerable<Dto>>(entities);
        }




    }
}
