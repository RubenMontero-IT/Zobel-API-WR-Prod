using AutoMapper;
using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Reports.Application.ViewModels.ReportManager;

namespace DhubSolutions.Reports.Application.Adapters
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<Organization, OrganizationVM>()
                .ForMember(dto => dto.OrganizationId, opt => opt.MapFrom(org => org.LucanetId));
        }
    }
}
