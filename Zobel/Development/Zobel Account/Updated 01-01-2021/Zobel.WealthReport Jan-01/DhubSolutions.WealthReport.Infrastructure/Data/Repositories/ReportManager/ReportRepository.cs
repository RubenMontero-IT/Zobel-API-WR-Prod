using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using DhubSolutions.WealthReport.Infrastructure.Data.Context;
using DhubSolutions.Core.Infrastructure.Data.Repositories;
using DhubSolutions.Reports.Domain.Entities.ReportManager;
using DhubSolutions.Reports.Domain.Repositories.ReportManager;
using DhubSolutions.Core.Infrastructure.Data;
using DhubSolutions.Core.Domain.Entity;
using DhubSolutions.Common.Domain.Entities.Admin;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Repositories.ReportManager
{
    public class ReportRepository : Repository<Report>, IReportRepository
    {
        private readonly IEntityFrameworkUnitOfWork _queryableUnitOfWork;

        public ReportRepository(ProjectManagementDbContext context, IEntityFrameworkUnitOfWork unitOfWork)
            : base(context, unitOfWork)
        {
            _queryableUnitOfWork = unitOfWork;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="report"></param>
        public override void Add(Report report)
        {
            if (string.IsNullOrEmpty(report.Id) || string.IsNullOrWhiteSpace(report.Id))
                //report.Id = Guid.NewGuid().ToString();
                throw new ArgumentNullException(nameof(report.Id));

            //Patch because he tries to insert this navigation prop as a new entity
            //therefore this results in a duplicate key insertion error (ALEX: Don't touch this, ask ernesto first, Atte: JesLoPov)
            report.Template = null;

            foreach (var element in report.GetAllContent())
            {
                element.ReportTemplateElement = null;
                element.CreatedById = report.CreatedById;
                element.LastModifiedById = report.CreatedById;
            }
            _dbContext.Set<Report>().Add(report);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="asNoTracking"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public override Report Get(
            Expression<Func<Report, bool>> predicate,
            bool asNoTracking = false,
            params Expression<Func<Report, object>>[] includes)
        {
            Report report = base.Get(predicate, asNoTracking, includes);

            if (report is null)
                return default;

            if (report.Template != null)
                report.Template = UnitOfWork.GetRepository<ReportTemplateRepository>()
                     .Get(template => template.Id == report.TemplateId, asNoTracking);

            if (report.Content.Count() != 0)
                report.Content = UnitOfWork.GetRepository<ReportElementRepository>()
                    .GetAll(e => e.ReportId == report.Id, asNoTracking,
                            e => e.ReportTemplateElement,
                            e => e.Children)
                    .Where(e => e.ContainerId == null)
                    .ToList();

            //report.Template.ReportTemplatePermissions = UnitOfWork.GetRepository<ReportTemplatePermissionRepository>()
            //    .GetAll(p => p.ReportTemplateId == report.TemplateId, asNoTracking, p => p.Permission)
            //    .ToList();

            return report;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public virtual string CloneReport(Report report)
        {
            var content = report.Content;
            var reportPermissions = report.ReportPermissions;

            CloneEntity(report);

            report.Name = $"{report.Name}_copy";
            report.CreationDate = DateTime.Now;
            report.LastModified = DateTime.Now;

            CloneEntities(reportPermissions);

            #region CloneContent       
            foreach (var element in content)
            {
                CloneReportElement(element);
            }
            #endregion

            return report.Id;

            ReportElement CloneReportElement(ReportElement reportElement)
            {
                foreach (var element in reportElement.Children)
                {
                    CloneReportElement(element);
                }
                CloneEntity(reportElement);

                reportElement.CreationDate = DateTime.Now;
                reportElement.LastModified = DateTime.Now;

                CloneEntities(reportElement.ReportElementPermissions);

                return reportElement;
            }

            T CloneEntity<T>(T entity) where T : IEntity
            {
                _dbContext.Entry(entity).State = EntityState.Detached;

                if (entity.GetType().GetProperty("Id") != null)
                {
                    entity.GetType().GetProperty("Id").SetValue(entity, $"{Guid.NewGuid()}");
                }

                _dbContext.Entry(entity).State = EntityState.Added;
                return entity;
            }

            IEnumerable<T> CloneEntities<T>(IEnumerable<T> entities) where T : IEntity
            {
                foreach (var entity in entities)
                {
                    CloneEntity(entity);
                }
                return entities;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizationRole"></param>
        /// <returns></returns>
        public virtual string GetSettings(OrganizationRole organizationRole)
        {
            var parameters = new { UserRoleOrgID = organizationRole.Id };
            var query = @"SELECT asro.[Value]	  
                              FROM userm.UserRoleOrg uro LEFT JOIN  app.AppSettingRoleVOrg  asro ON uro.RVID = asro.RVID  AND  uro.ORGID = asro.ORGID
                                                         LEFT JOIN  app.AppSetting          appSetting ON appSetting.SettingID = asro.SettingID
							                             LEFT JOIN  app.Apps                apps ON apps.APPID = appSetting.APPID
                              WHERE  UserRoleOrgID = @UserRoleOrgID
                                 AND AppCode = 'reportManager'";

            IEnumerable<string> result = _queryableUnitOfWork.ExecuteQuery<string>(query, parameters, commandTimeout: 600);
            return result.SingleOrDefault();
        }
    }
}
