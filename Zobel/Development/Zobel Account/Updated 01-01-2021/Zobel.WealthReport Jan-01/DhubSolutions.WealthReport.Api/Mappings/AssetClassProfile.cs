using AutoMapper;
using DhubSolutions.WealthReport.Api.ViewModels;
using DhubSolutions.WealthReport.Domain.Entities;

namespace DhubSolutions.WealthReport.Api.Mappings
{
    public class AssetClassProfile : Profile
    {
        public AssetClassProfile()
        {
            CreateMap<AssetClass, MetadataVM>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.AssetClassName));
        }
    }
}
