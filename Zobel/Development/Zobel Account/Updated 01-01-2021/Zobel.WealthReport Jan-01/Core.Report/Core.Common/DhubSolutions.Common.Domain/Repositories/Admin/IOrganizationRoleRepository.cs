using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Domain.Data.Repositories;
using System.Collections.Generic;

namespace DhubSolutions.Common.Domain.Repositories.Admin
{
    public interface IOrganizationRoleRepository : IRepository<OrganizationRole>
    {
        IEnumerable<SystemGroupMemberShip> GetSystemGroupMemberShips(string orgRoleId);
    }
}
