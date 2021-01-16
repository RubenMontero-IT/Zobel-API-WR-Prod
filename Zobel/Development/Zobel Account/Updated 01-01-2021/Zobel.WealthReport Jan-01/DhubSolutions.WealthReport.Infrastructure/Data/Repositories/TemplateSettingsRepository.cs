using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Core.Infrastructure.Data.Repositories;
using DhubSolutions.Reports.Domain.Entities.ReportManager;
using DhubSolutions.Reports.Domain.Repositories.ReportManager;
using DhubSolutions.WealthReport.Infrastructure.Data.Context;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Repositories
{
    public class TemplateSettingsRepository : Repository<TemplateSettings>, ITemplateSettingsRepository
    {
        public TemplateSettingsRepository(ProjectManagementDbContext dbContext, IUnitOfWork unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }
    }
}
