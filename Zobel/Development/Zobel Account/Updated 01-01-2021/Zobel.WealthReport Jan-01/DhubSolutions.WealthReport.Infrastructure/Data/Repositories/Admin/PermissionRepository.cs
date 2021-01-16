using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Common.Domain.Repositories.Admin;
using DhubSolutions.Core.Infrastructure.Data;
using DhubSolutions.Core.Infrastructure.Data.Repositories;
using DhubSolutions.WealthReport.Infrastructure.Data.Context;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Repositories.Admin
{
    public class PermissionRepository : Repository<Permission>, IPermissionRepository
    {
        public PermissionRepository(ProjectManagementDbContext context, IEntityFrameworkUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }
    }
}
