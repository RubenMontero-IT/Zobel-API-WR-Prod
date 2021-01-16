using AutoMapper;
using DhubSolutions.WealthReport.Api.ViewModels;
using DhubSolutions.WealthReport.Domain.Entities;

namespace DhubSolutions.WealthReport.Api.Mappings
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<Organization_WR, OrganizationGeneralInfoVM>();
        }
    }
}
