using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Common.Application.Services.Admin.Base;
using DhubSolutions.Common.Domain.Repositories.Admin;
using DhubSolutions.Core.Application.Services;
using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Core.Domain.Adapters;

namespace DhubSolutions.Common.Application.Services.Admin
{
    public class LogUserByAppService : ServiceMapper<LogUserByApp>, ILogUserByAppService
    {
        public LogUserByAppService(IUnitOfWork unitOfWork, ITypeAdapter typeAdapter, ILogUserByAppRepository repository)
            : base(unitOfWork, typeAdapter, repository)
        {
        }
    }
}
