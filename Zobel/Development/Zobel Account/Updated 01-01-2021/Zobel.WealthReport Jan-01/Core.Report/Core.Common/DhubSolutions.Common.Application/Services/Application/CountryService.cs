using DhubSolutions.Common.Domain.Entities.Application;
using DhubSolutions.Common.Application.Services.Application.Base;
using DhubSolutions.Core.Domain.Common.Repositories.Application;
using DhubSolutions.Core.Application.Services;
using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Core.Domain.Adapters;

namespace DhubSolutions.Common.Application.Services.Application
{
    public class CountryService : ServiceMapper<Country>, ICountryService
    {

        public CountryService(IUnitOfWork unitOfWork, ITypeAdapter typeAdapter, ICountryRepository repository)
            : base(unitOfWork, typeAdapter, repository)
        {

        }
    }
}
