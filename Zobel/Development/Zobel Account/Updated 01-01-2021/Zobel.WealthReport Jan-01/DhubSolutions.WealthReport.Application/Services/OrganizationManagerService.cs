using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.WealthReport.Application.Services.Base;
using DhubSolutions.WealthReport.Domain.Entities;
using DhubSolutions.WealthReport.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DhubSolutions.WealthReport.Application.Services
{
    public class OrganizationManagerService : IOrganizationManagerService
    {
        private readonly IConnectionByOrganizationRepository _repository;
        private readonly IConfiguration _configuration;

        public OrganizationManagerService(IConfiguration configuration, IConnectionByOrganizationRepository repository)
        {
            _configuration = configuration;
            _repository = repository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns></returns>
        public string GetConnectionString(Organization organization)
        {
            string connectionId = GetConnectionId(organization);

            if (string.IsNullOrEmpty(connectionId) || string.IsNullOrWhiteSpace(connectionId))
                throw new InvalidOperationException($"The connection string name does not have a valid value.");

            return _configuration.GetConnectionString(connectionId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        public ConnectionByOrganization GetConnectionByOrganization(Organization organization)
        {
            return _repository.GetConnectionByOrganization(organization);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConnectionByOrganization> GetAllConnectionByOrganizations()
        {
            return _repository.GetAllConnectionByOrganizations();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns></returns>
        public string GetConnectionId(Organization organization)
        {
            string connectionId = _repository.GetConnectionId(organization);
            if (connectionId is null)
                throw new NullReferenceException($"No connection string identifier found for {organization.OrganizationName}");

            return connectionId;
        }
    }
}
