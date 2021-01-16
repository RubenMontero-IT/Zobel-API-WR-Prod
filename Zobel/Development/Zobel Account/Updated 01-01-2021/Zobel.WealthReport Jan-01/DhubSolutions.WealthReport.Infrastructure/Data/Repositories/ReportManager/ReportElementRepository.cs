using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Core.Infrastructure.Data.Repositories;
using DhubSolutions.Reports.Domain.Entities.ReportManager;
using DhubSolutions.Reports.Domain.Repositories.ReportManager;
using DhubSolutions.WealthReport.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Repositories.ReportManager
{
    public class ReportElementRepository : Repository<ReportElement>, IReportElementRepository
    {
        public ReportElementRepository(ProjectManagementDbContext context, IUnitOfWork unitOfWork)
            : base(context, unitOfWork)
        { }

        public override IEnumerable<ReportElement> GetAll(
            Expression<Func<ReportElement, bool>> predicate,
            bool asNoTracking = true,
            params Expression<Func<ReportElement, object>>[] includes)
        {
            var reportElements = base.GetAll(predicate, asNoTracking, includes)
                .ToList();

            var repository = UnitOfWork.GetRepository<ReportTemplateElementPermissionRepository>();

            reportElements.ForEach(reportElement =>
            {
                if (reportElement.ReportTemplateElement != null)
                    reportElement.ReportTemplateElement.ReportTemplateElementPermissions = repository
                        .GetAll(permission => permission.ReportTemplateElementId == reportElement.ReportTemplateElementId,
                             asNoTracking,
                             includes: p => p.Permission)
                        .ToList();
            });

            return reportElements;
        }
    }
}
