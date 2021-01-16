using AutoMapper;
using DhubSolutions.Reports.Application.Dtos.ReportManager;
using DhubSolutions.Reports.Application.ViewModels.ReportManager;
using DhubSolutions.Reports.Domain.Entities.ReportManager;
using Newtonsoft.Json.Linq;
using System;

namespace DhubSolutions.Reports.Application.Adapters
{
    public class ReportTemplateElementProfile : Profile
    {
        public ReportTemplateElementProfile()
        {
            CreateMap<ReportTemplateElementVM, ReportTemplateElementDto>()
                .ForMember(rte => rte.Id, opt => opt.Ignore())

                .AfterMap((vm, dto) =>
                {
                    dto.Id = $"{Guid.NewGuid()}";
                });

            CreateMap<ReportTemplateElementDto, ReportTemplateElement>();

            //Mapping to create a ReportTemplateElementVM entity (postTemplate) 
            CreateMap<ReportTemplateElement, ReportTemplateElementVM>()
               .ForMember(vm => vm.Config, opt =>
                {
                    opt.PreCondition(rte => string.IsNullOrEmpty(rte.Config));
                    opt.Ignore();

                })
               .ForMember(vm => vm.Children, opt => opt.MapFrom(rte => rte.Children))

                .AfterMap((src, target) =>
                {
                    if (!string.IsNullOrEmpty(src.Config))
                        target.Config = JObject.Parse($"{src.Config}");
                });

            //Mapping between an exiting entity and dto 
            CreateMap<ReportTemplateElement, ReportTemplateElementDto>()
                .ForMember(dto => dto.Config, opt => opt.Ignore())

                .AfterMap((src, dto) =>
                {
                    if (!string.IsNullOrEmpty(src.Config))
                        dto.Config = JObject.Parse($"{src.Config}");
                });
        }
    }
}
