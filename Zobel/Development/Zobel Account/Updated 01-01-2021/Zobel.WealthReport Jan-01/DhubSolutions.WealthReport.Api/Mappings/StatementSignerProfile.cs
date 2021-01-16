using AutoMapper;
using DhubSolutions.WealthReport.Api.ViewModels;
using DhubSolutions.WealthReport.Domain.Entities;

namespace DhubSolutions.WealthReport.Api.Mappings
{
    public class StatementSignerProfile : Profile
    {
        public StatementSignerProfile()
        {
            CreateMap<StatementSigner, StatementSignerVM>()
                .ConvertUsing<StatementSignerConverter>();
        }


    }
}
