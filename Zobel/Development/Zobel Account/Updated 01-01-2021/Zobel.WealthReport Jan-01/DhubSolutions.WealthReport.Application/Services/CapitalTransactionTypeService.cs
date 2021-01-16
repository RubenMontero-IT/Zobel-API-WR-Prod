using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.WealthReport.Application.Services.Base;
using DhubSolutions.WealthReport.Domain.Entities;
using DhubSolutions.WealthReport.Domain.Repositories;

namespace DhubSolutions.WealthReport.Application.Services
{
    public class CapitalTransactionTypeService : WealthReportService<CapitalTransactionType>, ICapitalTransactionTypeService
    {
        public CapitalTransactionTypeService(ITypeAdapter typeAdapter, IWealthReportRepository<CapitalTransactionType> reportRepository)
           : base(typeAdapter, reportRepository)
        {
        }
    }
}
