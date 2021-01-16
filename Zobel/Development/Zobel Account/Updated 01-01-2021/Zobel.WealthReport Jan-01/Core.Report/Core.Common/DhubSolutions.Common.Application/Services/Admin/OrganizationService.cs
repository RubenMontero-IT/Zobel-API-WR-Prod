using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Common.Application.Services.Admin.Base;
using DhubSolutions.Common.Domain.Repositories.Admin;
using DhubSolutions.Core.Application.Services;
using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Core.Domain.Adapters;
using System.Collections.Generic;
using System;

namespace DhubSolutions.Common.Application.Services.Admin
{
    public class OrganizationService : ServiceMapper<Organization>, IOrganizationService
    {
        public OrganizationService(IUnitOfWork unitOfWork, ITypeAdapter typeAdapter, IOrganizationRepository repository)
            : base(unitOfWork, typeAdapter, repository)
        {
        }

        public IEnumerable<OrganizationRole> GetOrganizationRoles(string orgId)
        {
            throw new NotImplementedException();
        }
    }
}
