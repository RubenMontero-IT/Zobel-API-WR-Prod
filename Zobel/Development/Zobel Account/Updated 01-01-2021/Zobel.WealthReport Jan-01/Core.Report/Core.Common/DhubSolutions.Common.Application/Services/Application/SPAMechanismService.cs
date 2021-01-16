using DhubSolutions.Common.Domain.Entities.Application;
using DhubSolutions.Common.Application.Services.Application.Base;
using DhubSolutions.Common.Domain.Repositories.Application;
using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.Core.Application.Services;

namespace DhubSolutions.Common.Application.Services.Application
{
    public class SPAMechanismService : ServiceMapper<SPAMechanism>, ISPAMechanismService
    {
        public SPAMechanismService(IUnitOfWork unitOfWork, ITypeAdapter typeAdapter, ISPAMechanismRepository repository)
            : base(unitOfWork, typeAdapter, repository)
        {

        }
    }
}
