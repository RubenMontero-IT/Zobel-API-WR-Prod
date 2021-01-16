using DhubSolutions.Common.Domain.Repositories.Admin;
using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Reports.Application.Dtos.ReportManager;
using DhubSolutions.Reports.Application.Services.ReportManager.Base;
using DhubSolutions.Reports.Domain.Entities.ReportManager;
using DhubSolutions.Reports.Domain.Entities.ReportManager.Extensions;
using DhubSolutions.Reports.Domain.Repositories.ReportManager;
using DhubSolutions.Reports.Domain.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DhubSolutions.Reports.Application.Services.ReportManager
{
    public class ReportTemplateService : IReportTemplateService
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IOrganizationRoleRepository _organizationRoleRepository;
        private readonly IReportTemplateRepository _reportTemplateRepository;
        private readonly IReportTemplateElementRepository _reportTemplateElementRepository;
        private readonly IReportTemplateManagerService _reportTemplateManagerService;

        public ReportTemplateService(
            IUnitOfWork unitOfWork,
            ITypeAdapter typeAdapter,
            IOrganizationRepository organizationRepository,
            IOrganizationRoleRepository organizationRoleRepository,
            IUserRoleOrgRepository userRoleOrgRepository,
            IReportTemplateRepository repository,
            IReportTemplateElementRepository reportTemplateElementRepository,
            IReportTemplateManagerService reportTemplateManagerService,
            ITemplateSettingsRepository templateSettingsRepository
            /*IWealthReportRepository<TemplateSettings> templateSettingsRepository*/)
        {
            UnitOfWork = unitOfWork;
            TypeAdapter = typeAdapter;
            _organizationRepository = organizationRepository;
            _organizationRoleRepository = organizationRoleRepository;
            _reportTemplateRepository = repository;
            _reportTemplateElementRepository = reportTemplateElementRepository;
            _reportTemplateManagerService = reportTemplateManagerService;
        }

        public IUnitOfWork UnitOfWork { get; }
        public ITypeAdapter TypeAdapter { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="creationReportTemplateModel"></param>
        /// <returns></returns>
        public Dto CreateTemplate<Dto>(ReportTemplateCreationDto creationReportTemplateModel)
            where Dto : class
        {
            ReportTemplateDto reportTemplateDto = creationReportTemplateModel.ReportTemplate;

            ReportTemplate reportTemplate = TypeAdapter.Adapt<ReportTemplateDto, ReportTemplate>(reportTemplateDto);

            reportTemplate.CreatedById = creationReportTemplateModel.UserId;

            _reportTemplateRepository.Add(reportTemplate);

            return TypeAdapter.Adapt<Dto>(reportTemplate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportTemplateUpdate"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void UpdateReportTemplate(ReportTemplateUpdateDto reportTemplateUpdate)
        {
            string templateId = reportTemplateUpdate.TemplateId;

            ReportTemplate reportTemplate = _reportTemplateRepository.Get(
                                               predicate: t => t.Id == templateId,
                                               asNoTracking: true);
            if (reportTemplate == null)
                throw new InvalidOperationException($"The template with Id: {templateId} was not found");

            ReportTemplateDto reportTemplateDto = reportTemplateUpdate.ReportTemplate;

            reportTemplate.Name = reportTemplateDto.Name;
            reportTemplate.Description = reportTemplateDto.Description;
            reportTemplate.Data = $"{reportTemplateDto.Data}";
            reportTemplate.Metadata = $"{reportTemplateDto.Metadata}";
            reportTemplate.LastModified = DateTime.Now;

            string userId = reportTemplateUpdate.UserId;

            AddOrUpdateReportTemplateElement(reportTemplateDto.Content, (container: null, children: reportTemplate.Content));

            _reportTemplateRepository.Update(reportTemplate);

            #region  local method

            void AddOrUpdateReportTemplateElement(
                IEnumerable<ReportTemplateElementDto> elementDtos,
                (string container, IEnumerable<ReportTemplateElement> children) relationShip)
            {
                var dtos = new Queue<IEnumerable<ReportTemplateElementDto>>();
                var ps = new Queue<(string containerId, IEnumerable<ReportTemplateElement> children)>();

                dtos.Enqueue(elementDtos);
                ps.Enqueue(relationShip);
               
                while (dtos.Count > 0 && ps.Count > 0)
                {
                    var newDtos = dtos.Dequeue();
                    (var containerId, var children) = ps.Dequeue();

                    foreach (var dto in newDtos)
                    {
                        var element = children.SingleOrDefault(e => string.Equals(e.Code, dto.Code, StringComparison.OrdinalIgnoreCase));
                        if (element == null)
                        {
                            element = new ReportTemplateElement
                            {
                                Name = dto.Name,
                                Description = dto.Description,
                                Type = dto.Type,
                                Code = dto.Code ?? $"{Guid.NewGuid()}",
                                Config = $"{dto.Config}",
                                ContainerId = containerId,
                                ReportTemplateId = templateId,
                                CreationDate = DateTime.Now,
                                LastModified = DateTime.Now,
                                CreatedById = userId,
                                LastModifiedById = userId,
                            };

                            //Add new ReportTemplateElement()
                            _reportTemplateElementRepository.Add(element);
                        }

                        else
                        {
                            element.Name = dto.Name;
                            element.Description = dto.Description;
                            element.Config = $"{dto.Config}";
                            element.LastModified = DateTime.Now;
                            element.LastModifiedById = userId;

                            //Update ReportTemplateElement()
                            _reportTemplateElementRepository.Update(element);
                        }

                        dtos.Enqueue(dto.Children);
                        ps.Enqueue((containerId: element.Id, children: element.Children));

                    }
                }
            }

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="templateId"></param>
        /// <param name="asNoTracking"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public Dto GetTemplateById<Dto>(string templateId, bool asNoTracking = true, params Expression<Func<ReportTemplate, object>>[] includes)
            where Dto : class
        {
            ReportTemplate reportTemplate = _reportTemplateRepository
                .Get(t => t.Id == templateId, asNoTracking, includes);

            if (reportTemplate is null)
                return default;

            return TypeAdapter.Adapt<Dto>(reportTemplate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="asNotracking"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public IEnumerable<Dto> GetAll<Dto>(Expression<Func<ReportTemplate, bool>> predicate = null, bool asNotracking = true, params Expression<Func<ReportTemplate, object>>[] includes) where Dto : class
        {
            IEnumerable<ReportTemplate> reportTemplates = _reportTemplateRepository.GetAll(predicate, asNotracking, includes);

            return TypeAdapter.Adapt<IEnumerable<Dto>>(reportTemplates);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public IEnumerable<Dto> GetAllReportTemplatesAvailable<Dto>(CollectionReportTemplateDto collection) where Dto : class
        {
            var reportTemplates = collection.ReportTemplates
                                            .Where(template => template.IsAccessible(collection.Organization));

            return TypeAdapter.Adapt<IEnumerable<Dto>>(reportTemplates);

            IEnumerable<ReportTemplate> GetAllReportTemplatesAvailable()
            {
                foreach (ReportTemplate reportTemplate in collection.ReportTemplates)
                {
                    reportTemplate.ActivePeriods = reportTemplate
                        .GetActivePeriods(collection.Organization)
                        .ToList();

                    if (reportTemplate.IsAvailable(collection.Organization))
                        yield return reportTemplate;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public IEnumerable<Dto> GetAllReportTemplateActivePeriod<Dto>(CollectionOrganizationDto collection) where Dto : class
        {
            var activePeriods = collection.ReportTemplate.GetAllActivePeriods(collection.Organizations);

            return TypeAdapter.Adapt<IEnumerable<Dto>>(activePeriods);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        public IEnumerable<Dto> GetAllCreationPeriods<Dto>(ReportTemplateOrganizationDto dto) where Dto : class
        {
            var creationPeriods = dto.ReportTemplate.GetAllCreationPeriods(dto.Organization);

            return TypeAdapter.Adapt<IEnumerable<Dto>>(creationPeriods);
        }


    }
}
