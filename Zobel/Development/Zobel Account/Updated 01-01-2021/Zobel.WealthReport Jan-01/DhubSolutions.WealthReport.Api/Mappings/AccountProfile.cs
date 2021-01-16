using AutoMapper;
using DhubSolutions.WealthReport.Api.ViewModels;
using DhubSolutions.WealthReport.Domain.Entities;

namespace DhubSolutions.WealthReport.Api.Mappings
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, MetadataVM>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.AccountName));
        }
    }
}
