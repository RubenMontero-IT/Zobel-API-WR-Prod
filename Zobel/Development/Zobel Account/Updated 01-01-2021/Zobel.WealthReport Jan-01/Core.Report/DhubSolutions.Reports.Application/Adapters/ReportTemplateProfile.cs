using AutoMapper;
using DhubSolutions.Reports.Application.Dtos.ReportManager;
using DhubSolutions.Reports.Application.ViewModels.ReportManager;
using DhubSolutions.Reports.Domain.Entities.ReportManager;
using Newtonsoft.Json.Linq;
using System;

namespace DhubSolutions.Reports.Application.Adapters
{
    public class ReportTemplateProfile : Profile
    {
        public ReportTemplateProfile()
        {
            //Mapping to create a ReportTemplate dto
            CreateMap<ReportTemplateVM, ReportTemplateDto>()
                .ForMember(dto => dto.Id, opt => opt.Ignore())

                .AfterMap((vm, dto) =>
                {
                    dto.Id = $"{Guid.NewGuid()}";
                });

            CreateMap<ReportTemplateDto, ReportTemplate>();

            //Mapping to create a ReportTemplateVM entity (postTemplate) 
            CreateMap<ReportTemplate, ReportTemplateVM>()
                .ForMember(vm => vm.Data, opt => opt.Ignore())
                .ForMember(vm => vm.Metadata, opt => opt.Ignore())
                .ForMember(vm => vm.Content, opt => opt.MapFrom(e => e.Content))

                .AfterMap((reportTemplate, reportTemplateVM) =>
                {
                    reportTemplateVM.Data = JObject.Parse(reportTemplate.Data);
                    reportTemplateVM.Metadata = JObject.Parse(reportTemplate.Metadata);
                });


            //Mapping between an exiting entity and dto 
            CreateMap<ReportTemplate, ReportTemplateDto>()
                .ForMember(dto => dto.Data, opt => opt.Ignore())
                .ForMember(dto => dto.Metadata, opt => opt.Ignore())

                .AfterMap((reportTemplate, dto) =>
                {
                    dto.Data = JObject.Parse(reportTemplate.Data);
                    dto.Metadata = JObject.Parse(reportTemplate.Metadata);

                });

            CreateMap<ReportTemplate, ShortReportTemplateVM>();
        }
    }
}
