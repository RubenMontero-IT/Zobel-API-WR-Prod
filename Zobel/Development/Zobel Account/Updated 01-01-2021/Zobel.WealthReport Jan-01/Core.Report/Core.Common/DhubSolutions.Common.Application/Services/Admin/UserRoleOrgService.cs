using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Common.Domain.Repositories.Admin;
using DhubSolutions.Common.Application.Services.Admin.Base;
using DhubSolutions.Core.Application.Services;
using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Core.Domain.Adapters;

namespace DhubSolutions.Common.Application.Services.Admin
{
    public class UserRoleOrgService : ServiceMapper<UserRoleOrg>, IUserRoleOrgService
    {
        public UserRoleOrgService(IUnitOfWork unitOfWork, ITypeAdapter typeAdapter, IUserRoleOrgRepository repository)
            : base(unitOfWork, typeAdapter, repository)
        {
        }
    }
}
