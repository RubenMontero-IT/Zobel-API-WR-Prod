using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DhubSolutions.Core.Domain.Data
{
    /// <summary>
    /// Base contract for support 'dialect specific queries'.
    /// </summary>
    public interface ISql
    {
        /// <summary>
        /// Execute specific query with underliying persistence store
        /// </summary>
        /// <typeparam name="TEntity">Entity type to map query results</typeparam>
        /// <param name="sqlQuery">
        /// Dialect Query 
        /// <example>
        /// SELECT idCustomer,Name FROM dbo.[Customers] WHERE idCustomer > {0}
        /// </example>
        /// </param>
        /// <param name="parameters">A vector of parameters values</param>
        /// <returns>
        /// Enumerable results 
        /// </returns>
        IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, object parameters = null, bool buffered = true,
            IDbTransaction transaction = null, CommandType? commandType = null, int? commandTimeout = null);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sqlQuery"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> ExecuteQueryAsync<TEntity>(string sqlQuery, object parameters = null,
            IDbTransaction transaction = null, CommandType? commandType = null, int? commandTimeout = null);


        /// <summary>
        /// Execute arbitrary command into underliying persistence store
        /// </summary>
        /// <param name="sqlCommand">
        /// Command to execute
        /// <example>
        /// SELECT idCustomer,Name FROM dbo.[Customers] WHERE idCustomer > {0}
        /// </example>
        ///</param>
        /// <param name="parameters">A vector of parameters values</param>
        /// <returns>The number of affected records</returns>
        int ExecuteCommand(string sqlCommand, object parameters = null,
            IDbTransaction transaction = null, CommandType? commandType = null, int? commandTimeout = null);

        /// <summary>
        /// Execute arbitrary command into underliying persistence store
        /// </summary>
        /// <param name="sqlCommand">
        /// Command to execute
        /// <example>
        /// SELECT idCustomer,Name FROM dbo.[Customers] WHERE idCustomer > {0}
        /// </example>
        ///</param>
        /// <param name="parameters">A vector of parameters values</param>
        /// <returns>The number of affected records</returns>
        Task<int> ExecuteCommandAsync(string sqlCommand, object parameters = null,
            IDbTransaction transaction = null, CommandType? commandType = null, int? commandTimeout = null);


    }
}
