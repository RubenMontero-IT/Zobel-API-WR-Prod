using DhubSolutions.Common.Domain.Entities.Application;
using DhubSolutions.Common.Application.Services.Application.Base;
using DhubSolutions.Common.Domain.Repositories.Application;
using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.Core.Application.Services;

namespace DhubSolutions.Common.Application.Services.Application
{
    public class IndustryService : ServiceMapper<Industry>, IIndustryService
    {
        public IndustryService(IUnitOfWork unitOfWork, ITypeAdapter typeAdapter, IIndustryRepository repository)
            : base(unitOfWork, typeAdapter, repository)
        {

        }
    }
}
