using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.WealthReport.Application.Services.Base;
using DhubSolutions.WealthReport.Domain.Entities;
using DhubSolutions.WealthReport.Domain.Repositories;

namespace DhubSolutions.WealthReport.Application.Services
{
    public class AccountService : WealthReportService<Account>, IAccountService
    {
        public AccountService(ITypeAdapter typeAdapter, IWealthReportRepository<Account> reportRepository)
           : base(typeAdapter, reportRepository)
        {
        }
    }
}


