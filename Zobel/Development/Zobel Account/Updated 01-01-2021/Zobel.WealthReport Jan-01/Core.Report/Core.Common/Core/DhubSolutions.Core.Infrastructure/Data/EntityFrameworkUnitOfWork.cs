using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Linq;
using System.Data.SqlClient;
using System.Reflection;
using DhubSolutions.Core.Domain.Data.Repositories;

namespace DhubSolutions.Core.Infrastructure.Data
{
    public class EntityFrameworkUnitOfWork<TContex> : IEntityFrameworkUnitOfWork<TContex>
        where TContex : DbContext
    {
        private readonly Dictionary<string, IRepository> repositories;
        private readonly Dictionary<string, IRepositoryAsync> asynchronousRepositories;
        private readonly Dictionary<string, IReadOnlyRepository> readOnlyRepositories;
        private readonly Dictionary<string, IReadOnlyRepositoryAsync> asynchronousReadOnlyRepositories;

        public EntityFrameworkUnitOfWork(TContex context)
        {
            Context = context;

            repositories = new Dictionary<string, IRepository>();
            asynchronousRepositories = new Dictionary<string, IRepositoryAsync>();
            readOnlyRepositories = new Dictionary<string, IReadOnlyRepository>();
            asynchronousReadOnlyRepositories = new Dictionary<string, IReadOnlyRepositoryAsync>();

            InitRepositories();

            void InitRepositories()
            {

                IEnumerable<Type> allTypes = Assembly.GetEntryAssembly()
                        .GetReferencedAssemblies()
                        .Select(assemblyName => Assembly.Load(assemblyName))
                        .SelectMany(assembly => assembly.GetTypes());

                InitRepositories<IRepository>(allTypes, type =>
                {
                    repositories[type.Name] = Activator.CreateInstance(type, Context, this) as IRepository;
                });

                InitRepositories<IRepositoryAsync>(allTypes, type =>
                {
                    asynchronousRepositories[type.Name] = Activator.CreateInstance(type, Context, this) as IRepositoryAsync;
                });

                InitRepositories<IReadOnlyRepository>(allTypes, type =>
                {
                    readOnlyRepositories[type.Name] = Activator.CreateInstance(type, Context, this) as IReadOnlyRepository;
                });

                InitRepositories<IReadOnlyRepositoryAsync>(allTypes, type =>
                {
                    asynchronousReadOnlyRepositories[type.Name] = Activator.CreateInstance(type, Context, this) as IReadOnlyRepositoryAsync;
                });


                void InitRepositories<Repository>(IEnumerable<Type> types, Action<Type> action) where Repository : class
                {
                    Type repositoryType = typeof(Repository);

                    types.Where(type => repositoryType.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                   .ToList().ForEach(type => action(type));
                }
            }
        }
        public TContex Context { get; }

        #region IQueryableUnitOfWork

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="original"></param>
        /// <param name="current"></param>
        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class
        {
            Context.Entry(original).CurrentValues.SetValues(current);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public void Attach<TEntity>(TEntity entity) where TEntity : class
        {
            Context.Entry(entity).State = EntityState.Unchanged;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public void Detach<TEntity>(TEntity entity) where TEntity : class
        {
            Context.Entry(entity).State = EntityState.Detached;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public void SetModified<TEntity>(TEntity entity) where TEntity : class
        {
            Context.Entry(entity).State = EntityState.Modified;
        }
        #endregion

        #region ISql

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sqlQuery"></param>
        /// <param name="parameters"></param>
        /// <param name="buffered"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, object parameters = null, bool buffered = true, IDbTransaction transaction = null, CommandType? commandType = null, int? commandTimeout = null)
        {
            using (IDbConnection connection = Connection)
            {
                return connection.Query<TEntity>(sqlQuery,
                                                 param: parameters,
                                                 buffered: buffered,
                                                 transaction: transaction,
                                                 commandType: commandType,
                                                 commandTimeout: commandTimeout);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sqlQuery"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> ExecuteQueryAsync<TEntity>(string sqlQuery, object parameters = null, IDbTransaction transaction = null, CommandType? commandType = null, int? commandTimeout = null)
        {
            using (IDbConnection connection = Connection)
            {
                return await connection.QueryAsync<TEntity>(sqlQuery,
                                                            param: parameters,
                                                            transaction: transaction,
                                                            commandType: commandType,
                                                            commandTimeout: commandTimeout);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public int ExecuteCommand(string sqlCommand, object parameters = null, IDbTransaction transaction = null, CommandType? commandType = null, int? commandTimeout = null)
        {
            using (IDbConnection connection = Connection)
            {
                return connection.Execute(sqlCommand,
                                          param: parameters,
                                          transaction: transaction,
                                          commandTimeout: commandTimeout);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public async Task<int> ExecuteCommandAsync(string sqlCommand, object parameters = null, IDbTransaction transaction = null, CommandType? commandType = null, int? commandTimeout = null)
        {
            using (IDbConnection connection = Connection)
            {
                return await connection.ExecuteAsync(sqlCommand,
                                                     param: parameters,
                                                     transaction: transaction,
                                                     commandTimeout: commandTimeout);
            }
        }

        private IDbConnection Connection
        {
            get
            {
                var connectionString = Context.Database.GetDbConnection().ConnectionString;
                return new SqlConnection(connectionString);
            }
        }

        #endregion

        #region IUnitOfWork
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Repository"></typeparam>
        /// <returns></returns>
        public Repository GetRepository<Repository>() where Repository : IRepository
        {
            if (repositories.TryGetValue(typeof(Repository).Name, out IRepository repository))
                return (Repository)repository;

            return default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="RepositoryAsync"></typeparam>
        /// <returns></returns>
        public RepositoryAsync GetRepositoryAsync<RepositoryAsync>() where RepositoryAsync : IRepositoryAsync
        {
            if (asynchronousRepositories.TryGetValue(typeof(RepositoryAsync).Name, out IRepositoryAsync repository))
                return (RepositoryAsync)repository;

            return default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="ReadOnlyRepository"></typeparam>
        /// <returns></returns>
        public ReadOnlyRepository GetReadOnlyRepository<ReadOnlyRepository>() where ReadOnlyRepository : IReadOnlyRepository
        {
            if (readOnlyRepositories.TryGetValue(typeof(ReadOnlyRepository).Name, out IReadOnlyRepository repository))
                return (ReadOnlyRepository)repository;

            return default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="ReadOnlyRepositoryAsync"></typeparam>
        /// <returns></returns>
        public ReadOnlyRepositoryAsync GetReadOnlyRepositoryAsync<ReadOnlyRepositoryAsync>() where ReadOnlyRepositoryAsync : IReadOnlyRepositoryAsync
        {
            if (asynchronousReadOnlyRepositories.TryGetValue(typeof(ReadOnlyRepositoryAsync).Name, out IReadOnlyRepositoryAsync repository))
                return (ReadOnlyRepositoryAsync)repository;

            return default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        #endregion
    }
}
