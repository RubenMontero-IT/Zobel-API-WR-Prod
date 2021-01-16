using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.WealthReport.Application.Services.Base;
using DhubSolutions.WealthReport.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DhubSolutions.WealthReport.Application.Services
{
    public abstract class WealthReportService<TEntity> : IWealthReportService<TEntity>
        where TEntity : class, new()
    {
        private readonly IWealthReportRepository<TEntity> _repository;

        protected WealthReportService(ITypeAdapter typeAdapter, IWealthReportRepository<TEntity> repository)
        {
            TypeAdapter = typeAdapter;
            _repository = repository;
        }

        public ITypeAdapter TypeAdapter { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="organization"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public TEntity Add<Dto>(Organization organization, Dto dto) where Dto : class
        {
            // load data from DTO to the entity
            TEntity entity = TypeAdapter.Adapt<Dto, TEntity>(dto);

            // add to entity repository
            _repository.Add(organization, entity);

            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="organization"></param>
        /// <param name="dtos"></param>
        public void AddRange<Dto>(Organization organization, IEnumerable<Dto> dtos) where Dto : class
        {
            var entities = TypeAdapter.Adapt<IEnumerable<Dto>, IEnumerable<TEntity>>(dtos);

            _repository.AddRange(organization, entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <returns></returns>
        public Dto Create<Dto>() where Dto : class
        {
            return TypeAdapter.Adapt<TEntity, Dto>(_repository.Create());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="organization"></param>
        /// <param name="entityKeyValues"></param>
        /// <returns></returns>
        public Dto Find<Dto>(Organization organization, params object[] entityKeyValues) where Dto : class
        {
            TEntity entity = _repository.Find(organization, entityKeyValues);
            if (entity != null)
                return TypeAdapter.Adapt<TEntity, Dto>(entity);

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="organization"></param>
        /// <param name="filter"></param>
        /// <param name="asNoTracking"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public Dto Get<Dto>(Organization organization,
            Expression<Func<TEntity, bool>> filter,
            bool asNoTracking = true,
            params Expression<Func<TEntity, object>>[] includes) where Dto : class
        {
            var entity = _repository.Get(organization, filter, asNoTracking, includes);

            return TypeAdapter.Adapt<Dto>(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="organization"></param>
        /// <param name="filter"></param>
        /// <param name="asNoTracking"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public IEnumerable<Dto> GetAll<Dto>(Organization organization,
            Expression<Func<TEntity,
                bool>> filter = null,
            bool asNoTracking = true,
            params Expression<Func<TEntity, object>>[] includes) where Dto : class
        {
            var entities = _repository.GetAll(organization, filter, asNoTracking, includes);

            return TypeAdapter.Adapt<IEnumerable<Dto>>(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="organization"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="asNotracking"></param>
        /// <param name="filter"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public IEnumerable<Dto> GetAll<Dto>(Organization organization,
            int pageIndex,
            int pageCount,
            bool asNotracking = true,
            Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includes) where Dto : class
        {
            var entities = _repository.GetAll(organization, pageIndex, pageCount, filter, asNotracking, includes);

            return TypeAdapter.Adapt<IEnumerable<Dto>>(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="organization"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public TEntity Remove<Dto>(Organization organization, Dto dto) where Dto : class
        {
            TEntity entity = TypeAdapter.Adapt<Dto, TEntity>(dto);

            _repository.Remove(organization, entity);

            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="organization"></param>
        /// <param name="dtos"></param>
        public void RemoveRange<Dto>(Organization organization, IEnumerable<Dto> dtos) where Dto : class
        {
            IEnumerable<TEntity> entities = TypeAdapter.Adapt<IEnumerable<Dto>, IEnumerable<TEntity>>(dtos);

            _repository.RemoveRange(organization, entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="organization"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public TEntity Update<Dto>(Organization organization, Dto dto) where Dto : class
        {
            TEntity entity = TypeAdapter.Adapt<Dto, TEntity>(dto);

            _repository.Update(organization, entity);

            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="organization"></param>
        /// <param name="dtos"></param>
        public void UpdateRange<Dto>(Organization organization, IEnumerable<Dto> dtos) where Dto : class
        {
            IEnumerable<TEntity> entities = TypeAdapter.Adapt<IEnumerable<Dto>, IEnumerable<TEntity>>(dtos);

            _repository.UpdateRange(organization, entities);
        }
    }
}
