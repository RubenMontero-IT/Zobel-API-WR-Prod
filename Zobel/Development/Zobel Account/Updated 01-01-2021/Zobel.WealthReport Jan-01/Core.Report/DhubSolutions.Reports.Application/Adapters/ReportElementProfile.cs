using AutoMapper;
using DhubSolutions.Reports.Application.Dtos.ReportManager;
using DhubSolutions.Reports.Application.ViewModels.ReportManager;
using DhubSolutions.Reports.Domain.Entities.ReportManager;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace DhubSolutions.Reports.Application.Adapters
{
    public class ReportElementProfile : Profile
    {
        public ReportElementProfile()
        {
            CreateMap<ReportElement, ReportElementVM>()
                .ForMember(dest => dest.Config, opt =>
                {
                    opt.PreCondition(src => !string.IsNullOrEmpty(src.Config));
                    opt.MapFrom(src => JObject.Parse(src.Config));
                });

            CreateMap<ReportElement, ReportElementPermissionVM>()
               .IncludeBase<ReportElement, ReportElementVM>()
               .ForMember(reVm => reVm.Permissions, opt => opt.MapFrom(r => r.ReportTemplateElement.ReportTemplateElementPermissions
                                                                                                   .Select(p => p.Permission.PermissionCode)));

            CreateMap<ReportElementVM, ReportElementDto>()

                .AfterMap((src, dest) =>
                {
                    //Add new ReportElement
                    if (dest.Id == null)
                        dest.Id = $"{Guid.NewGuid()}";
                });

            CreateMap<ReportElementDto, ReportElement>()
                .ForMember(dest => dest.Children, opt => opt.Ignore())
                .ForMember(dest => dest.Config, opt =>
                 {
                     opt.PreCondition(src => src.Config != null);
                     opt.MapFrom(src => src.Config);
                 });
        }
    }
}
