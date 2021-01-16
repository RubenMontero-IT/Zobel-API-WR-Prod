using AutoMapper;
using DhubSolutions.WealthReport.Api.ViewModels;
using DhubSolutions.WealthReport.Application.Dtos;
using DhubSolutions.WealthReport.Domain.Entities;
using System;

namespace DhubSolutions.WealthReport.Api.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductCreationVM, ProductCreationDto>()
               .ForMember(dest => dest.ProductGeneralInfo, opt => opt.MapFrom(src => src.Info))
               .ForMember(dest => dest.Transactions, opt => opt.PreCondition(src => src.Transactions != null))
               .ForMember(dest => dest.ParentPortfolio, opt => opt.PreCondition(src => src.ParentPortfolio != null))
               .ForMember(dest => dest.ProductHistoricalInfo, opt =>
               {
                   opt.PreCondition(src => src.Data != null);
                   opt.MapFrom(src => src.Data);
               });
                       
               

            CreateMap<ProductGeneralInfoVM, ProductGeneralInfoDto>()
                .ForMember(dest => dest.InitialDate, opt => opt.Ignore())
           //   .ForMember(dest => dest.EndDate, opt => opt.Ignore())

                .AfterMap((src, dest) =>
                {
                    dest.InitialDate = DateTime.Now;
               //   dest.EndDate = DateTime.Now;
                });

            CreateMap<ProductHistoricalInfoVM, ProductHistoricalInfoDto>()
                .ForMember(dest => dest.ProductData, opt => opt.MapFrom(src => src.Data));
            
            CreateMap<TransactionUpdateVM, TransactionUpdateDto>();
            CreateMap<TransactionVM, TransactionDto>();
            CreateMap<ProductDataUpdateVM, ProductDataUpdateDto>();
            CreateMap<ProductDataVM, ProductDataDto>();
            CreateMap<ProductVM, ProductDto>();

            CreateMap<ProductUpdateVM, ProductUpdateDto>()
                .ForMember(dest => dest.ProductGeneralInfo, opt =>
                {
                    opt.PreCondition(src => src.Info != null);
                    opt.MapFrom(src => src.Info);
                })
                .ForMember(dest => dest.ProductHistoricalInfoCollection, opt =>
                {
                    opt.PreCondition(src => src.Data != null);
                    opt.MapFrom(src => src.Data);
                })
                .ForMember(dest => dest.transactions, opt => opt.PreCondition(src => src.transactions != null));

            CreateMap<ProductHistoricalInfoCollectionVM, ProductHistoricalInfoCollectionDto>()
               .ForMember(dest => dest.ProductDataCollection, opt => opt.MapFrom(src => src.Data));

            CreateMap<ProductTransactionsCollectionVM, ProductTransactionsCollectionDto>();

            CreateMap<ProductDataCollectionVM, ProductDataCollectionDto>()
                .ForMember(dest => dest.Add, opt => opt.MapFrom(src => src.Add))
                .ForMember(dest => dest.Update, opt => opt.MapFrom(src => src.Update))
                .ForMember(dest => dest.Remove, opt => opt.MapFrom(src => src.Remove));

            CreateMap<UploadHistoricalPricesVM, UploadHistoricalPricesDto>();

        }
    }
}
