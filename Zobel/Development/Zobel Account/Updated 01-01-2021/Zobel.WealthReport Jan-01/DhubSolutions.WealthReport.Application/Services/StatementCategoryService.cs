using DhubSolutions.Core.Application.Services;
using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.Core.Domain.Data;
using DhubSolutions.WealthReport.Application.Services.Base;
using DhubSolutions.WealthReport.Domain.Entities;
using DhubSolutions.WealthReport.Domain.Repositories;

namespace DhubSolutions.WealthReport.Application.Services
{
    public class StatementCategoryService : ServiceMapper<StatementCategory>, IStatementCategoryService
    {
        public StatementCategoryService(IUnitOfWork unitOfWork, ITypeAdapter typeAdapter, IStatementCategoryRepository repository)
            : base(unitOfWork, typeAdapter, repository)
        {
        }
    }
}
