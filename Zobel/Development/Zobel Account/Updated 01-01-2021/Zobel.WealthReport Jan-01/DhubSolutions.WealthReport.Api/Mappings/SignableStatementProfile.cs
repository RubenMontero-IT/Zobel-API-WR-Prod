using AutoMapper;
using DhubSolutions.WealthReport.Api.ViewModels;
using DhubSolutions.WealthReport.Application.Dtos;
using DhubSolutions.WealthReport.Domain.Entities;
using System;
using System.Linq;

namespace DhubSolutions.WealthReport.Api.Mappings
{
    public class SignableStatementProfile : Profile
    {
        public SignableStatementProfile()
        {
            CreateMap<SignableStatement, SignableStatementVM>()
                .ForMember(vm => vm.Name, opt => opt.MapFrom(e => e.StatementCategory.StatementCategoryName))
                .ForMember(vm => vm.Content, opt => opt.MapFrom(e => e.Content))
                .ForMember(vm => vm.Signers, opt => opt.MapFrom(e => e.StatementSigners))
                .ForMember(vm => vm.SignedOff, opt => opt.Ignore())

                .AfterMap((e, vm) =>
                {
                    vm.SignedOff = e.IsSignedOff();
                });

            CreateMap<SignableStatementVM, SignableStatementDto>()
                .ForMember(dto => dto.Name, opt => opt.MapFrom(vm => vm.Name))
                .ForMember(dto => dto.Content, opt => opt.MapFrom(vm => vm.Content))
                .ForMember(dto => dto.SignedOff, opt => opt.MapFrom(vm => vm.SignedOff))
                .ForMember(dto => dto.SignedBy, opt => opt.Ignore())
                .ForMember(dto => dto.SignedDate, opt => opt.Ignore())

                .AfterMap((vm, dto) =>
                {
                    dto.SignedBy = vm.Signers.SingleOrDefault()?.SignedBy;
                    dto.SignedDate = DateTime.Now;
                });
        }
    }
}
