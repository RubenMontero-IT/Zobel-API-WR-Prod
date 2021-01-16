using DhubSolutions.Common.Domain.Entities.Application;
using DhubSolutions.Common.Application.Services.Application.Base;
using DhubSolutions.Common.Domain.Repositories.Application;
using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.Core.Application.Services;

namespace DhubSolutions.Common.Application.Services.Application
{
    public class FinancingRepaymentService : ServiceMapper<FinancingRepayment>, IFinancingRepaymentService
    {

        public FinancingRepaymentService(IUnitOfWork unitOfWork, ITypeAdapter typeAdapter, IFinancingRepaymentRepository repository)
            : base(unitOfWork, typeAdapter, repository)
        {
        }
    }
}
