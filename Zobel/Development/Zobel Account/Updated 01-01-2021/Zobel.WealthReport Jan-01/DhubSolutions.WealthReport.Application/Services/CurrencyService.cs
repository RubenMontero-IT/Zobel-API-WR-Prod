using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.WealthReport.Application.Services.Base;
using DhubSolutions.WealthReport.Domain.Entities;
using DhubSolutions.WealthReport.Domain.Repositories;

namespace DhubSolutions.WealthReport.Application.Services
{
    public class CurrencyService : WealthReportService<Currency>, ICurrencyService
    {
        public CurrencyService(ITypeAdapter typeAdapter, IWealthReportRepository<Currency> repository)
            : base(typeAdapter, repository)
        { }
    }
}
