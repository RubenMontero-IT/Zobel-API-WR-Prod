using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Common.Application.Services.Admin.Base;
using DhubSolutions.Common.Domain.Repositories.Admin;
using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.Core.Application.Services;
using System.Linq;
using System.Collections.Generic;

namespace DhubSolutions.Common.Application.Services.Admin
{
    public class OrganizationRoleService : ServiceMapper<OrganizationRole>, IOrganizationRoleService
    {
        public OrganizationRoleService(IUnitOfWork unitOfWork, ITypeAdapter typeAdapter, IOrganizationRoleRepository repository)
            : base(unitOfWork, typeAdapter, repository)
        {
        }

        public IEnumerable<SystemGroupMemberShip> GetSystemGroupMemberShips(string orgRoleId)
        {
            return ((IOrganizationRoleRepository)_repository).GetSystemGroupMemberShips(orgRoleId);
        }

        public bool IsReportMaster(OrganizationRole organizatioRole)
        {
            return organizatioRole.SystemGroupMemberShips.Any(s => s.SystemGroup.Code == "report_master");

        }
    }
}
