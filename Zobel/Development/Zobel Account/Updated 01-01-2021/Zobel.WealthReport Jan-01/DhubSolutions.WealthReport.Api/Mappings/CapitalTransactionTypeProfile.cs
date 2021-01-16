using AutoMapper;
using DhubSolutions.WealthReport.Api.ViewModels;
using DhubSolutions.WealthReport.Domain.Entities;

namespace DhubSolutions.WealthReport.Api.Mappings
{
    public class CapitalTransactionTypeProfile : Profile
    {
        public CapitalTransactionTypeProfile()
        {
            CreateMap<CapitalTransactionType, MetadataVM>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.CapitalTransactionTypeName));
        }
    }
}
