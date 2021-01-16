using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Domain.Services;
using System.Collections.Generic;

namespace DhubSolutions.Common.Application.Services.Admin.Base
{
    public interface IOrganizationService : IServiceMapper<Organization>
    {
        IEnumerable<OrganizationRole> GetOrganizationRoles(string orgId);
    }
}
