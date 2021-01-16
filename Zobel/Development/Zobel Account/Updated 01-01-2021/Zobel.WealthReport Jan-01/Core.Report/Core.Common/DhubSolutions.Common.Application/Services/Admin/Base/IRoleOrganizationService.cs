using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Domain.Services;
using System.Collections.Generic;

namespace DhubSolutions.Common.Application.Services.Admin.Base
{
    public interface IOrganizationRoleService : IServiceMapper<OrganizationRole>
    {
        IEnumerable<SystemGroupMemberShip> GetSystemGroupMemberShips(string orgRoleId);

        bool IsReportMaster(OrganizationRole organizatioRole);
    }
}
