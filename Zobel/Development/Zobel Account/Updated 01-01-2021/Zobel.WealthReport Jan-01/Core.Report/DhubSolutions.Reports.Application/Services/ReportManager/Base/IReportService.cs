using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Domain.Services;
using DhubSolutions.Reports.Application.Dtos.ReportManager;
using DhubSolutions.Reports.Domain.Entities.ReportManager;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DhubSolutions.Reports.Application.Services.ReportManager.Base
{
    public interface IReportService : IServiceMapper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganizationRole"></param>
        /// <returns></returns>
        string GetSettings(OrganizationRole OrganizationRole);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newName"></param>
        /// <param name="report"></param>
        /// <returns></returns>
        string CloneReport(string newName, Report report);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="reportCreationDto"></param>
        /// <returns></returns>
        TViewModel CreateReport<TViewModel>(ReportCreationDto reportCreationDto) where TViewModel : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="filter"></param>
        /// <param name="asNoTracking"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        IEnumerable<TViewModel> GetAllReport<TViewModel>(
            Expression<Func<Report, bool>> filter = null,
            bool asNoTracking = false,
            params Expression<Func<Report, object>>[] includes) where TViewModel : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="reportId"></param>
        /// <param name="asNoTracking"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        TViewModel GetReportById<TViewModel>(string reportId,
            bool asNoTracking = false,
            params Expression<Func<Report, object>>[] includes) where TViewModel : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="detailReportDto"></param>
        /// <returns></returns>
        Dto GetDetailReport<Dto>(DetailReportDto detailReportDto) where Dto : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="fullReport"></param>
        /// <returns></returns>
        Dto GetFullReport<Dto>(FullReportDto fullReport) where Dto : class;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportUpdated"></param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        void UpdateReportContent(ReportUpdatedDto reportUpdated);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <exception cref="InvalidOperationException"></exception>
        void UpdateReportData(ReportDataUpdatedDto dto);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportTemplateSettingsDto"></param>
        /// <exception cref="InvalidOperationException"></exception>
        void AddOrUpdateTemplateSettings(ReportTemplateSettingsDto reportTemplateSettingsDto);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportTemplateSettingsDto"></param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns></returns>
        JObject GetTemplateSettings(ReportTemplateSettingsDto reportTemplateSettingsDto);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportId"></param>
        /// <exception cref="InvalidOperationException"></exception>
        void RemoveReport(string reportId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportIds"></param>
        /// <exception cref="InvalidOperationException"></exception>
        void RemoveReportRange(params string[] reportIds);
    }
}
