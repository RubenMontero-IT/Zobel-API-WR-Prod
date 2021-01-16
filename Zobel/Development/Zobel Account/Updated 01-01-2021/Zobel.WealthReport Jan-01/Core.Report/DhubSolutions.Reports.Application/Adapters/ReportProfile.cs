using AutoMapper;
using DhubSolutions.Reports.Application.Dtos.ReportManager;
using DhubSolutions.Reports.Application.ViewModels.ReportManager;
using DhubSolutions.Reports.Domain.Entities.ReportManager;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace DhubSolutions.Reports.Application.Adapters
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<Report, ShortReportVM>()
                .ForMember(rvm => rvm.CreatedBy, opt => opt.MapFrom(r => r.CreatedBy.UserName))
                .ForMember(rvm => rvm.Organization, opt => opt.Ignore())

                .AfterMap((report, reportShortVM) =>
                {
                    reportShortVM.Organization = $"{JObject.Parse(report.Metadata)["organization"]}";
                });

            CreateMap<Report, ReportVM>()
               .ForMember(rVm => rVm.Data, opt => opt.Ignore())
               .ForMember(rVm => rVm.Metadata, opt => opt.Ignore())
               .ForMember(rvm => rvm.CreatedBy, opt => opt.MapFrom(r => r.CreatedBy.UserName))
               .ForMember(rvm => rvm.Permissions, opt => opt.MapFrom(r => r.Template.ReportTemplatePermissions
                                                                                           .Select(p => p.Permission.PermissionCode)))

                .AfterMap((report, reportVM) =>
                {
                    reportVM.Data = JObject.Parse(report.Data);
                    reportVM.Metadata = JObject.Parse(report.Metadata);
                });

            CreateMap<ReportUpdateVM, ReportUpdatedDto>();


        }


    }
}
