using Dapper;
using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.WealthReport.Application.Services.Base;
using DhubSolutions.WealthReport.Application.Services.Tools;
using DhubSolutions.WealthReport.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DhubSolutions.WealthReport.Application.Services
{
    public class ProcessManagerService : IProcessManagerService
    {
        private readonly Dictionary<string, Process> processes;
        private readonly IOrganizationManagerService _connectionStringGeneratorService;

        public ProcessManagerService(IOrganizationManagerService connectionStringGeneratorService)
        {
            processes = new Dictionary<string, Process>();
            _connectionStringGeneratorService = connectionStringGeneratorService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        public ProcessStatus GetProcessStatus(Organization organization, Product product)
        {
            if (!processes.TryGetValue(product.Id, out Process process))
            {
                process = GetLastProcess(organization, product);
                if (product == null)
                    throw new NullReferenceException(nameof(process));

                processes.Add(product.Id, process);
            }

            using (IDbConnection connection = GetConnection(organization))
            {
                var parameters = new { processID = process.ProcessId };

                connection.Open();
                string processStatus = connection.ExecuteScalar<string>(sql: "GetProcessStatus", param: parameters);

                return Enum.Parse<ProcessStatus>(processStatus, ignoreCase: true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        public Process GetLastProcess(Organization organization, Product product)
        {
            using (IDbConnection connection = GetConnection(organization))
            {
                connection.Open();
                string processId = connection.ExecuteScalar<string>(sql: "GetLastProcessId");
                if (processId is null)
                    return default;
                return new Process(processId);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <param name="process"></param>
        /// <returns></returns>
        public void AddProcess(Product product, Process process)
        {
            processes.Add(product.Id, process);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        public void RemoveProcess(Product product)
        {
            processes.Remove(product.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="processName"></param>
        /// <returns></returns>
        public bool AllProcessFinished(Organization organization, string processName = "TL_PAO")
        {
            using (IDbConnection connection = GetConnection(organization))
            {
                connection.Open();
                IEnumerable<ProcessLog> processLogs = connection.Query<ProcessLog>(
                                                            sql: "GetAllProcessLog",
                                                            param: new { IProcessName = processName });

                return !processLogs.Any(processLog => processLog.ProcessStatus == ProcessStatus.Running);
            }
        }

        #region private methods

        IDbConnection GetConnection(Organization organization)
        {
            try
            {
                string connectionString = _connectionStringGeneratorService.GetConnectionString(organization);

                return new SqlConnection(connectionString);
            }
            catch (Exception)
            {

                throw;
            }


        }



        #endregion



    }
}
