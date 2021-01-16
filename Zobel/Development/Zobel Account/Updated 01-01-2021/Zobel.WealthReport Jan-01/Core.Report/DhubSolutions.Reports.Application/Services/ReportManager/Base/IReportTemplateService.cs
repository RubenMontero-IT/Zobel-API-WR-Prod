using DhubSolutions.Reports.Application.Dtos.ReportManager;
using DhubSolutions.Reports.Domain.Entities.ReportManager;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DhubSolutions.Reports.Application.Services.ReportManager.Base
{
    public interface IReportTemplateService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="creationReportTemplateModel"></param>
        /// <returns></returns>
        Dto CreateTemplate<Dto>(ReportTemplateCreationDto creationReportTemplateModel)
            where Dto : class;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportTemplateUpdate"></param>
        /// <exception cref="InvalidOperationException"></exception>
        void UpdateReportTemplate(ReportTemplateUpdateDto reportTemplateUpdate);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="templateId"></param>
        /// <param name="asNotracking"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Dto GetTemplateById<Dto>(string templateId, bool asNotracking = true, params Expression<Func<ReportTemplate, object>>[] includes)
            where Dto : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="asNotracking"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        IEnumerable<Dto> GetAll<Dto>(Expression<Func<ReportTemplate, bool>> predicate = null, bool asNotracking = true, params Expression<Func<ReportTemplate, object>>[] includes)
            where Dto : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        IEnumerable<Dto> GetAllReportTemplatesAvailable<Dto>(CollectionReportTemplateDto collection) where Dto : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        IEnumerable<Dto> GetAllReportTemplateActivePeriod<Dto>(CollectionOrganizationDto collection) where Dto : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        IEnumerable<Dto> GetAllCreationPeriods<Dto>(ReportTemplateOrganizationDto dto) where Dto : class;


    }
}
