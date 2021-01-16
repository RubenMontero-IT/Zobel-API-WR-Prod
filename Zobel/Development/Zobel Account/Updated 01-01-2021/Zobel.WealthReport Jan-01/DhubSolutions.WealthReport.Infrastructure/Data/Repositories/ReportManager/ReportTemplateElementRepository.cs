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
    public class ReportTemplateElementRepository : Repository<ReportTemplateElement>, IReportTemplateElementRepository
    {
        public ReportTemplateElementRepository(ProjectManagementDbContext context, IUnitOfWork unitOfWork)
            : base(context, unitOfWork)
        { }

        public override IEnumerable<ReportTemplateElement> GetAll(Expression<Func<ReportTemplateElement, bool>> filter = null, bool noTracking = true, params Expression<Func<ReportTemplateElement, object>>[] includes)
        {
            var reportTemplateElements = base.GetAll(filter, noTracking, includes)
                                             .ToList();

            var repository = UnitOfWork.GetRepository<ReportTemplateElementPermissionRepository>();

            reportTemplateElements.ForEach(reportTemplateElement =>
            {
                reportTemplateElement.ReportTemplateElementPermissions = repository.
                        GetAll(permission => permission.ReportTemplateElementId == reportTemplateElement.Id,
                               noTracking,
                               p => p.Permission)
                        .ToList();
            });
            return reportTemplateElements;
        }
    }
}
