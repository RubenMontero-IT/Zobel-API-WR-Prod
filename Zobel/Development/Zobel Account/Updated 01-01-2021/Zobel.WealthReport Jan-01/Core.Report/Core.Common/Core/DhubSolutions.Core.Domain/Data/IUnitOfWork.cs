using DhubSolutions.Core.Domain.Data.Repositories;

namespace DhubSolutions.Core.Domain.Data
{
    /// <summary>
    ///     Contract for UnitOfWork pattern.
    /// </summary>
    public interface IUnitOfWork
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Repository"></typeparam>
        /// <returns></returns>
        Repository GetRepository<Repository>() where Repository : IRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="RepositoryAsync"></typeparam>
        /// <returns></returns>
        RepositoryAsync GetRepositoryAsync<RepositoryAsync>() where RepositoryAsync : IRepositoryAsync;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="IReadOnlyRespository"></typeparam>
        /// <returns></returns>
        ReadOnlyRepository GetReadOnlyRepository<ReadOnlyRepository>() where ReadOnlyRepository : IReadOnlyRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="ReadOnlyRepositoryAsync"></typeparam>
        /// <returns></returns>
        ReadOnlyRepositoryAsync GetReadOnlyRepositoryAsync<ReadOnlyRepositoryAsync>() where ReadOnlyRepositoryAsync : IReadOnlyRepositoryAsync;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
    }
}
