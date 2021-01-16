using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.WealthReport.Application.Services.Base;
using DhubSolutions.WealthReport.Domain.Entities;
using DhubSolutions.WealthReport.Domain.Repositories;

namespace DhubSolutions.WealthReport.Application.Services
{
    public class TransactionTypeService : WealthReportService<TransactionType>, ITransactionTypeService
    {
        public TransactionTypeService(ITypeAdapter typeAdapter, IWealthReportRepository<TransactionType> reportRepository)
           : base(typeAdapter, reportRepository)
        {
        }
    }
}
