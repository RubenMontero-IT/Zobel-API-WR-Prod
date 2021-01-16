using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Core.Infrastructure.Data.Repositories;
using DhubSolutions.Reports.Domain.Entities.ReportManager;
using DhubSolutions.Reports.Domain.Repositories.ReportManager;
using DhubSolutions.WealthReport.Infrastructure.Data.Context;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Repositories.ReportManager
{
    public class ReportTemplateRepository : Repository<ReportTemplate>, IReportTemplateRepository
    {

        public ReportTemplateRepository(ProjectManagementDbContext context, IUnitOfWork unitOfWork)
            : base(context, unitOfWork)
        { }
        public override void Add(ReportTemplate template)
        {
            template.CreationDate = DateTime.Now;
            template.LastModified = DateTime.Now;

            foreach (ReportTemplateElement templateElement in template.GetAllContent())
            {
                templateElement.CreatedById = template.CreatedById;
                templateElement.LastModifiedById = template.CreatedById;
                templateElement.ReportTemplateId = template.Id;
                templateElement.CreationDate = DateTime.Now;
                templateElement.LastModified = DateTime.Now;

                if (string.IsNullOrEmpty(templateElement.Code))
                    templateElement.Code = $"{Guid.NewGuid()}";
            }

            _dbContext.Set<ReportTemplate>().Add(template);
        }

        public override ReportTemplate Get(
            Expression<Func<ReportTemplate, bool>> predicate,
            bool asNoTracking = true,
            params Expression<Func<ReportTemplate, object>>[] includes)
        {
            ReportTemplate reportTemplate = base.Get(predicate, asNoTracking, includes);

            if (reportTemplate is null)
                return default;

            reportTemplate.Content = UnitOfWork.GetRepository<ReportTemplateElementRepository>()
                .GetAll(e => e.ReportTemplateId == reportTemplate.Id,
                        asNoTracking,
                        e => e.Children)
                .Where(e => e.ContainerId == null)
                .ToList();

            reportTemplate.ReportTemplatePermissions = UnitOfWork.GetRepository<ReportTemplatePermissionRepository>()
                .GetAll(permission => permission.ReportTemplateId == reportTemplate.Id,
                        asNoTracking,
                        p => p.Permission)
                .ToList();

            return reportTemplate;
        }


    }
}
