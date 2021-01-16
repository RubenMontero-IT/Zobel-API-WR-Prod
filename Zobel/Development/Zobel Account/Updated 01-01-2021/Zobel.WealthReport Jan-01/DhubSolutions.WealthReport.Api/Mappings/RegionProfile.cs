using AutoMapper;
using DhubSolutions.WealthReport.Api.ViewModels;
using DhubSolutions.WealthReport.Domain.Entities;

namespace DhubSolutions.WealthReport.Api.Mappings
{
    public class RegionProfile : Profile
    {
        public RegionProfile()
        {
            CreateMap<Region, MetadataVM>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.RegionName));

        }
    }
}
