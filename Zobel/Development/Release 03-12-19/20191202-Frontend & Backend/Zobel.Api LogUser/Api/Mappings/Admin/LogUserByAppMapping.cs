using Api.ViewModels.DbInvestementVMs;
using AutoMapper;
using Domain.Entities.DbInvestmentEntities;

namespace Api.Mappings.DBInvestment
{
    public class FileTypeMappings : Profile
    {
        public FileTypeMappings()
        {
            CreateMap<FileType, FileTypeViewModel>();
            CreateMap<FileTypeViewModel, FileType>();
        }
    }
}
