using AutoMapper;
using DhubSolutions.WealthReport.Api.ViewModels;
using DhubSolutions.WealthReport.Domain.Entities;

namespace DhubSolutions.WealthReport.Api.Mappings
{
    public class PortfolioCategoryProfile : Profile
    {
        public PortfolioCategoryProfile()
        {
            CreateMap<PortfolioCategory, MetadataVM>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.PortfolioCategoryName));
        }
    }
}
