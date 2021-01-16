using AutoMapper;
using AutoMapper.QueryableExtensions;
using DhubSolutions.Core.Domain.Adapters;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DhubSolutions.Core.Application.Adapters
{
    /// <summary>
    ///     Automapper type adapter implementation
    /// </summary>
    public class AutoMapperTypeAdapter : ITypeAdapter
    {
        protected readonly IMapper _mapper;
        protected readonly MapperConfiguration _configuration;

        public AutoMapperTypeAdapter(IMapper mapper)
        {
            _mapper = mapper;
        }
        public AutoMapperTypeAdapter(MapperConfiguration configuration)
        {
            _mapper = configuration.CreateMapper();
            _configuration = configuration;
        }

        /// <summary>
        ///     Adapt a source object to an instance of type <typeparamref name="TTarget" />
        /// </summary>
        /// <typeparam name="TSource">Type of source item</typeparam>
        /// <typeparam name="TTarget">Type of target item</typeparam>
        /// <param name="source">Instance to adapt</param>
        /// <returns><paramref name="source" /> mapped to <typeparamref name="TTarget" /></returns>
        public TTarget Adapt<TSource, TTarget>(TSource source)
            where TSource : class
            where TTarget : class
        {
            return _mapper.Map<TSource, TTarget>(source);
        }

        /// <summary>
        ///     Adapt a source object to an instance of type <typeparamref name="TTarget" />
        /// </summary>
        /// <typeparam name="TTarget">Type of target item</typeparam>
        /// <param name="source">Instance to adapt</param>
        /// <returns><paramref name="source" /> mapped to <typeparamref name="TTarget" /></returns>
        public TTarget Adapt<TTarget>(object source) where TTarget : class
        {
            return _mapper.Map<TTarget>(source);
        }

        /// <summary>
        ///     Adapt a source object to an instance of type <typeparamref name="TTarget" />
        /// </summary>
        /// <typeparam name="TSource">Type of source item</typeparam>
        /// <typeparam name="TTarget">Type of target item</typeparam>
        /// <param name="source">Instance to adapt</param>
        /// <param name="target">Target instance</param>
        /// <returns><paramref name="source" /> mapped to <typeparamref name="TTarget" /></returns>

        public TTarget Adapt<TSource, TTarget>(TSource source, TTarget target)
            where TSource : class
            where TTarget : class
        {
            return _mapper.Map(source, target);
        }

        /// <summary>
        ///     Make a projection from a query of <typeparamref name="TSource" />'s objects to <typeparamref name="TTarget" />'s
        ///     objects
        /// </summary>
        /// <typeparam name="TSource">Type of source item</typeparam>
        /// <typeparam name="TTarget">Type of target item</typeparam>
        /// <param name="source">A query with objects to adapt</param>
        /// <param name="membersToExpand">Explicit members to expand</param>
        /// <returns></returns>
        public IQueryable<TTarget> Project<TSource, TTarget>(IQueryable<TSource> source,
            params Expression<Func<TTarget, object>>[] membersToExpand)
            where TSource : class
            where TTarget : class
        {
            return source.ProjectTo(_configuration, membersToExpand);
        }
    }
}
