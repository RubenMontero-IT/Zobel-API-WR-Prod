using Dapper;
using DhubSolutions.WealthReport.Domain.Repositories;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Repositories
{
    public class QueryableRepository : IQueryableRepository
    {
        private string _connectionString;

        public QueryableRepository()
        {
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
            using (IDbConnection connection = GetDbConnection())
            {
                connection.Open();

                return connection.Execute(
                       sql: sqlCommand,
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
            using (IDbConnection connection = GetDbConnection())
            {
                connection.Open();

                return await connection.ExecuteAsync(
                        sql: sqlCommand,
                        param: parameters,
                        transaction: transaction,
                        commandTimeout: commandTimeout);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlQuery"></param>
        /// <param name="parameters"></param>
        /// <param name="buffered"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public IEnumerable<T> ExecuteQuery<T>(string sqlQuery, object parameters = null, bool buffered = true, IDbTransaction transaction = null, CommandType? commandType = null, int? commandTimeout = null)
        {
            using (IDbConnection connection = GetDbConnection())
            {
                connection.Open();

                return connection.Query<T>(
                    sql: sqlQuery,
                    param: parameters,
                    transaction: transaction,
                    buffered: buffered,
                    commandTimeout: commandTimeout,
                    commandType: commandType);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlQuery"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string sqlQuery, object parameters = null, IDbTransaction transaction = null, CommandType? commandType = null, int? commandTimeout = null)
        {
            using (IDbConnection connection = GetDbConnection())
            {
                connection.Open();

                return await connection.QueryAsync<T>(
                      sql: sqlQuery,
                      param: parameters,
                      transaction: transaction,
                      commandTimeout: commandTimeout,
                      commandType: commandType);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public void SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }


        #region Private methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IDbConnection GetDbConnection()
        {
            return new SqlConnection(_connectionString);
        }

        #endregion
    }
}
