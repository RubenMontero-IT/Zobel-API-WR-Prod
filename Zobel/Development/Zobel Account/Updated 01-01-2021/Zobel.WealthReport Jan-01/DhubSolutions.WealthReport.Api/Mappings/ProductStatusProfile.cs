using AutoMapper;
using DhubSolutions.WealthReport.Api.ViewModels;
using DhubSolutions.WealthReport.Domain.Entities;

namespace DhubSolutions.WealthReport.Api.Mappings
{
    public class ProductStatusProfile : Profile
    {
        public ProductStatusProfile()
        {
            CreateMap<ProductStatus, MetadataVM>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.ProductStatusName));
        }
    }
}
