using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Common.Domain.Entities.Application;
using DhubSolutions.Common.Application.Services.Application.Base;
using DhubSolutions.Common.Domain.Repositories.Application;
using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.Core.Application.Services;
using System.Collections.Generic;

namespace DhubSolutions.Common.Application.Services.Application
{
    public class AppService : ServiceMapper<Apps>, IAppService
    {
        public AppService(IUnitOfWork unitOfWork, ITypeAdapter typeAdapter, IAppRepository repository)
            : base(unitOfWork, typeAdapter, repository)
        {
        }

        /// <summary>
        /// All the apps that a user has access to
        /// </summary>
        /// <param name="user">The user to be checked</param>
        /// <returns>A collection with all the apps accessible by this user</returns>
        public IEnumerable<Apps> GetAllUserApps(User user)
        {
            var allApps = _repository.GetAll();
            var filteredApps = new List<Apps>();
            foreach (var app in allApps)
            {
                var currentAppPermisions = new List<RoleAppPermission>(app.RoleAppPermission);
                foreach (var appPermission in currentAppPermisions)
                {
                    //Go for the apps permisions, and foreach check if the user has a comb role-org equals the current app permisions 
                    List<UserRoleOrg> userRoles = new List<UserRoleOrg>(user.UserRoleOrg);
                    if (userRoles.Exists(role => role.Orgid == appPermission.Orgid && role.Rvid == appPermission.Rvid))
                    {
                        filteredApps.Add(app);
                    }
                }
            }
            return filteredApps;
        }


        /// <summary>
        /// Gets all the apps that belongs to an organization and role
        /// </summary>
        /// <param name="organization">the organization to be checked</param>
        /// <param name="roleValue">the role value to be checked</param>
        /// <returns>All the apps that have that org and role in his permisions</returns>
        public IEnumerable<Apps> GetAppsByOrganizarionRole(Organization organization, RoleValue roleValue)
        {
            var allApps = _repository.GetAll();
            var filteredApps = new List<Apps>();
            foreach (var app in allApps)
            {
                var currentAppPermisions = new List<RoleAppPermission>(app.RoleAppPermission);
                if (currentAppPermisions.Exists(rap => rap.OrganizationRole.OrganizationId == organization.Id && rap.OrganizationRole.Rvid == roleValue.Id))
                {
                    filteredApps.Add(app);
                }
            }
            return filteredApps;
        }
    }
}
