using DhubSolutions.Common.Domain.Entities.Application;
using DhubSolutions.Common.Application.Services.Application.Base;
using DhubSolutions.Common.Domain.Repositories.Application;
using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.Core.Application.Services;

namespace DhubSolutions.Common.Application.Services.Application
{
    public class SellerService : ServiceMapper<Seller>, ISellerService
    {
        public SellerService(IUnitOfWork unitOfWork, ITypeAdapter typeAdapter, ISellerRepository repository)
            : base(unitOfWork, typeAdapter, repository)
        {

        }
    }
}
