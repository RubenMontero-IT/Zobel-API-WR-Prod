using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.WealthReport.Domain.Entities;
using System;
using System.Collections.Generic;

namespace DhubSolutions.WealthReport.Application.Services.Base
{
    public interface IOrganizationManagerService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns></returns>
        string GetConnectionId(Organization organization);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns></returns>
        string GetConnectionString(Organization organization);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<ConnectionByOrganization> GetAllConnectionByOrganizations();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        ConnectionByOrganization GetConnectionByOrganization(Organization organization);
    }
}
