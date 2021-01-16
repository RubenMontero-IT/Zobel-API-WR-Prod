using DhubSolutions.Common.Domain.Entities.Application;
using DhubSolutions.Common.Application.Services.Application.Base;
using DhubSolutions.Common.Domain.Repositories.Application;
using DhubSolutions.Core.Application.Services;
using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Core.Domain.Adapters;

namespace DhubSolutions.Common.Application.Services.Application
{
    public class CurrencyService : ServiceMapper<Currency>, ICurrencyService
    {
        public CurrencyService(IUnitOfWork unitOfWork, ITypeAdapter typeAdapter, ICurrencyRepository repository)
            : base(unitOfWork, typeAdapter, repository)
        {

        }
    }
}
