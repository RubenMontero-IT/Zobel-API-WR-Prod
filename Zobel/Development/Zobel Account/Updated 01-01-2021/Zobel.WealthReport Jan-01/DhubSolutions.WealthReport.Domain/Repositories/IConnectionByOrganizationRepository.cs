using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.WealthReport.Domain.Entities;
using System.Collections.Generic;

namespace DhubSolutions.WealthReport.Domain.Repositories
{
    public interface IConnectionByOrganizationRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        string GetConnectionId(Organization organization);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<ConnectionByOrganization> GetAllConnectionByOrganizations();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        ConnectionByOrganization GetConnectionByOrganization(Organization organization);
    }
}
