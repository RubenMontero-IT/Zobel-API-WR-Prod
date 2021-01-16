using AutoMapper;
using DhubSolutions.WealthReport.Api.ViewModels;
using DhubSolutions.WealthReport.Domain.Entities;

namespace DhubSolutions.WealthReport.Api.Mappings
{
    public class LiquidityProfile : Profile
    {
        public LiquidityProfile()
        {
            CreateMap<Liquidity, MetadataVM>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.LiquidityValue));
        }
    }
}
