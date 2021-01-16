using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Common.Application.Services.Admin.Base;
using DhubSolutions.Common.Domain.Repositories.Admin;
using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Core.Application.Services;
using DhubSolutions.Core.Domain.Adapters;

namespace DhubSolutions.Reports.Application.Services.ReportManager
{
    public class SystemGroupService : ServiceMapper<SystemGroup>, ISystemGroupService
    {
        public SystemGroupService(IUnitOfWork unitOfWork, ITypeAdapter typeAdapter, ISystemGroupRepository repository)
            : base(unitOfWork, typeAdapter, repository)
        {
        }
    }
}
