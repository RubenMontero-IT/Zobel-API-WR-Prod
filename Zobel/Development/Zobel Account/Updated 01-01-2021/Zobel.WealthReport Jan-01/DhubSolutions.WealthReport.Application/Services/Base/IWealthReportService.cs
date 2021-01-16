using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Domain.Adapters;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DhubSolutions.WealthReport.Application.Services.Base
{
    public interface IWealthReportService<TEntity> where TEntity : class
    {
        ITypeAdapter TypeAdapter { get; }

        TEntity Add<Dto>(Organization organization, Dto dto) where Dto : class;
        void AddRange<Dto>(Organization organization, IEnumerable<Dto> dtos) where Dto : class;
        Dto Create<Dto>() where Dto : class;
        Dto Find<Dto>(Organization organization, params object[] entityKeyValues) where Dto : class;
        Dto Get<Dto>(Organization organization, Expression<Func<TEntity, bool>> filter, bool asNoTracking = true, params Expression<Func<TEntity, object>>[] includes) where Dto : class;
        IEnumerable<Dto> GetAll<Dto>(Organization organization, Expression<Func<TEntity, bool>> filter = null, bool asNoTracking = true, params Expression<Func<TEntity, object>>[] includes) where Dto : class;
        IEnumerable<Dto> GetAll<Dto>(Organization organization, int pageIndex, int pageCount, bool asNoTracking = true, Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes) where Dto : class;
        TEntity Remove<Dto>(Organization organization, Dto dto) where Dto : class;
        void RemoveRange<Dto>(Organization organization, IEnumerable<Dto> dtos) where Dto : class;
        TEntity Update<Dto>(Organization organization, Dto dto) where Dto : class;
        void UpdateRange<Dto>(Organization organization, IEnumerable<Dto> dtos) where Dto : class;

    }
}