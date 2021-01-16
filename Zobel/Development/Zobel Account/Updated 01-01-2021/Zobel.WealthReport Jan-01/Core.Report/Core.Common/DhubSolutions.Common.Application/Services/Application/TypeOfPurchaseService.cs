using DhubSolutions.Common.Domain.Entities.Application;
using DhubSolutions.Common.Application.Services.Application.Base;
using DhubSolutions.Common.Domain.Repositories.Application;
using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Core.Application.Services;
using DhubSolutions.Core.Domain.Adapters;

namespace DhubSolutions.Common.Application.Services.Application
{
    public class TypeOfPurchaseService : ServiceMapper<TypeOfPurchase>, ITypeOfPurchaseService
    {
        public TypeOfPurchaseService(IUnitOfWork unitOfWork, ITypeAdapter typeAdapter, ITypeOfPurchaseRepository repository)
            : base(unitOfWork, typeAdapter, repository)
        {
        }
    }
}
