using AutoMapper;
using DhubSolutions.WealthReport.Api.ViewModels;
using DhubSolutions.WealthReport.Domain.Entities;

namespace DhubSolutions.WealthReport.Api.Mappings
{
    public class TransactionTypeProfile : Profile
    {
        public TransactionTypeProfile()
        {
            CreateMap<TransactionType, MetadataVM>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.TransactionTypeName));
        }
    }
}
