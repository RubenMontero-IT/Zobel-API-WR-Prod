using AutoMapper;
using DhubSolutions.WealthReport.Api.ViewModels;
using DhubSolutions.WealthReport.Domain.Entities;

namespace DhubSolutions.WealthReport.Api.Mappings
{
    public class SectionProfile : Profile
    {
        public SectionProfile()
        {
            CreateMap<Section, MetadataVM>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.SectionName));
        }
    }
}
