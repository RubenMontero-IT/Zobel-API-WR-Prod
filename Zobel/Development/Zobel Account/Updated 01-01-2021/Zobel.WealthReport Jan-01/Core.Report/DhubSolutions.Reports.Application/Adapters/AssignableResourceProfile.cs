using AutoMapper;
using DhubSolutions.Common.Domain.Entities.Base;
using DhubSolutions.Reports.Application.ViewModels.ReportManager;
using DhubSolutions.Reports.Domain.Entities.ReportManager;

namespace DhubSolutions.Reports.Application.Adapters
{
    public class AssignableResourceProfile : Profile
    {
        public AssignableResourceProfile()
        {
            CreateMap<IAssignableResource, PermissionVM>()
                .ForMember(target => target.OrganizationRoleId, opt => opt.MapFrom(src => src.OrganizationRoleId))
                .ForMember(target => target.Type, opt => opt.MapFrom(src => src.Permission.PermissionCode));

            CreateMap<ReportTemplate, ReportTemplatePermissionVM>()
                .ForMember(target => target.Permissions, opt => opt.MapFrom(src => src.ReportTemplatePermissions))
                .ForMember(target => target.ReportTemplateElementPermissions, opt => opt.MapFrom(src => src.GetAllContent()));

            CreateMap<ReportTemplateElement, ReportTemplateElementPermissionVM>()
                .ForMember(target => target.ReportTemplateElementId, opt => opt.MapFrom(src => src.Id))
                .ForMember(target => target.Permissions, opt => opt.MapFrom(src => src.ReportTemplateElementPermissions));
        }
    }
}
