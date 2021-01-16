using AutoMapper;
using DhubSolutions.WealthReport.Api.ViewModels;
using DhubSolutions.WealthReport.Domain.Entities;

namespace DhubSolutions.WealthReport.Api.Mappings
{
    public class CurrencyProfile : Profile
    {
        public CurrencyProfile()
        {
            CreateMap<Currency, MetadataVM>()
                //                .ForMember(dest => dest.CurrencyId, opt => opt.MapFrom(src => src.Id));
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.CurrencyName));
        }
    }
}
