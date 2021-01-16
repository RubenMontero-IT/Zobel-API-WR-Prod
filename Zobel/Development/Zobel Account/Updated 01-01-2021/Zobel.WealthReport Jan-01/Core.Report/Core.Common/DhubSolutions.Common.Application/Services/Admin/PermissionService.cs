using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Common.Application.Services.Admin.Base;
using DhubSolutions.Common.Domain.Repositories.Admin;
using DhubSolutions.Core.Application.Services;
using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.Core.Domain.Data;

namespace DhubSolutions.Common.Application.Services.Admin
{
    public class PermissionService : ServiceMapper<Permission>, IPermissionService
    {
        public PermissionService(IUnitOfWork unitOfWork, ITypeAdapter typeAdapter, IPermissionRepository repository)
            : base(unitOfWork, typeAdapter, repository)
        {
        }
    }
}
