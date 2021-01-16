using Dapper;
using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.WealthReport.Domain.Entities;
using DhubSolutions.WealthReport.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Repositories
{
    public class ConnectionByOrganizationRepository : IConnectionByOrganizationRepository
    {
        private readonly string _connectionString;

        public ConnectionByOrganizationRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Zobel_ConnectionString");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConnectionByOrganization> GetAllConnectionByOrganizations()
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                string sqlQuery = @"select ORGID from wrp.ConnectionByOrganization";

                connection.Open();

                return connection.Query(sql: sqlQuery)
                    .Select(o => new ConnectionByOrganization { Id = o.ORGID, ConnectionID = o.ConnectionID });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        public ConnectionByOrganization GetConnectionByOrganization(Organization organization)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var param = new { OrgId = organization.Id };

                string sqlQuery = @"select * from wrp.ConnectionByOrganization
                                              where ORGID=@OrgId";

                connection.Open();

                dynamic obj = connection.QuerySingle(sql: sqlQuery, param: param);

                return new ConnectionByOrganization { Id = obj.ORGID, ConnectionID = obj.ConnectionID };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        public string GetConnectionId(Organization organization)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var param = new { OrgId = organization.Id };

                string sqlQuery = @"select ConnectionID 
                                    from wrp.ConnectionByOrganization
                                    where ORGID=@OrgId";

                connection.Open();

                return connection.ExecuteScalar<string>(sql: sqlQuery, param: param);
            }
        }
    }

}