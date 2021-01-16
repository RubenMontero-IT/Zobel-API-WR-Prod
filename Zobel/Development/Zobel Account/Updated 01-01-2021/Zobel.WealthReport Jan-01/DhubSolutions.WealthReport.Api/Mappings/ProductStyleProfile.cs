using AutoMapper;
using DhubSolutions.WealthReport.Api.ViewModels;
using DhubSolutions.WealthReport.Domain.Entities;

namespace DhubSolutions.WealthReport.Api.Mappings
{
    public class ProductStyleProfile : Profile
    {
        public ProductStyleProfile()
        {
            CreateMap<ProductStyle, MetadataVM>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.ProductStyleName));
        }
    }
}
