using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Core.Domain.Data.Repositories;
using DhubSolutions.Core.Domain.Entity;
using DhubSolutions.Core.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DhubSolutions.Core.Application.Services
{
    public class ServiceMapperAsync<TEntity> : IServiceMapperAsync<TEntity>
        where TEntity : class, IEntity, new()
    {
        private readonly IRepositoryAsync<TEntity> _repository;

        public ServiceMapperAsync(IUnitOfWork unitOfWork, ITypeAdapter typeAdapter, IRepositoryAsync<TEntity> repository)
        {
            UnitOfWork = unitOfWork;
            TypeAdapter = typeAdapter;
            _repository = repository;
        }

        /// <summary>
        ///     Gets the unit of work in this repository.
        /// </summary>
        public IUnitOfWork UnitOfWork { get; }

        /// <summary>
        ///     Add item into repository.
        /// </summary>
        /// <param name="item">Item to add to repository.</param>
        public ITypeAdapter TypeAdapter { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="dto">Item to add to repository</param>
        /// <returns></returns>
        public virtual async Task<TEntity> AddAsync<Dto>(Dto dto) where Dto : class
        {
            // load data from DTO to the entity
            TEntity entity = TypeAdapter.Adapt<Dto, TEntity>(dto);

            // add to entity repository
            await _repository.AddAsync(entity);

            return entity;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public virtual async Task AddRangeAsync<Dto>(IEnumerable<Dto> dtos)
        {
            var entities = TypeAdapter.Adapt<IEnumerable<Dto>, IEnumerable<TEntity>>(dtos);

            await _repository.AddRangeAsync(entities);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="entityKeyValues"></param>
        /// <returns></returns>
        public virtual async Task<Dto> FindAsync<Dto>(params object[] entityKeyValues) where Dto : class
        {
            TEntity entity = await _repository.FindAsync(entityKeyValues);
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
        public virtual async Task<Dto> GetAsync<Dto>(
            Expression<Func<TEntity, bool>> filter,
            bool asNoTracking = false,
            params Expression<Func<TEntity, object>>[] joinedEntities) where Dto : class
        {
            TEntity entity = await _repository.GetAsync(filter, asNoTracking, joinedEntities);

            return TypeAdapter.Adapt<TEntity, Dto>(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="filter"></param>
        /// <param name="noTracking"></param>
        /// <param name="joinedEntities"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<Dto>> GetAllAsync<Dto>(
            Expression<Func<TEntity, bool>> filter = null,
            bool noTracking = false,
            params Expression<Func<TEntity, object>>[] joinedEntities) where Dto : class
        {
            IEnumerable<TEntity> entities = await _repository.GetAllAsync(filter, noTracking, joinedEntities);

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
        public virtual async Task<IEnumerable<Dto>> GetAllAsync<Dto>(
            int pageIndex,
            int pageCount,
            Expression<Func<TEntity, bool>> filter = null,
            bool noTracking = false,
            params Expression<Func<TEntity, object>>[] includes) where Dto : class
        {
            IEnumerable<TEntity> entities = await _repository.GetAllAsync(pageIndex, pageCount, filter, noTracking, includes);

            return TypeAdapter.Adapt<IEnumerable<TEntity>, IEnumerable<Dto>>(entities);
        }
    }
}
