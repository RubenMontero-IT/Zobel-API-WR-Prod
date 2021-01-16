using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Common.Domain.Entities.Application;
using DhubSolutions.Core.Domain.Services;
using System.Collections.Generic;

namespace DhubSolutions.Common.Application.Services.Application.Base
{
    public interface IAppService : IServiceMapper<Apps>
    {
        /// <summary>
        /// All the apps that a user has access to
        /// </summary>
        /// <param name="user">The user to be checked</param>
        /// <returns>A collection with all the apps accessible by this user</returns>
        IEnumerable<Apps> GetAllUserApps(User user);


        /// <summary>
        /// Gets all the apps that belongs to an organization and role
        /// </summary>
        /// <param name="organization">the organization to be checked</param>
        /// <param name="roleValue">the role value to be checked</param>
        /// <returns>All the apps that have that org and role in his permisions</returns>
        IEnumerable<Apps> GetAppsByOrganizarionRole(Organization organization, RoleValue roleValue);

    }
}
